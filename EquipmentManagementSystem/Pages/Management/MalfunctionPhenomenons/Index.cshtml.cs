using EquipmentManagementSystem.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EquipmentManagementSystem.Pages.Management.MalfunctionPhenomenons
{
    public class IndexModel : PageModel
    {
        private readonly EquipmentManagementSystem.Data.EquipmentContext _context;

        public IndexModel(EquipmentManagementSystem.Data.EquipmentContext context)
        {
            _context = context;
        }

        public IList<MalfunctionPhenomenon> MalfunctionPhenomenon { get; set; }

        public async Task OnGetAsync()
        {
            MalfunctionPhenomenon = await _context.MalfunctionPhenomenon.ToListAsync();
        }
    }
}
