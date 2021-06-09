using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Portal_Project.Data;
using Portal_Project.Models.Portal;
using Portal_Project.Models.Portal.VMC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SignInResult = Microsoft.AspNetCore.Identity.SignInResult;

namespace Portal_Project.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _host;

        public AccountController(UserManager<ApplicationUser> userManager,
                                 SignInManager<ApplicationUser> signInManager,
                                 ApplicationDbContext context,
                                 IWebHostEnvironment host)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _context = context;
            _host = host;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login([Bind("Username,Password,RememberMe")] LoginVM model)
        {
            if (ModelState.IsValid)
            {
                ApplicationUser user = _userManager.FindByNameAsync(model.Username).Result;

                if (user == null)
                {
                    ModelState.AddModelError(string.Empty, "Invalid Username / Password");
                }
                else
                {
                    SignInResult result = await _signInManager.PasswordSignInAsync(user.UserName,
                                                                                   model.Password,
                                                                                   model.RememberMe,
                                                                                   true);

                    if (result.Succeeded)
                    {
                        var userRoles = await _userManager.GetRolesAsync(user);

                        if (_userManager.IsInRoleAsync(user, "Admin").Result)
                        {
                            return RedirectToAction("Index", "Home", new { area = "Admin" });
                        }
                        else
                        {
                            return RedirectToAction("Index", "Home", new { area = "User" });
                        }
                    }
                }
            }

            return View(model);
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();

            return RedirectToAction("Index", "Home");
        }
    }
}