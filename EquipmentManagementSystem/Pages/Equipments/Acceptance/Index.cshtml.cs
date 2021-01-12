using System.Collections.Generic;
using System.Threading.Tasks;
using EquipmentManagementSystem.Data;
using EquipmentManagementSystem.Models;
using EquipmentManagementSystem.Pages.Instruments;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using EquipmentManagementSystem.Authorization;

namespace EquipmentManagementSystem.Pages.Equipments.Acceptance
{
    public class IndexModel : BasePageModel
    {
        public IndexModel(EquipmentContext context,
            UserManager<User> userManager,
            IAuthorizationService authorizationService)
            : base(context, userManager, authorizationService)
        {
        }

        public IList<InstrumentAcceptance> InstrumentAcceptance { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            InstrumentAcceptance = await _context.InstrumentAcceptances
                                            .AsNoTracking()
                                            .ToListAsync();

            var isAuthorized = await _authorizationService.AuthorizeAsync(User, InstrumentAcceptance.FirstOrDefault(), Operations.Read);

            if (!isAuthorized.Succeeded)
            {
                return Forbid();
            }

            return Page();
        }
    }
}
