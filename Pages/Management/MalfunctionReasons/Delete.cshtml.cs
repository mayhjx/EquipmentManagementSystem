using EquipmentManagementSystem.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace EquipmentManagementSystem.Pages.Management.MalfunctionReasons
{
    public class DeleteModel : PageModel
    {
        private readonly EquipmentManagementSystem.Data.EquipmentContext _context;

        public DeleteModel(EquipmentManagementSystem.Data.EquipmentContext context)
        {
            _context = context;
        }

        [BindProperty]
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

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            MalfunctionReason = await _context.MalfunctionReason.FindAsync(id);

            if (MalfunctionReason != null)
            {
                _context.MalfunctionReason.Remove(MalfunctionReason);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
