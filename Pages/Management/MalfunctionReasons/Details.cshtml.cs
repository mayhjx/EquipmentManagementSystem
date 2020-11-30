using EquipmentManagementSystem.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace EquipmentManagementSystem.Pages.Management.MalfunctionReasons
{
    public class DetailsModel : PageModel
    {
        private readonly EquipmentManagementSystem.Data.EquipmentContext _context;

        public DetailsModel(EquipmentManagementSystem.Data.EquipmentContext context)
        {
            _context = context;
        }

        public MalfunctionReason MalfunctionReason { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            MalfunctionReason = await _context.MalfunctionReason.FirstOrDefaultAsync(m => m.ID == id);

            if (MalfunctionReason == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
