using EquipmentManagementSystem.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace EquipmentManagementSystem.Pages.Components
{
    [Authorize(Roles = "中心主任")]
    public class DeleteModel : PageModel
    {
        private readonly EquipmentManagementSystem.Data.EquipmentContext _context;

        public DeleteModel(EquipmentManagementSystem.Data.EquipmentContext context)
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

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Component = await _context.Components.FindAsync(id);

            var instrumentID = Component.InstrumentID;

            if (Component != null)
            {
                _context.Components.Remove(Component);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("../Instruments/Details", new { id = instrumentID });
        }
    }
}
