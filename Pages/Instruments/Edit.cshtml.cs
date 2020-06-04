using EquipmentManagementSystem.Data;
using EquipmentManagementSystem.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace EquipmentManagementSystem.Pages.Instruments
{
    public class EditModel : PageModel
    {
        private readonly EquipmentContext _context;

        public EditModel(EquipmentContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Instrument Instrument { get; set; }

        public async Task<IActionResult> OnGetAsync(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Instrument = await _context.Instruments.FirstOrDefaultAsync(m => m.ID == id);

            if (Instrument == null)
            {
                return NotFound();
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Instrument = await _context.Instruments.FirstAsync(m => m.ID == id);

            if (Instrument == null)
            {
                return NotFound();
            }

            if (await TryUpdateModelAsync<Instrument>(
                    Instrument,
                    "Instrument",
                    i => i.Platform, i => i.Name, i => i.StartUsingDate, i => i.CalibrationCycle,
                    i => i.MetrologicalCharacteristics, i => i.Status, i => i.Location, i => i.Principal,
                    i => i.Remark, i => i.NewSystemCode))
            {
                try
                {
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!InstrumentExists(Instrument.ID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }

                return RedirectToPage("../Instruments/Details", new { id = Instrument.ID });
            }
            return Page();
        }

        private bool InstrumentExists(string id)
        {
            return _context.Instruments.Any(e => e.ID == id);
        }
    }
}
