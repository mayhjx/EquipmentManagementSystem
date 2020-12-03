using EquipmentManagementSystem.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace EquipmentManagementSystem.Pages.Management.MalfunctionSolutions
{
    public class DeleteModel : PageModel
    {
        private readonly EquipmentManagementSystem.Data.EquipmentContext _context;

        public DeleteModel(EquipmentManagementSystem.Data.EquipmentContext context)
        {
            _context = context;
        }

        [BindProperty]
        public MalfunctionSolution MalfunctionSolution { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            MalfunctionSolution = await _context.MalfunctionSolution.FirstOrDefaultAsync(m => m.ID == id);

            if (MalfunctionSolution == null)
            {
                return NotFound();
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            MalfunctionSolution = await _context.MalfunctionSolution.FindAsync(id);

            if (MalfunctionSolution != null)
            {
                _context.MalfunctionSolution.Remove(MalfunctionSolution);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
