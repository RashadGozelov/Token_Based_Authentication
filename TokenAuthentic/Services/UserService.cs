using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Mapster;
using Microsoft.AspNetCore.Identity;
using TokenAuthentic.Domain.Response;
using TokenAuthentic.Domain.Service;
using TokenAuthentic.Models;
using TokenAuthentic.ResourceViewModel;

namespace TokenAuthentic.Services
{
    public class UserService : BaseServices, IUserService
    {
        public UserService(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, RoleManager<AppUser> roleManager) : base(userManager, signInManager, roleManager)
        {
        }

        public async Task<Tuple<AppUser, IList<Claim>>> GetUserByRefreshToken(string refreshToken)
        {
            Claim claimrefreshToken = new Claim("refreshToken", refreshToken);

            var users = await userManager.GetUsersForClaimAsync(claimrefreshToken);

            if (users.Any())
            {
                var user = users.First();
                IList<Claim> userClaims = await userManager.GetClaimsAsync(user);
                string refreshTokenEndDate = userClaims.First(u => u.Type == "refreshTokenEndDate").Value;

                if (DateTime.Parse(refreshTokenEndDate)>DateTime.Now)
                {
                    return new Tuple<AppUser, IList<Claim>>(user, userClaims);
                }
                else
                {
                    return new Tuple<AppUser, IList<Claim>>(null, null);
                }
            }

            return new Tuple<AppUser, IList<Claim>>(null, null);
        }

        public async Task<AppUser> GetUserByUserName(string userName)
        {
            return await userManager.FindByNameAsync(userName);
        }

        public async Task<bool> RevokeRefreshToken(string refreshToken)
        {
            var result = await GetUserByRefreshToken(refreshToken);

            if (result.Item1 == null) return false;

            IdentityResult response = await userManager.RemoveClaimsAsync(result.Item1, result.Item2);

            if (response.Succeeded)
            {
                return true;
            }
            return false;
        }

        public async Task<BaseResponse<UserViewModelResource>> UpdateUser(UserViewModelResource userViewModel, string userName)
        {
            AppUser user = await userManager.FindByNameAsync(userName);

            if (userManager.Users.Count(u=>u.PhoneNumber==userViewModel.PhoneNumber)>1)
            {
                return new BaseResponse<UserViewModelResource>("Bu telefon nömrəsi başqa birinə aiddir");
            };

            user.Datetime = userViewModel.Birthday;
            user.Email = userViewModel.Email;
            user.Gender =(int) userViewModel.Gender;
            user.UserName = userViewModel.UserName;
            user.City = userViewModel.City;
            user.PhoneNumber = userViewModel.PhoneNumber;

            IdentityResult result = await userManager.UpdateAsync(user);
            if (result.Succeeded)
            {
                return new BaseResponse<UserViewModelResource>(user.Adapt<UserViewModelResource>());
            }
            else
            {
                return new BaseResponse<UserViewModelResource>(result.Errors.First().Description);
            }
            
            
        }

        public async Task<BaseResponse<AppUser>> UploadUserPicture(string picturePath, string userName)
        {
            AppUser user = await userManager.FindByNameAsync(userName);

            var picture = picturePath;

            IdentityResult result= await userManager.UpdateAsync(user);

            if (result.Succeeded)
            {
                return new BaseResponse<AppUser>(user);
            }
            else
            {
                return new BaseResponse<AppUser>(result.Errors.First().Description);
            }

        }
    }
}
