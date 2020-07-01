using System.Threading.Tasks;
using EquipmentManagementSystem.Authorization;
using EquipmentManagementSystem.Data;
using EquipmentManagementSystem.Models;
using EquipmentManagementSystem.Pages.Instruments;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EquipmentManagementSystem.Pages.Calibrations
{
    public class DeleteModel : BasePageModel
    {
        public DeleteModel(EquipmentContext context,
            UserManager<User> userManager,
            IAuthorizationService authorizationService)
            : base(context, userManager, authorizationService)
        {
        }

        [BindProperty]
        public Calibration Calibration { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Calibration = await _context.Calibrations
                            .FirstOrDefaultAsync(m => m.ID == id);

            if (Calibration == null)
            {
                return NotFound();
            }

            var isAuthorized = await _authorizationService.AuthorizeAsync(User, Calibration, Operations.Delete);

            if (!isAuthorized.Succeeded)
            {
                return Forbid();
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Calibration = await _context.Calibrations.FindAsync(id);
            var instrumentID = Calibration.InstrumentID; //用于返回设备详情页面

            var isAuthorized = await _authorizationService.AuthorizeAsync(User, Calibration, Operations.Delete);

            if (!isAuthorized.Succeeded)
            {
                return Forbid();
            }

            if (Calibration != null)
            {
                _context.Calibrations.Remove(Calibration);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("../Instruments/Details", new { id = instrumentID });
        }
    }
}
