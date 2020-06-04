using EquipmentManagementSystem.Data;
using EquipmentManagementSystem.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Threading.Tasks;

namespace EquipmentManagementSystem.Pages.Calibrations
{
    public class CreateModel : PageModel
    {
        private readonly EquipmentContext _context;

        public CreateModel(EquipmentContext context)
        {
            _context = context;
        }

        public IActionResult OnGet(string id)
        {
            ViewData["InstrumentID"] = new SelectList(_context.Instruments, "ID", "ID", id);
            return Page();
        }

        [BindProperty]
        public Calibration Calibration { get; set; }

        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Calibrations.Add(Calibration);
            await _context.SaveChangesAsync();

            return RedirectToPage("../Instruments/Details", new { id = Calibration.InstrumentID });
        }
    }
}
