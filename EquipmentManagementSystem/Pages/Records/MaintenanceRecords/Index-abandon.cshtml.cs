using System.Collections.Generic;
using System.Threading.Tasks;
using EquipmentManagementSystem.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace EquipmentManagementSystem.Pages.MaintenanceRecords
{
    public class IndexModel : PageModel
    {
        private readonly EquipmentManagementSystem.Data.EquipmentContext _context;

        public IndexModel(EquipmentManagementSystem.Data.EquipmentContext context)
        {
            _context = context;
        }

        public IList<MaintenanceRecord> MaintenanceRecord { get; set; }

        public async Task OnGetAsync()
        {
            MaintenanceRecord = await _context.MaintenanceRecords
                .Include(m => m.Instrument)
                .Include(m => m.Project).ToListAsync();
        }
    }
}
