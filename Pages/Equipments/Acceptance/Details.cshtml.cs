using System.Threading.Tasks;
using EquipmentManagementSystem.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace EquipmentManagementSystem.Pages.Equipments.Acceptance
{
    public class DetailsModel : PageModel
    {
        private readonly EquipmentManagementSystem.Data.EquipmentContext _context;

        public DetailsModel(EquipmentManagementSystem.Data.EquipmentContext context)
        {
            _context = context;
        }

        public InstrumentAcceptance InstrumentAcceptance { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            InstrumentAcceptance = await _context.InstrumentAcceptances
                .FirstOrDefaultAsync(m => m.Id == id);

            if (InstrumentAcceptance == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
