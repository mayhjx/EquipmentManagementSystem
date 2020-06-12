using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using EquipmentManagementSystem.Data;
using EquipmentManagementSystem.Models;

namespace EquipmentManagementSystem.Pages.DailyUsageRecord
{
    public class DeleteModel : PageModel
    {
        private readonly EquipmentManagementSystem.Data.EquipmentContext _context;

        public DeleteModel(EquipmentManagementSystem.Data.EquipmentContext context)
        {
            _context = context;
        }

        [BindProperty]
        public UsageRecord UsageRecord { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            UsageRecord = await _context.UsageRecords
                .Include(u => u.Instrument).FirstOrDefaultAsync(m => m.Id == id);

            if (UsageRecord == null)
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

            UsageRecord = await _context.UsageRecords.FindAsync(id);

            if (UsageRecord != null)
            {
                _context.UsageRecords.Remove(UsageRecord);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
