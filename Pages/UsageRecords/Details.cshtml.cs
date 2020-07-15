using System.Threading.Tasks;
using EquipmentManagementSystem.Data;
using EquipmentManagementSystem.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

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
                                        .Include(u => u.Project)
                                            .ThenInclude(p => p.Group)
                                        .FirstOrDefaultAsync(u => u.Id == id);

            if (UsageRecord == null)
            {
                return NotFound();
            }

            return Page();
        }
    }
}
