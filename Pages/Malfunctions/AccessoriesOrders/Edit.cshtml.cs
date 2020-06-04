using EquipmentManagementSystem.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace EquipmentManagementSystem.Pages.Malfunctions.AccessoriesOrders
{
    public class EditModel : PageModel
    {
        private readonly EquipmentManagementSystem.Data.MalfunctionContext _context;

        public EditModel(EquipmentManagementSystem.Data.MalfunctionContext context)
        {
            _context = context;
        }

        [BindProperty]
        public AccessoriesOrder AccessoriesOrder { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            AccessoriesOrder = await _context.AccessoriesOrder.FirstOrDefaultAsync(m => m.ID == id);

            if (AccessoriesOrder == null)
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

            AccessoriesOrder = await _context.AccessoriesOrder
                                .Include(m => m.MalfunctionWorkOrder)
                                .FirstAsync(m => m.ID == id);

            if (AccessoriesOrder == null)
            {
                return NotFound();
            }

            if (await TryUpdateModelAsync<AccessoriesOrder>(
                    AccessoriesOrder,
                    "AccessoriesOrder",
                    i => i.Name, i => i.PlaceTime, i => i.ArrivalTime, i => i.Remark))
            {
                // 更新进度
                if (AccessoriesOrder.MalfunctionWorkOrder.Progress < WorkOrderProgress.Waiting)
                {
                    AccessoriesOrder.MalfunctionWorkOrder.Progress = WorkOrderProgress.Waiting;
                }

                await _context.SaveChangesAsync();

                return RedirectToPage("../WorkOrders/Details", new { id = AccessoriesOrder.MalfunctionWorkOrderID });
            }

            return Page();
        }
    }
}
