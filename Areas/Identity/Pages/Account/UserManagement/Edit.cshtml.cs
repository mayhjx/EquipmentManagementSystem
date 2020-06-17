using EquipmentManagementSystem.Data;
using EquipmentManagementSystem.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EquipmentManagementSystem.Areas.Identity.Pages.Account.UserManagement
{
    [Authorize(Roles = "Administrator, 设备主任")]
    public class EditModel : PageModel
    {
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly EquipmentContext _context;

        public EditModel(EquipmentContext context, UserManager<User> userManager, RoleManager<IdentityRole> roleManager)
        {
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        [BindProperty]
        public User userToUpdate { get; set; }

        [BindProperty]
        public Role role { get; set; }

        public class Role
        {
            [Display(Name = "角色")]
            public string Name { get; set; }
        }
        public async Task<IActionResult> OnGet(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                return NotFound();
            }

            userToUpdate = await _userManager.FindByNameAsync(name);

            if (userToUpdate == null)
            {
                return NotFound();
            }

            ViewData["Roles"] = new SelectList(_roleManager.Roles.Where(u => u.Name != "Administrator").ToList(), "Name", "Name", _userManager.GetRolesAsync(userToUpdate).Result[0]);
            ViewData["Groups"] = new SelectList(_context.Groups, "Name", "Name", userToUpdate.Group);

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                return NotFound();
            }

            // 禁止编辑管理员信息
            if (name == "Admin")
            {
                return Forbid();
            }

            userToUpdate = await _userManager.FindByNameAsync(name);

            if (userToUpdate == null)
            {
                return NotFound();
            }

            if (await TryUpdateModelAsync<User>(
                userToUpdate,
                "userToUpdate",
                i => i.Group, i => i.Name, i => i.Number, i => i.Email))
            {
                userToUpdate.UserName = userToUpdate.Number;
                // 删除已关联的角色
                await _userManager.RemoveFromRolesAsync(userToUpdate, await _userManager.GetRolesAsync(userToUpdate));
                // 关联角色
                await _userManager.AddToRoleAsync(userToUpdate, role.Name);
                await _userManager.UpdateAsync(userToUpdate);
                return RedirectToPage("./Index");
            }

            return Page();
        }
    }
}
