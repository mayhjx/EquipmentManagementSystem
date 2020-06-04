using EquipmentManagementSystem.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace EquipmentManagementSystem.Pages.Asserts
{
    public class EditModel : PageModel
    {
        private readonly EquipmentManagementSystem.Data.EquipmentContext _context;

        public EditModel(EquipmentManagementSystem.Data.EquipmentContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Assert Assert { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Assert = await _context.Asserts
                        .FirstOrDefaultAsync(m => m.ID == id);

            if (Assert == null)
            {
                return NotFound();
            }
            ViewData["instrumentId"] = new SelectList(_context.Instruments, "ID", "ID");
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Assert = await _context.Asserts.FirstAsync(m => m.ID == id);

            if (Assert == null)
            {
                return NotFound();
            }

            if (await TryUpdateModelAsync<Assert>(
                    Assert,
                    "Assert",
                    i => i.Code, i => i.Name, i => i.EntryDate, i => i.SourceUnit, i => i.Remark))
            {
                try
                {
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AssertExists(Assert.ID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }

                return RedirectToPage("../Instruments/Details", new { id = Assert.InstrumentId });
            }
            return Page();
        }

        private bool AssertExists(int id)
        {
            return _context.Asserts.Any(e => e.ID == id);
        }
    }
}
