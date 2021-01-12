using EquipmentManagementSystem.Authorization;
using EquipmentManagementSystem.Data;
using EquipmentManagementSystem.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace EquipmentManagementSystem.Pages.Malfunctions.AccessoriesOrders
{
    public class EditModel : BasePageModel
    {
        public EditModel(EquipmentContext context,
            IAuthorizationService authorizationService,
            UserManager<User> userManager)
            : base(context, authorizationService, userManager)
        {
        }

        [BindProperty]
        public AccessoriesOrder AccessoriesOrder { get; set; }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            AccessoriesOrder = await _context.AccessoriesOrder
                                .Include(m => m.MalfunctionWorkOrder)
                                .ThenInclude(m => m.Instrument)
                                .FirstOrDefaultAsync(m => m.ID == id);

            if (AccessoriesOrder == null)
            {
                return NotFound();
            }

            // 如果工单已完成则跳转到工单详情页
            if (AccessoriesOrder.MalfunctionWorkOrder.Progress == WorkOrderProgress.Completed)
            {
                return RedirectToPage("../WorkOrders/Details", new { id = AccessoriesOrder.MalfunctionWorkOrderID });
            }

            var isAuthorized = await _authorizationService.AuthorizeAsync(User, AccessoriesOrder, Operations.Update);

            if (!isAuthorized.Succeeded)
            {
                return Forbid();
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int id)
        {
            AccessoriesOrder = await _context.AccessoriesOrder
                                .Include(m => m.MalfunctionWorkOrder)
                                .ThenInclude(m => m.Instrument)
                                .FirstOrDefaultAsync(m => m.ID == id);

            if (AccessoriesOrder == null)
            {
                return NotFound();
            }

            // 如果工单已完成则跳转到工单详情页
            if (AccessoriesOrder.MalfunctionWorkOrder.Progress == WorkOrderProgress.Completed)
            {
                return RedirectToPage("../WorkOrders/Details", new { id = AccessoriesOrder.MalfunctionWorkOrderID });
            }

            var isAuthorized = await _authorizationService.AuthorizeAsync(User, AccessoriesOrder, Operations.Update);

            if (!isAuthorized.Succeeded)
            {
                return Forbid();
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
