using EquipmentManagementSystem.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace EquipmentManagementSystem.Pages.Calibrations
{
    public class EditModel : PageModel
    {
        private readonly EquipmentManagementSystem.Data.EquipmentContext _context;

        public EditModel(EquipmentManagementSystem.Data.EquipmentContext context)
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
            //ViewData["InstrumentID"] = new SelectList(_context.Instruments, "ID", "ID");
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Calibration = await _context.Calibrations.FirstAsync(m => m.ID == id);

            if (Calibration == null)
            {
                return NotFound();
            }

            if (await TryUpdateModelAsync<Calibration>(
                    Calibration,
                    "Calibration",
                    i => i.Date, i => i.Unit, i => i.Result, i => i.Calibrator))
            {
                try
                {
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CalibrationExists(Calibration.ID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }

                return RedirectToPage("../Instruments/Details", new { id = Calibration.InstrumentID });
            }
            return Page();
        }

        private bool CalibrationExists(int id)
        {
            return _context.Calibrations.Any(e => e.ID == id);
        }
    }
}
