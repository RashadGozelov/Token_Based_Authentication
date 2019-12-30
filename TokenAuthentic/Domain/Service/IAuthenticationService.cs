using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TokenAuthentic.Domain.Response;
using TokenAuthentic.ResourceViewModel;
using TokenAuthentic.Security.Token;

namespace TokenAuthentic.Domain.Service
{
   public interface IAuthenticationService
    {
        Task<BaseResponse<UserViewModelResource>> SignUp(UserViewModelResource userViewModel);
        Task<BaseResponse<AccessToken>> SignIn(SignInViewModelResource signInViewModel);
        Task<BaseResponse<AccessToken>> CreateAccessTokenByRefreshToken(RefreshTokenViewModelResource refreshTokenViewModel);
        Task<BaseResponse<AccessToken>> RevokeRefreshToken(RefreshTokenViewModelResource refreshTokenViewModel);
    }
}
