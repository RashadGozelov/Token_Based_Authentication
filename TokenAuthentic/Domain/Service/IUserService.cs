using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using TokenAuthentic.Domain.Response;
using TokenAuthentic.Models;
using TokenAuthentic.ResourceViewModel;

namespace TokenAuthentic.Domain.Service
{
   public interface IUserService
    {
        Task<BaseResponse<UserViewModelResource>> UpdateUser(UserViewModelResource userViewModelResource, string userName);

        Task<AppUser> GetUserByUserName(string userName);

        Task<BaseResponse<AppUser>> UploadUserPicture(string picturePath, string userName);

        Task<Tuple<AppUser, IList<Claim>>> GetUserByRefreshToken(string refreshToken);

        Task<bool> RevokeRefreshToken(string refreshToken);
    }
}
