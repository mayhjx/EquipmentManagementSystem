using EquipmentManagementSystem.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace EquipmentManagementSystem.Pages.Calibrations
{
    [Authorize(Roles = "设备主任")]
    public class DeleteModel : PageModel
    {
        private readonly EquipmentManagementSystem.Data.EquipmentContext _context;

        public DeleteModel(EquipmentManagementSystem.Data.EquipmentContext context)
        {
            _context = context;
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

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Calibration = await _context.Calibrations.FindAsync(id);
            var instrumentID = Calibration.InstrumentID;

            if (Calibration != null)
            {
                _context.Calibrations.Remove(Calibration);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("../Instruments/Details", new { id = instrumentID });
            //return RedirectToPage("../Instruments/Index");
        }
    }
}
