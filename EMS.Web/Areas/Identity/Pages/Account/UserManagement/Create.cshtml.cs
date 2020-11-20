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
    public class CreateModel : PageModel
    {
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly EquipmentContext _context;
        public CreateModel(EquipmentContext context, UserManager<User> userManager, RoleManager<IdentityRole> roleManager)
        {
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public class InputModel
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
            [Display(Name = "角色")]
            public string Role { get; set; }

            [Required]
            [StringLength(100, ErrorMessage = "{0}长度要求：大于等于{2}且小于{1}", MinimumLength = 6)]
            [DataType(DataType.Password)]
            [Display(Name = "密码")]
            public string Password { get; set; }

            [DataType(DataType.Password)]
            [Display(Name = "确认密码")]
            [Compare("Password", ErrorMessage = "确认密码与密码不一致")]
            public string ConfirmPassword { get; set; }
        }

        public IActionResult OnGet()
        {
            ViewData["Roles"] = new SelectList(_roleManager.Roles.Where(u => u.Name != "Administrator").ToList(), "Name", "Name");
            ViewData["Groups"] = new SelectList(_context.Groups, "Name", "Name");
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
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
    }
}
