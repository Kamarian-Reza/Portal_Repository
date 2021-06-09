using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Portal_Project.Areas.Admin.Models.Portal.VMC;
using Portal_Project.Data;
using Portal_Project.Models.Portal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Portal_Project.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class UserController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IPasswordValidator<ApplicationUser> _passwordValidator;
        private readonly IPasswordHasher<ApplicationUser> _passwordHasher;

        public UserController(UserManager<ApplicationUser> userManager,
                              IPasswordValidator<ApplicationUser> passwordValidator,
                              IPasswordHasher<ApplicationUser> passwordHasher,
                              ApplicationDbContext context)
        {
            _userManager = userManager;
            _passwordValidator = passwordValidator;
            _passwordHasher = passwordHasher;
            _context = context;
        }

        public IActionResult Index()
        {
            List<ApplicationUser> model = _userManager.Users.ToList();

            return View(model);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,NationalID,FirstName,LastName,Password")] UserVM model)
        {
            if (ModelState.IsValid)
            {
                ApplicationUser user = new ApplicationUser()
                {
                    UserName = model.NationalID,
                    FirstName = model.FirstName,
                    LastName = model.LastName
                };

                IdentityResult result1 = await _userManager.CreateAsync(user, model.Password);

                if (result1.Succeeded)
                {
                    IdentityResult result2 = _userManager.AddToRoleAsync(user, "User").Result;

                    if (result2.Succeeded)
                    {
                        return RedirectToAction(nameof(Index));
                    }
                    else
                    {
                        foreach (IdentityError error in result2.Errors)
                        {
                            ModelState.AddModelError("", error.Description);
                        }
                    }
                }
                else
                {
                    foreach (IdentityError error in result1.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }
                }
            }

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            ApplicationUser model = await _userManager.FindByIdAsync(id);

            if (model == null)
            {
                return NotFound();
            }

            UserVM user = new UserVM()
            {
                Id = model.Id,
                FirstName = model.FirstName,
                LastName = model.LastName,
                NationalID = model.UserName,
                Password = null
            };

            return View(user);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([Bind("Id,NationalID,FirstName,LastName,Password")] UserVM model)
        {
            if (ModelState.IsValid)
            {
                ApplicationUser user = await _userManager.FindByIdAsync(model.Id);

                if (user == null)
                {
                    return NotFound();
                }

                user.FirstName = model.FirstName;
                user.LastName = model.LastName;

                if (model.Password != "******")
                {
                    IdentityResult result = await _passwordValidator.ValidateAsync(_userManager, user, model.Password);

                    if (result.Succeeded)
                    {
                        user.PasswordHash = _passwordHasher.HashPassword(user, model.Password);
                    }
                    else
                    {
                        foreach (IdentityError error in result.Errors)
                        {
                            ModelState.AddModelError("", error.Description);
                        }
                    }
                }

                if (ModelState.ErrorCount == 0)
                {
                    IdentityResult result = await _userManager.UpdateAsync(user);

                    if (result.Succeeded)
                    {
                        return RedirectToAction(nameof(Index));
                    }
                    else
                    {
                        foreach (IdentityError error in result.Errors)
                        {
                            ModelState.AddModelError("", error.Description);
                        }
                    }
                }
            }

            return View(model);
        }

        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            ApplicationUser user = await _userManager.FindByIdAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            UserVM model = new UserVM()
            {
                Id = user.Id,
                NationalID = user.UserName,
                FirstName = user.FirstName,
                LastName = user.LastName
            };

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            ApplicationUser user = await _userManager.FindByIdAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            bool isDeletable = ! await _context.Bids.AnyAsync(b => b.UserID == user.Id);
            ViewData["IsDeletable"] = isDeletable;

            UserVM model = new UserVM()
            {
                Id = user.Id,
                NationalID = user.UserName,
                FirstName = user.FirstName,
                LastName = user.LastName
            };

            return View(model);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            ApplicationUser model = await _userManager.FindByIdAsync(id);

            IdentityResult result = await _userManager.DeleteAsync(model);

            if (result.Succeeded)
            {
                return RedirectToAction(nameof(Index));
            }
            else
            {
                foreach (IdentityError error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }

                bool isDeletable = true;
                ViewData["IsDeletable"] = isDeletable;

                return View(model);
            }
        }
    }
}