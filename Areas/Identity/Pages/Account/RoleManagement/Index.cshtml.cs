using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EquipmentManagementSystem.Areas.Identity.Pages.Account.RoleManagement
{
    public class IndexModel : PageModel
    {
        private readonly RoleManager<IdentityRole> _roleManager;

        public IndexModel(RoleManager<IdentityRole> roleManager)
        {
            _roleManager = roleManager;
        }

        public IList<IdentityRole> roles { get; set; }

        public async Task<IActionResult> OnGet()
        {
            roles = await _roleManager.Roles.AsNoTracking().ToListAsync();
            return Page();
        }
    }
}
