using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EquipmentManagementSystem.Authorization;
using EquipmentManagementSystem.Data;
using EquipmentManagementSystem.Models;
using EquipmentManagementSystem.Pages.Instruments;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace EquipmentManagementSystem.Pages.Equipments.Instruments
{
    [AllowAnonymous]
    public class IndexModel : BasePageModel
    {
        public IndexModel(EquipmentContext context,
            UserManager<User> userManager,
            IAuthorizationService authorizationService)
            : base(context, userManager, authorizationService)
        {
        }

        public IList<Instrument> Instruments { get; set; }

        // 用来在Index Page确认新建权限
        public Instrument Instrument { get; } = new Instrument();

        public async Task<IActionResult> OnGetAsync()
        {
            var isAuthorized = await _authorizationService.AuthorizeAsync(User, Instrument, Operations.Read);

            if (!isAuthorized.Succeeded)
            {
                return Forbid();
            }

            Instruments = await _context.Instruments.OrderBy(m => m.ID)
                                                    .AsNoTracking()
                                                    .Include(m => m.Calibrations)
                                                    .ToListAsync();
            return Page();
        }
    }
}
