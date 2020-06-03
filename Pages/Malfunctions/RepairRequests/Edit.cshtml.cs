using EquipmentManagementSystem.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace EquipmentManagementSystem.Pages.Malfunctions.RepairRequests
{
    public class EditModel : PageModel
    {
        private readonly EquipmentManagementSystem.Data.MalfunctionContext _context;

        public EditModel(EquipmentManagementSystem.Data.MalfunctionContext context)
        {
            _context = context;
        }

        [BindProperty]
        public RepairRequest RepairRequest { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            RepairRequest = await _context.RepairRequest
                            .FirstOrDefaultAsync(m => m.ID == id);

            if (RepairRequest == null)
            {
                return NotFound();
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            RepairRequest = await _context.RepairRequest
                                .Include(m => m.MalfunctionWorkOrder)
                                .FirstAsync(m => m.ID == id);

            if (RepairRequest == null)
            {
                return NotFound();
            }

            if (await TryUpdateModelAsync<RepairRequest>(
                    RepairRequest,
                    "RepairRequest",
                    i => i.RequestTime, i => i.BookingsTime, i => i.Fixer, i => i.Engineer, i => i.IsConfirm))
            {
                // 更新进度
                if (RepairRequest.MalfunctionWorkOrder.Progress < WorkOrderProgress.RepairRequested)
                {
                    RepairRequest.MalfunctionWorkOrder.Progress = WorkOrderProgress.RepairRequested;
                }
                await _context.SaveChangesAsync();
                return RedirectToPage("../WorkOrders/Details", new { id = RepairRequest.MalfunctionWorkOrderID });
            }

            return Page();
        }
    }
}
