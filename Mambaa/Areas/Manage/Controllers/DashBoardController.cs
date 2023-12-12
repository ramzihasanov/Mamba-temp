using Mamba.Core.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Mambaa.Areas.Manage.Controllers
{
    [Area("Manage")]
    [Authorize(Roles ="Admin,SuperAdmin")]
    public class DashBoardController : Controller
    {
        private readonly UserManager<AppUser> userManager;
        private readonly RoleManager<IdentityRole> roleManager;

        public DashBoardController(UserManager<AppUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            this.userManager = userManager;
            this.roleManager = roleManager;
        }
        public IActionResult Index()
        {
            return View();
        }


        public async Task<IActionResult> CreateAdmin()
        {
            AppUser appUser = new AppUser()
            {
                Fullname = "remzi",
                UserName = "Remzi1919"
            };

            var result = await userManager.CreateAsync(appUser, "Remzi2004@");

            return Ok();
        }
        public async Task<IActionResult> CreateRole()
        {
            IdentityRole role1 = new IdentityRole("SuperAdmin");
            IdentityRole role2 = new IdentityRole("Admin");
            IdentityRole role3 = new IdentityRole("Member");


            await roleManager.CreateAsync(role1);
            await roleManager.CreateAsync(role2);
            await roleManager.CreateAsync(role3);

            return Ok();

        }

        public async Task<IActionResult> AddRoleAdmin()
        {
            var appUser = await userManager.FindByNameAsync("Remzi1919");
            await userManager.AddToRoleAsync(appUser, "SuperAdmin");

            return Ok();

        }
    }
}
