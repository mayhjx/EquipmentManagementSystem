using EquipmentManagementSystem.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;

namespace EquipmentManagementSystem.Areas.Identity.Data
{
    public class AdminInitialize
    {
        public static async Task InitializeAsync(IServiceProvider service)
        {
            var roleManager = service.GetRequiredService<RoleManager<IdentityRole>>();
            await EnsureRolesAsync(roleManager, "Administrator");
            await EnsureRolesAsync(roleManager, "中心主任");
            await EnsureRolesAsync(roleManager, "中心主管");
            await EnsureRolesAsync(roleManager, "设备管理员");
            await EnsureRolesAsync(roleManager, "设备负责人");
            await EnsureRolesAsync(roleManager, "技术员");

            var userManager = service.GetRequiredService<UserManager<User>>();
            await EnsureAdminAsync(userManager);
        }

        private static async Task EnsureAdminAsync(UserManager<User> userManager)
        {
            var Admin = await userManager.FindByNameAsync("Admin");

            if (Admin != null) return;

            Admin = new User
            {
                UserName = "Admin",
                Name = "系统管理员",
            };

            await userManager.CreateAsync(Admin, "P@ssw0rd");
            await userManager.AddToRoleAsync(Admin, "Administrator");
        }

        private static async Task EnsureRolesAsync(RoleManager<IdentityRole> roleManager, string role)
        {
            var alreadyExists = await roleManager.RoleExistsAsync(role);
            if (alreadyExists) return;
            await roleManager.CreateAsync(new IdentityRole(role));
        }
    }
}
