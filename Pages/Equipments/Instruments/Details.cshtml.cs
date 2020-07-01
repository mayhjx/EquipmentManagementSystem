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
            // TODO: 如果故障信息包含的附件大于1MB，下面的语句会耗费800ms左右
            Instrument = await _context.Instruments
                                .AsNoTracking()
                                //.Include(a => a.Assert)
                                .Include(b => b.Calibrations)
                                .Include(c => c.Components)
                                .Include(d => d.MalfunctionWorkOrder)
                                    .ThenInclude(e => e.MalfunctionInfo)
                                .FirstOrDefaultAsync(m => m.ID == id);

            if (Instrument == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
