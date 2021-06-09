using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Portal_Project.Models.Portal
{
    public static class CreateDefaults
    {
        public static async Task Roles(IApplicationBuilder app)
        {
            var scope = app.ApplicationServices.CreateScope();

            RoleManager<ApplicationRole> _roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<ApplicationRole>>();

            bool hasAdmin = await _roleManager.RoleExistsAsync("Admin");
            bool hasUser = await _roleManager.RoleExistsAsync("User");

            if (!hasAdmin)
            {
                IdentityResult result = await _roleManager.CreateAsync(new ApplicationRole() { Name = "Admin", NormalizedName = "ADMIN", Title = "Admin"});
            }

            if (!hasUser)
            {
                IdentityResult result = await _roleManager.CreateAsync(new ApplicationRole() { Name = "User", NormalizedName = "USER", Title = "User"});
            }
        }

        public static async Task Users(IApplicationBuilder app, IConfiguration configuration)
        {
            var scope = app.ApplicationServices.CreateScope();

            UserManager<ApplicationUser> _userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();
            RoleManager<ApplicationRole> _roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<ApplicationRole>>();

            if (await _userManager.FindByNameAsync("Admin") == null)
            {
                ApplicationUser user = new ApplicationUser()
                {
                    UserName = "4501114517",
                    FirstName = "Reza",
                    LastName = "Kamarian"
                };

                IdentityResult result = await _userManager.CreateAsync(user, "Abc@123$");

                if (result.Succeeded)
                {
                    await _userManager.AddToRoleAsync(user, "ADMIN");
                }
            }
        }
    }
}