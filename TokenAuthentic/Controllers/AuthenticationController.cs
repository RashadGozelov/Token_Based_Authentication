using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TokenAuthentic.Domain.Response;
using TokenAuthentic.Domain.Service;
using TokenAuthentic.ResourceViewModel;

namespace TokenAuthentic.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private IAuthenticationService authenticationService;

        public AuthenticationController(IAuthenticationService service)
        {
            this.authenticationService = service;
        }

        [HttpGet]
        public ActionResult Authentication()
        {
            return Ok(User.Identity.IsAuthenticated);
        }

        [HttpPost]
        public async Task<IActionResult> SignUp(UserViewModelResource userViewModelResource)
        {
           BaseResponse<UserViewModelResource> response= await this.authenticationService.SignUp(userViewModelResource);

            if (response.Success)
            {
                return Ok(response.Extra);
            }
            else
            {
                return BadRequest(response.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> SignIn(SignInViewModelResource signInViewModelResource)
        {
            var response = await authenticationService.SignIn(signInViewModelResource);

            if (response.Success)
            {
                return Ok(response.Extra);
            }
            else
            {
                return BadRequest(response.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateAccessTokenByRefreshToken(RefreshTokenViewModelResource refreshTokenViewModelResource)
        {
            var refreshToken = await authenticationService.CreateAccessTokenByRefreshToken(refreshTokenViewModelResource);

            if (refreshToken.Success)
            {
                return Ok(refreshToken.Extra);
            }
            else
            {
                return BadRequest(refreshToken.Message);
            }

        }

        [HttpPost]
        public async Task<IActionResult> RevokeRefreshToken(RefreshTokenViewModelResource refreshTokenViewModelView)
        {
            var response = await authenticationService.RevokeRefreshToken(refreshTokenViewModelView);

            if (response.Success)
            {
                return Ok(response.Extra);
            }
            else
            {
                return BadRequest(response.Message);
            }

        }
    }
}