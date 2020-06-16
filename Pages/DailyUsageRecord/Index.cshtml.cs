using EquipmentManagementSystem.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EquipmentManagementSystem.Pages.DailyUsageRecord
{
    [AllowAnonymous]
    public class IndexModel : PageModel
    {
        private readonly EquipmentManagementSystem.Data.EquipmentContext _context;

        public IndexModel(EquipmentManagementSystem.Data.EquipmentContext context)
        {
            _context = context;
        }

        public IList<UsageRecord> UsageRecord { get; set; }

        public async Task OnGetAsync()
        {
            UsageRecord = await _context.UsageRecords
                .Include(u => u.Instrument)
                .AsNoTracking()
                .ToListAsync();
        }
    }
}
