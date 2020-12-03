using EquipmentManagementSystem.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Threading.Tasks;

namespace EquipmentManagementSystem.Pages.Management.MalfunctionParts
{
    public class CreateModel : PageModel
    {
        private readonly EquipmentManagementSystem.Data.EquipmentContext _context;

        public CreateModel(EquipmentManagementSystem.Data.EquipmentContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public MalfunctionPart MalfunctionPart { get; set; }

        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.MalfunctionParts.Add(MalfunctionPart);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
