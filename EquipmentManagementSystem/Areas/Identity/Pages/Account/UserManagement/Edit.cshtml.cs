using EquipmentManagementSystem.Authorization;
using EquipmentManagementSystem.Data;
using EquipmentManagementSystem.Interfaces;
using EquipmentManagementSystem.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EquipmentManagementSystem.Areas.Identity.Pages.Account.UserManagement
{
    public class EditModel : BasePageModel
    {
        private readonly IGroupRepository _groupRepo;
        public EditModel(IGroupRepository groupRepository,
            RoleManager<IdentityRole> roleManager,
            UserManager<User> userManager,
            IAuthorizationService authorizationService)
            : base(userManager, authorizationService)
        {
            _groupRepo = groupRepository;
            Role = new RoleModel();
        }

        [BindProperty]
        public User userToUpdate { get; set; }

        [BindProperty]
        public RoleModel Role { get; set; }

        public SelectList Groups { get; set; }

        public async Task<IActionResult> OnGet(string name)
        {
            var isAuthorized = await _authorizationService.AuthorizeAsync(User, new User(), Operations.Update);

            if (!isAuthorized.Succeeded)
            {
                return Forbid();
            }

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

            Role.Name = (await _userManager.GetRolesAsync(userToUpdate)).ToList().FirstOrDefault();
            Groups = new SelectList(await _groupRepo.GetAll(), "Name", "Name", userToUpdate.Group);

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(string name)
        {
            var isAuthorized = await _authorizationService.AuthorizeAsync(User, new User(), Operations.Update);

            if (!isAuthorized.Succeeded)
            {
                return Forbid();
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
                if (userToUpdate.Group == null)
                {
                    userToUpdate.Group = "";
                }

                userToUpdate.UserName = userToUpdate.Number;
                // 删除已关联的角色
                await _userManager.RemoveFromRolesAsync(userToUpdate, await _userManager.GetRolesAsync(userToUpdate));
                // 关联角色
                await _userManager.AddToRoleAsync(userToUpdate, Role.Name);
                await _userManager.UpdateAsync(userToUpdate);
                return RedirectToPage("./Index");
            }

            return Page();
        }

        public class RoleModel
        {
            [Display(Name = "角色")]
            public string Name { get; set; }


            public List<SelectListItem> Roles { get; } = new List<SelectListItem>
            {
                new SelectListItem { Value="中心主任",Text="中心主任"},
                new SelectListItem { Value="中心主管",Text="中心主管"},
                new SelectListItem { Value="设备管理员",Text="设备管理员"},
                new SelectListItem { Value="设备负责人",Text="设备负责人"},
                new SelectListItem { Value="技术员",Text="技术员"},
            };
        }
    }
}
