using EquipmentManagementSystem.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace EquipmentManagementSystem.Pages.Malfunctions
{
    public class DetailsModel : PageModel
    {
        private readonly EquipmentManagementSystem.Data.EquipmentContext _context;

        public DetailsModel(EquipmentManagementSystem.Data.EquipmentContext context)
        {
            _context = context;
        }

        public Malfunction Malfunction { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Malfunction = await _context.Malfunctions
                .Include(m => m.Component)
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.ID == id);

            if (Malfunction == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
