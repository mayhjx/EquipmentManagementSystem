using System.Threading.Tasks;
using EquipmentManagementSystem.Data;
using EquipmentManagementSystem.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace EquipmentManagementSystem.Pages.Management.MaintenanceContents
{
    public class DeleteModel : PageModel
    {
        private readonly EquipmentContext _context;

        public DeleteModel(EquipmentContext context)
        {
            _context = context;
        }

        [BindProperty]
        public MaintenanceContent MaintenanceContent { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            MaintenanceContent = await _context.MaintenanceContents.FirstOrDefaultAsync(m => m.Id == id);

            if (MaintenanceContent == null)
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

            MaintenanceContent = await _context.MaintenanceContents.FindAsync(id);

            if (MaintenanceContent != null)
            {
                _context.MaintenanceContents.Remove(MaintenanceContent);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
