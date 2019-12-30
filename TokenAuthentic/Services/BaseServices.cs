using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TokenAuthentic.Models;

namespace TokenAuthentic.Services
{
    public class BaseServices
    {
        protected UserManager<AppUser> userManager { get; }

        protected SignInManager<AppUser> signInManager { get; }

        protected RoleManager<AppUser> roleManager { get; }

        public BaseServices(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, RoleManager<AppUser> roleManager)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.roleManager = roleManager;
        }
    }
}
