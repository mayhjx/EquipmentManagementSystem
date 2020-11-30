using EquipmentManagementSystem.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EquipmentManagementSystem.Pages.Management.MalfunctionReasons
{
    public class IndexModel : PageModel
    {
        private readonly EquipmentManagementSystem.Data.EquipmentContext _context;

        public IndexModel(EquipmentManagementSystem.Data.EquipmentContext context)
        {
            _context = context;
        }

        public IList<MalfunctionReason> MalfunctionReason { get; set; }

        public async Task OnGetAsync()
        {
            MalfunctionReason = await _context.MalfunctionReason.ToListAsync();
        }
    }
}
