using System.Collections.Generic;
using System.Threading.Tasks;
using EquipmentManagementSystem.Data;
using EquipmentManagementSystem.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace EquipmentManagementSystem.Pages.Management.MaintenanceTypes
{
    public class IndexModel : PageModel
    {
        private readonly EquipmentContext _context;

        public IndexModel(EquipmentContext context)
        {
            _context = context;
        }

        public IList<MaintenanceType> MaintenanceType { get; set; }

        public async Task OnGetAsync()
        {
            MaintenanceType = await _context.MaintenanceTypes
                .Include(m => m.Content)
                .ToListAsync();
        }
    }
}
