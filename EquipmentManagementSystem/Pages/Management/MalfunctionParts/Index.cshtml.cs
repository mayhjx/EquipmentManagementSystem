using EquipmentManagementSystem.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EquipmentManagementSystem.Pages.Management.MalfunctionParts
{
    public class IndexModel : PageModel
    {
        private readonly EquipmentManagementSystem.Data.EquipmentContext _context;

        public IndexModel(EquipmentManagementSystem.Data.EquipmentContext context)
        {
            _context = context;
        }

        public IList<MalfunctionPart> MalfunctionPart { get; set; }

        public async Task OnGetAsync()
        {
            MalfunctionPart = await _context.MalfunctionParts.ToListAsync();
        }
    }
}
