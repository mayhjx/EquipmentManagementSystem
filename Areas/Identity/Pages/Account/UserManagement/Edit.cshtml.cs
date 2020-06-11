using EquipmentManagementSystem.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Linq;
using System.Threading.Tasks;

namespace EquipmentManagementSystem.Areas.Identity.Pages.Account.UserManagement
{
    public class EditModel : PageModel
    {
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public EditModel(UserManager<User> userManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        [BindProperty]
        public User userToUpdate { get; set; }

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

            ViewData["Roles"] = new SelectList(_roleManager.Roles.ToList(), "Name", "Name");
            return Page();
        }

        //public async Task<IActionResult> OnPostAsync()
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return Page();
        //    }

        //    var user = new User { UserName = Input.Number, Name = Input.Name, Email = Input.Email, Number = Input.Number };
        //    var result = await _userManager.CreateAsync(user, Input.Password);

        //    if (result.Succeeded)
        //    {
        //        await _userManager.AddToRoleAsync(user, Input.Role);
        //        return RedirectToPage("./Index");
        //    }

        //    foreach (var error in result.Errors)
        //    {
        //        ModelState.AddModelError(string.Empty, error.Description);
        //    }
        //    return Page();
        //}
    }
}
