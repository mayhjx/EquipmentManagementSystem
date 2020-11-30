using EquipmentManagementSystem.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EquipmentManagementSystem.Pages.Management.MalfunctionSolutions
{
    public class IndexModel : PageModel
    {
        private readonly EquipmentManagementSystem.Data.EquipmentContext _context;

        public IndexModel(EquipmentManagementSystem.Data.EquipmentContext context)
        {
            _context = context;
        }

        public IList<MalfunctionSolution> MalfunctionSolution { get; set; }

        public async Task OnGetAsync()
        {
            MalfunctionSolution = await _context.MalfunctionSolution.ToListAsync();
        }
    }
}
