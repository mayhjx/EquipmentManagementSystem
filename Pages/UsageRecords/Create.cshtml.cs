using EquipmentManagementSystem.Data;
using EquipmentManagementSystem.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Threading.Tasks;

namespace EquipmentManagementSystem.Pages.UsageRecords
{
    [AllowAnonymous]
    public class CreateModel : PageModel
    {
        private readonly EquipmentContext _context;

        public CreateModel(EquipmentContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            ViewData["InstrumentId"] = new SelectList(_context.Instruments, "ID", "ID");
            ViewData["Project"] = new SelectList(_context.Projects, "Name", "Name");
            return Page();
        }

        [BindProperty]
        public UsageRecord UsageRecord { get; set; }

        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.UsageRecords.Add(UsageRecord);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
