using EquipmentManagementSystem.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace EquipmentManagementSystem.Pages.Instruments
{
    [Authorize]
    public class DeleteModel : PageModel
    {
        private readonly EquipmentManagementSystem.Data.EquipmentContext _context;

        public DeleteModel(EquipmentManagementSystem.Data.EquipmentContext context)
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

            Instrument = await _context.Instruments.FindAsync(id);

            if (Instrument != null)
            {
                _context.Instruments.Remove(Instrument);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
