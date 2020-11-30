using EquipmentManagementSystem.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace EquipmentManagementSystem.Pages.Management.MalfunctionReasons
{
    public class EditModel : PageModel
    {
        private readonly EquipmentManagementSystem.Data.EquipmentContext _context;

        public EditModel(EquipmentManagementSystem.Data.EquipmentContext context)
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

        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(MalfunctionReason).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MalfunctionReasonExists(MalfunctionReason.ID))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool MalfunctionReasonExists(int id)
        {
            return _context.MalfunctionReason.Any(e => e.ID == id);
        }
    }
}
