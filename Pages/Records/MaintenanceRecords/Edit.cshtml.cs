using System.Threading.Tasks;
using EquipmentManagementSystem.Data;
using EquipmentManagementSystem.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace EquipmentManagementSystem.Pages.MaintenanceRecords
{
    public class EditModel : PageModel
    {
        private readonly EquipmentContext _context;

        public EditModel(EquipmentContext context)
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

        public async Task<IActionResult> OnPostAsync(int id)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var maintenanceRecordToUpdate = await _context.MaintenanceRecords
                                                    .FirstOrDefaultAsync(m => m.Id == id);

            if (maintenanceRecordToUpdate == null)
            {
                return NotFound();
            }

            if (await TryUpdateModelAsync<MaintenanceRecord>(
                maintenanceRecordToUpdate,
                "MaintenanceRecord",
                i => i.BeginTime, i => i.EndTime, i => i.Operator))
            {
                await _context.SaveChangesAsync();
                return RedirectToPage("../Index");
            }

            return Page();
        }
    }
}
