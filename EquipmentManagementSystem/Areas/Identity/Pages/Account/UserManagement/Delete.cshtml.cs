using EquipmentManagementSystem.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace EquipmentManagementSystem.Areas.Identity.Pages.Account.UserManagement
{
    public class DeleteModel : BasePageModel
    {
        public DeleteModel(UserManager<User> userManager,
            IAuthorizationService authorizationService)
            : base(userManager, authorizationService)
        {
        }

        [BindProperty]
        public User userToDelete { get; set; }
        public string ErrorMessage { get; set; }

        public async Task<IActionResult> OnGetAsync(string name, bool? saveChangesError = false)
        {
            if (string.IsNullOrEmpty(name))
            {
                return NotFound();
            }

            userToDelete = await _userManager.FindByNameAsync(name);

            if (userToDelete == null)
            {
                return NotFound();
            }

            if (saveChangesError.GetValueOrDefault())
            {
                ErrorMessage = "删除失败，请重试。";
            }

            return Page();
        }

        public async Task<IActionResult> OnPostDeleteAsync(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                return NotFound();
            }

            // 禁止删除管理员
            if (name == "Admin")
            {
                return Forbid();
            }

            userToDelete = await _userManager.FindByNameAsync(name);

            if (userToDelete == null)
            {
                return NotFound();
            }

            try
            {
                await _userManager.DeleteAsync(userToDelete);
                return RedirectToPage("./Index");
            }
            catch (DbUpdateException)
            {
                return RedirectToAction("./Delete",
                                        new { name, saveChangesError = true });
            }

        }
    }
}
