using EquipmentManagementSystem.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Threading.Tasks;

namespace EquipmentManagementSystem.Pages.MalfunctionWorkOrders
{
    public class CreateModel : PageModel
    {
        private readonly EquipmentManagementSystem.Data.MalfunctionContext _context;

        public CreateModel(EquipmentManagementSystem.Data.MalfunctionContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            ViewData["InstrumentID"] = new SelectList(_context.Set<Instrument>(), "ID", "ID");
            return Page();
        }

        [BindProperty]
        public MalfunctionWorkOrder MalfunctionWorkOrder { get; set; }

        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.MalfunctionWorkOrder.Add(MalfunctionWorkOrder);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
