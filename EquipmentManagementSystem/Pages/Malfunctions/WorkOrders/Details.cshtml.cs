using EquipmentManagementSystem.Data;
using EquipmentManagementSystem.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace EquipmentManagementSystem.Pages.Malfunctions.WorkOrders
{
    public class DetailsModel : PageModel
    {
        private readonly EquipmentContext _context;

        public DetailsModel(EquipmentContext context)
        {
            _context = context;
        }

        public MalfunctionWorkOrder MalfunctionWorkOrder { get; set; }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            MalfunctionWorkOrder = await _context.MalfunctionWorkOrder
                                        .AsNoTracking()
                                        .Include(m => m.MalfunctionInfo)
                                        .Include(m => m.Investigation)
                                        .Include(m => m.RepairRequest)
                                        .Include(m => m.AccessoriesOrder)
                                        .Include(m => m.Repair)
                                        .Include(m => m.Validation)
                                        .Include(m => m.Instrument)
                                        .FirstOrDefaultAsync(m => m.ID == id);

            if (MalfunctionWorkOrder == null)
            {
                return NotFound();
            }

            return Page();
        }
    }
}
