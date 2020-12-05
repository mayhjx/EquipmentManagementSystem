using EquipmentManagementSystem.Data;
using EquipmentManagementSystem.Models;
using EquipmentManagementSystem.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using EquipmentManagementSystem.Authorization;

namespace EquipmentManagementSystem.Areas.Identity.Pages.Account.UserManagement
{
    public class CreateModel : BasePageModel
    {
        private readonly IGroupRepository _groupRepo;
        public CreateModel(IGroupRepository groupRepository , 
            UserManager<User> userManager,
            IAuthorizationService authorizationService) 
            :base(userManager , authorizationService)
        {
            _groupRepo = groupRepository;

            Input = new InputForm();
        }

        [BindProperty]
        public InputForm Input { get; set; }

        public SelectList Groups { get; set; }

        public async Task<IActionResult> OnGet()
        {
            var isAuthorized = await _authorizationService.AuthorizeAsync(User, new User(), Operations.Create);

            if (!isAuthorized.Succeeded)
            {
                return Forbid();
            }

            Groups = new SelectList(await _groupRepo.GetAll(), "Name", "Name");
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var isAuthorized = await _authorizationService.AuthorizeAsync(User, new User(), Operations.Create);

            if (!isAuthorized.Succeeded)
            {
                return Forbid();
            }

            var user = new User { UserName = Input.Number, Name = Input.Name, Email = Input.Email, Number = Input.Number, Group = Input.Group };
            var result = await _userManager.CreateAsync(user, Input.Password);

            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(user, Input.Role);
                return RedirectToPage("./Index");
            }

            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }
            return Page();
        }

        public class InputForm
        {
            [Required]
            [Display(Name = "项目组")]
            public string Group { get; set; }

            [Required]
            [Display(Name = "姓名")]
            public string Name { get; set; }

            [Required]
            [Display(Name = "工号")]
            public string Number { get; set; }

            [Required]
            [EmailAddress(ErrorMessage = "输入的值不是有效的邮箱格式")]
            [Display(Name = "邮箱")]
            public string Email { get; set; }

            [Required]
            [Display(Name = "用户角色")]
            public string Role { get; set; }

            [Required]
            [StringLength(100, MinimumLength = 6)]
            [DataType(DataType.Password)]
            [Display(Name = "密码")]
            public string Password { get; set; }

            [DataType(DataType.Password)]
            [Display(Name = "确认密码")]
            [Compare("Password", ErrorMessage = "确认密码与密码不一致")]
            public string ConfirmPassword { get; set; }

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
