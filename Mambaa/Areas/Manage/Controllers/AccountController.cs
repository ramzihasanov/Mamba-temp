using Mamba.Core.Models;
using Mambaa.Areas.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace Mambaa.Areas.Manage.Controllers
{
    [Area("Manage")]
    //[Authorize(Roles = "Admin,SuperAdmin")]
    public class AccountController : Controller
    {
        private readonly UserManager<AppUser> userManager;
        private readonly SignInManager<AppUser> signInManager;

        public AccountController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
        }
        public IActionResult Index()
        {
            return View();
        }


        public IActionResult Login()
        {


            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(AdminLoginViewModel adminLoginViewModel)
        {
            if (!ModelState.IsValid) return View(adminLoginViewModel);
            AppUser appUser = null;
            appUser = await userManager.FindByNameAsync(adminLoginViewModel.Username);
            if (appUser == null)
            {
                ModelState.AddModelError("", "invalid username or paswword");
                return View();
            }
            var result = await signInManager.PasswordSignInAsync(appUser, adminLoginViewModel.Password, false, false);
            if (!result.Succeeded)
            {
                ModelState.AddModelError("", "invalid username or paswword");
                return View();
            }

            return RedirectToAction("Index", "DashBoard");
        }
    }

}

