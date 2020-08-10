using System.Threading.Tasks;
using EquipmentManagementSystem.Data;
using EquipmentManagementSystem.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace EquipmentManagementSystem.Pages.MaintenanceRecords
{
    public class DeleteModel : PageModel
    {
        private readonly EquipmentContext _context;

        public DeleteModel(EquipmentContext context)
        {
            _context = context;
        }

        [BindProperty]
        public MaintenanceRecord MaintenanceRecord { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            MaintenanceRecord = await _context.MaintenanceRecords
                .Include(m => m.Instrument)
                .Include(m => m.Project).FirstOrDefaultAsync(m => m.Id == id);

            if (MaintenanceRecord == null)
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

            MaintenanceRecord = await _context.MaintenanceRecords.FindAsync(id);

            if (MaintenanceRecord != null)
            {
                _context.MaintenanceRecords.Remove(MaintenanceRecord);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("../Index");
        }
    }
}
