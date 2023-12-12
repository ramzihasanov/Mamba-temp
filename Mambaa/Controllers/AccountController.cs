using Mamba.Core.Models;
using Mambaa.ViewModels;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Mambaa.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<AppUser> userManager;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly SignInManager<AppUser> signInManager;

        public AccountController(UserManager<AppUser> userManager, RoleManager<IdentityRole> roleManager, SignInManager<AppUser> signInManager)
        {
            this.userManager = userManager;
            this.roleManager = roleManager;
            this.signInManager = signInManager;
        }
        public IActionResult Index()
        {
            return View();
        }


        public IActionResult Register()
        {


            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Register(UserRegisterViewModel userregisterviewmodel)
        {
            if (!ModelState.IsValid) return View();
            AppUser appUser = null;
            appUser = await userManager.FindByNameAsync(userregisterviewmodel.Username);
            if (appUser != null)
            {
                ModelState.AddModelError("Username", "username have a account");
                return View();
            }
            appUser = await userManager.FindByEmailAsync(userregisterviewmodel.Email);
            if (appUser != null)
            {
                ModelState.AddModelError("Username", "username have a account");
                return View();
            }
            AppUser appUser1 = new AppUser()
            {
                Fullname = userregisterviewmodel.Fullname,
                UserName = userregisterviewmodel.Username,
                Email = userregisterviewmodel.Email,  
                
            };

            var result = await userManager.CreateAsync(appUser1, userregisterviewmodel.Password);
            if (!result.Succeeded)
            {
                foreach (var item in result.Errors)
                {
                    ModelState.AddModelError("", item.Description);
                    return View();
                }
            }
            await userManager.AddToRoleAsync(appUser1, "Member");
            await signInManager.SignInAsync(appUser1, isPersistent: false);
            return RedirectToAction("Index", "home");
        }

        [HttpPost]
        public async Task<IActionResult> Login(UserRegisterViewModel userregisterviewmodel)
        {
            if (!ModelState.IsValid) return View();
            AppUser admin = null;
            admin = await userManager.FindByEmailAsync(userregisterviewmodel.Email);

            if (admin == null)
            {
                ModelState.AddModelError("", "Invalid Email or Password");
                return View();
            }
            var result = await signInManager.PasswordSignInAsync(admin, userregisterviewmodel.Password, false, false);

            if (!result.Succeeded)
            {
                ModelState.AddModelError("", "Invalid Email or Password");
                return View();
            }
            return RedirectToAction("Index", "home");
        }
        [HttpPost]
        public IActionResult Logout()
        {
            foreach (var cookie in Request.Cookies.Keys)
            {
                Response.Cookies.Delete(cookie);
            }
            HttpContext.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }


    }
}

