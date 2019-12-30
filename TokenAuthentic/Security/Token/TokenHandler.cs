using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Threading.Tasks;
using TokenAuthentic.Models;

namespace TokenAuthentic.Security.Token
{
    public class TokenHandler : ITokenHandler
    {
        private readonly CustomTokenOptions customTokenOptions;

        public TokenHandler(IOptions<CustomTokenOptions> tokenHandler)
        {
            this.customTokenOptions = tokenHandler.Value;
        }

        public AccessToken CreateAccessToken(AppUser user)
        {
            var accessTokenExpiration = DateTime.Now.AddMinutes(customTokenOptions.AccessTokenExpiration);
            var securityKey = SignHandler.GetSecurityKey(customTokenOptions.SecurityKey);
            SigningCredentials signingCredentials = new SigningCredentials(securityKey,SecurityAlgorithms.HmacSha256Signature);

            JwtSecurityToken jwtSecurityToken = new JwtSecurityToken(
                issuer:customTokenOptions.Issuer,
                audience:customTokenOptions.Audience,
                expires:accessTokenExpiration,
                notBefore:DateTime.Now,
                claims:GetClaims(user),
                signingCredentials:signingCredentials
                );
            var handler = new JwtSecurityTokenHandler();

            var token = handler.WriteToken(jwtSecurityToken);
            AccessToken accessToken = new AccessToken();
            accessToken.Token = token;
            accessToken.RefreshToken = CreateRefreshToken();
            accessToken.Expiration = accessTokenExpiration;

            return accessToken;
        }

        public void RevokeRefreshToken(AppUser user)
        {
            throw new NotImplementedException();
        }

        private string CreateRefreshToken()
        {
            var numberByte = new Byte[32];
            using (var mg = RandomNumberGenerator.Create())
            {
                mg.GetBytes(numberByte);
                return Convert.ToBase64String(numberByte);
            }
        }

        private IEnumerable<Claim> GetClaims(AppUser user)
        {
            var claims = new List<Claim>
            {
               new Claim(ClaimTypes.NameIdentifier,user.Id.ToString()),
               new Claim(JwtRegisteredClaimNames.Email,user.Email),
               new Claim(ClaimTypes.Name, $"{ user.UserName}"),
               new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString())
            };
            return claims;
        }
    }
}
