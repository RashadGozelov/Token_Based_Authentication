using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TokenAuthentic.Models;

namespace TokenAuthentic.Security.Token
{
   public interface ITokenHandler
    {
        AccessToken CreateAccessToken(AppUser user);

        void RevokeRefreshToken(AppUser user);
    }
}
