using EquipmentManagementSystem.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace EquipmentManagementSystem.Areas.Identity.Data
{
    public class AdminInitialize
    {
        public static async Task InitializeAsync(IServiceProvider service)
        {
            var roleManager = service.GetRequiredService<RoleManager<IdentityRole>>();
            await EnsureRolesAsync(roleManager);

            var userManager = service.GetRequiredService<UserManager<User>>();
            await EnsureAdminAsync(userManager);
        }

        private static async Task EnsureAdminAsync(UserManager<User> userManager)
        {
            var Admin = await userManager.Users
                .Where(x => x.UserName == "Admin")
                .SingleOrDefaultAsync();

            if (Admin != null) return;

            Admin = new User
            {
                UserName = "Admin",
                Name = "系统管理员",
            };

            await userManager.CreateAsync(Admin, "P@ssw0rd");
            await userManager.AddToRoleAsync(Admin, "Administrator");
        }

        private static async Task EnsureRolesAsync(RoleManager<IdentityRole> roleManager)
        {
            var alreadyExists = await roleManager.RoleExistsAsync("Administrator");
            if (alreadyExists) return;
            await roleManager.CreateAsync(new IdentityRole("Administrator"));
        }
    }
}
