using EquipmentManagementSystem.Authorization;
using EquipmentManagementSystem.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EquipmentManagementSystem.Areas.Identity.Pages.Account.UserManagement
{
    public class IndexModel : BasePageModel
    {
        public IndexModel(UserManager<User> userManager,
            IAuthorizationService authorizationService)
            : base(userManager, authorizationService)
        {
        }

        public IList<User> Users { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            var isAuthorized = await _authorizationService.AuthorizeAsync(User, new User(), Operations.Read);

            if (!isAuthorized.Succeeded)
            {
                return Forbid();
            }

            Users = await _userManager.Users
                .Where(u => u.UserName != "Admin")
                .AsNoTracking()
                .ToListAsync();

            return Page();
        }
    }
}