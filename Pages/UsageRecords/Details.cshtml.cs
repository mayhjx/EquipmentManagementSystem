using EquipmentManagementSystem.Data;
using EquipmentManagementSystem.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace EquipmentManagementSystem.Pages.UsageRecords
{
    public class DetailsModel : PageModel
    {
        private readonly EquipmentContext _context;

        public DetailsModel(EquipmentContext context)
        {
            _context = context;
        }

        public UsageRecord UsageRecord { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            UsageRecord = await _context.UsageRecords
                                        .AsNoTracking()
                                        .FirstOrDefaultAsync(m => m.Id == id);

            if (UsageRecord == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
