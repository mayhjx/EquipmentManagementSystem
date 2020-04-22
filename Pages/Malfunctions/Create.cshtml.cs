using EquipmentManagementSystem.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Threading.Tasks;

namespace EquipmentManagementSystem.Pages.Malfunctions
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
            ViewData["InstrumentOption"] = new SelectList(_context.Instruments, "ID", "ID");
            ViewData["FieldOption"] = new SelectList(_context.MalfunctionFields, "ID", "Name");


            return Page();
        }

        [BindProperty]
        public Malfunction Malfunction { get; set; }

        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Malfunctions.Add(Malfunction);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
