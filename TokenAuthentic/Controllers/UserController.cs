using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Mapster;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using TokenAuthentic.Domain.Service;
using TokenAuthentic.Models;
using TokenAuthentic.ResourceViewModel;

namespace TokenAuthentic.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [Authorize]
    public class UserController : ControllerBase,IActionFilter
    {
        private readonly IUserService userService;

        public UserController(IUserService userService)
        {
            this.userService = userService;
        }

        [HttpGet]
        public async Task<IActionResult> GetUser()
        {
            AppUser user = await userService.GetUserByUserName(User.Identity.Name);

            return Ok(user.Adapt<UserViewModelResource>());
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            throw new NotImplementedException();
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            context.ModelState.Remove("Password");
        }

        [HttpPut]
        public async Task<IActionResult> UpdateUser(UserViewModelResource userViewModelResource)
        {
            var response = await userService.UpdateUser(userViewModelResource, User.Identity.Name);

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
        public async Task<IActionResult> UploadUserPicture(IFormFile picture)
        {
            var fileName = Guid.NewGuid().ToString() + Path.GetExtension(picture.FileName);

            var path = Path.Combine(Directory.GetCurrentDirectory() + "wwwroot/UserPictures", fileName);


            using (var stream=new FileStream(path,FileMode.Create))
            {
                await picture.CopyToAsync(stream);
            }

            var result = new
            {
                path = "https//" + Request.Host + "/UserPictures"
            };

            var response = await userService.UploadUserPicture(result.path, User.Identity.Name);

            if (response.Success)
            {
                return Ok(path);
            }
            else
            {
                return BadRequest(response.Message);
            }
        }
    }
}