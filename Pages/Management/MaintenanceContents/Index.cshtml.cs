using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EquipmentManagementSystem.Data;
using EquipmentManagementSystem.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace EquipmentManagementSystem.Pages.Management.MaintenanceContents
{
    public class IndexModel : PageModel
    {
        private readonly EquipmentContext _context;

        public IndexModel(EquipmentContext context)
        {
            _context = context;
        }

        public IList<MaintenanceContent> MaintenanceContents { get; set; }

        public async Task OnGetAsync()
        {
            MaintenanceContents = await _context.MaintenanceContents.OrderBy(m => m.InstrumentPlatform).ToListAsync();
        }
    }
}
