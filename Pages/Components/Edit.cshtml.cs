using EquipmentManagementSystem.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace EquipmentManagementSystem.Pages.Components
{
    public class EditModel : PageModel
    {
        private readonly EquipmentManagementSystem.Data.EquipmentContext _context;

        public EditModel(EquipmentManagementSystem.Data.EquipmentContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Component Component { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Component = await _context.Components
                        .FirstOrDefaultAsync(m => m.ID == id);

            if (Component == null)
            {
                return NotFound();
            }
            //ViewData["instrumentID"] = new SelectList(_context.Instruments, "ID", "ID");
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Component = await _context.Components.FirstAsync(m => m.ID == id);

            if (Component == null)
            {
                return NotFound();
            }

            if (await TryUpdateModelAsync<Component>(
                    Component,
                    "Component",
                    i => i.SerialNumber, i => i.Name, i => i.Model, i => i.Brand))
            {
                await _context.SaveChangesAsync();
                return RedirectToPage("../Instruments/Details", new { id = Component.InstrumentID });
            }
            return Page();
        }

    }
}
