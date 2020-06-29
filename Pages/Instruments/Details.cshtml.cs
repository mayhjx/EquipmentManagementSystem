using EquipmentManagementSystem.Data;
using EquipmentManagementSystem.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace EquipmentManagementSystem.Pages.Instruments
{
    public class DetailsModel : PageModel
    {
        private readonly EquipmentContext _context;

        public DetailsModel(EquipmentContext context)
        {
            _context = context;
        }

        public Instrument Instrument { get; set; }

        public async Task<IActionResult> OnGetAsync(string id)
        {
            Instrument = await _context.Instruments
                                .AsNoTracking()
                                //.Include(a => a.Assert)
                                .Include(b => b.Calibrations)
                                .Include(c => c.Components)
                                .Include(d => d.MalfunctionWorkOrder)
                                    .ThenInclude(e => e.MalfunctionInfo)
                                .Include(d => d.MalfunctionWorkOrder)
                                    .ThenInclude(e => e.Validation)
                                .FirstOrDefaultAsync(m => m.ID == id);

            if (Instrument == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
