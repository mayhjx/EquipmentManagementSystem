using EquipmentManagementSystem.Data;
using EquipmentManagementSystem.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EquipmentManagementSystem.Pages.UsageRecords
{
    public class IndexModel : PageModel
    {
        private readonly EquipmentContext _context;

        public IndexModel(EquipmentContext context)
        {
            _context = context;
        }

        public IList<UsageRecord> UsageRecord { get; set; }

        public async Task OnGetAsync()
        {
            UsageRecord = await _context.UsageRecords
                                .AsNoTracking()
                                .ToListAsync();
        }
    }
}
