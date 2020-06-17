using EquipmentManagementSystem.Authorization;
using EquipmentManagementSystem.Data;
using EquipmentManagementSystem.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace EquipmentManagementSystem.Pages.Calibrations
{
    [Authorize(Roles = "设备管理员, 设备主任, 设备负责人")]
    public class EditModel : PageModel
    {
        private readonly EquipmentContext _context;
        private readonly UserManager<User> _userManager;

        public EditModel(EquipmentContext context, UserManager<User> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        [BindProperty]
        public Calibration Calibration { get; set; }

        public async Task<IActionResult> OnGetAsync(int id)
        {

            Calibration = await _context.Calibrations
                                .Include(m => m.Instrument)
                                .FirstOrDefaultAsync(m => m.ID == id);

            if (Calibration == null)
            {
                return NotFound();
            }

            if (User.IsInRole(Constants.PrincipalRole))
            {
                var principalGroup = _userManager.GetUserAsync(User).Result.Group;
                if (principalGroup != Calibration.Instrument.Group)
                {
                    return Forbid();
                }
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int id)
        {
            Calibration = await _context.Calibrations
                                .Include(m => m.Instrument)
                                .FirstOrDefaultAsync(m => m.ID == id);

            if (Calibration == null)
            {
                return NotFound();
            }

            if (User.IsInRole(Constants.PrincipalRole))
            {
                var principalGroup = _userManager.GetUserAsync(User).Result.Group;
                if (principalGroup != Calibration.Instrument.Group)
                {
                    return Forbid();
                }
            }

            if (await TryUpdateModelAsync<Calibration>(
                    Calibration,
                    "Calibration",
                    i => i.Date, i => i.Unit, i => i.Result, i => i.Calibrator))
            {
                await _context.SaveChangesAsync();

                return RedirectToPage("../Instruments/Details", new { id = Calibration.InstrumentID });
            }

            return Page();
        }

    }
}
