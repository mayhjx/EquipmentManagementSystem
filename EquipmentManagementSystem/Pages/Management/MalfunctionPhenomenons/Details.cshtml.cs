using EquipmentManagementSystem.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace EquipmentManagementSystem.Pages.Management.MalfunctionPhenomenons
{
    public class DetailsModel : PageModel
    {
        private readonly EquipmentManagementSystem.Data.EquipmentContext _context;

        public DetailsModel(EquipmentManagementSystem.Data.EquipmentContext context)
        {
            _context = context;
        }

        public MalfunctionPhenomenon MalfunctionPhenomenon { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            MalfunctionPhenomenon = await _context.MalfunctionPhenomenon.FirstOrDefaultAsync(m => m.ID == id);

            if (MalfunctionPhenomenon == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
