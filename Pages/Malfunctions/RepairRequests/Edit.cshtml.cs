using EquipmentManagementSystem.Authorization;
using EquipmentManagementSystem.Data;
using EquipmentManagementSystem.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace EquipmentManagementSystem.Pages.Malfunctions.RepairRequests
{
    public class EditModel : BasePageModel
    {
        public EditModel(MalfunctionContext context,
            IAuthorizationService authorizationService,
            UserManager<User> userManager)
            : base(context, authorizationService, userManager)
        {
        }

        [BindProperty]
        public RepairRequest RepairRequest { get; set; }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            RepairRequest = await _context.RepairRequest
                                .Include(m => m.MalfunctionWorkOrder)
                                .ThenInclude(m => m.Instrument)
                                .FirstOrDefaultAsync(m => m.ID == id);

            if (RepairRequest == null)
            {
                return NotFound();
            }

            // 如果信息已确认或工单已完成则跳转到工单详情页
            if (RepairRequest.IsConfirm || RepairRequest.MalfunctionWorkOrder.Progress == WorkOrderProgress.Completed)
            {
                return RedirectToPage("../WorkOrders/Details", new { id = RepairRequest.MalfunctionWorkOrderID });
            }

            var isAuthorized = await _authorizationService.AuthorizeAsync(User, RepairRequest.MalfunctionWorkOrder, Operations.Update);

            if (!isAuthorized.Succeeded)
            {
                return Forbid();
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int id)
        {

            RepairRequest = await _context.RepairRequest
                                .Include(m => m.MalfunctionWorkOrder)
                                .ThenInclude(m => m.Instrument)
                                .FirstOrDefaultAsync(m => m.ID == id);

            if (RepairRequest == null)
            {
                return NotFound();
            }

            var isAuthorized = await _authorizationService.AuthorizeAsync(User, RepairRequest.MalfunctionWorkOrder, Operations.Update);

            if (!isAuthorized.Succeeded)
            {
                return Forbid();
            }

            // 如果信息已确认或工单已完成则跳转到工单详情页
            if (RepairRequest.IsConfirm || RepairRequest.MalfunctionWorkOrder.Progress == WorkOrderProgress.Completed)
            {
                return RedirectToPage("../WorkOrders/Details", new { id = RepairRequest.MalfunctionWorkOrderID });
            }

            if (await TryUpdateModelAsync<RepairRequest>(
                    RepairRequest,
                    "RepairRequest",
                    i => i.RequestTime, i => i.BookingsTime, i => i.Fixer, i => i.Engineer))
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

        // 批准维修申请
        public async Task<IActionResult> OnPutComfirmAsync(int id)
        {
            RepairRequest = await _context.RepairRequest
                                .Include(m => m.MalfunctionWorkOrder)
                                .ThenInclude(m => m.Instrument)
                                .FirstOrDefaultAsync(m => m.ID == id);

            if (RepairRequest == null)
            {
                return new JsonResult("未找到该记录");
            }

            var isAuthorized = await _authorizationService.AuthorizeAsync(User, RepairRequest.MalfunctionWorkOrder, Operations.Approve);

            if (!isAuthorized.Succeeded)
            {
                return new JsonResult("权限不足");
            }

            if (RepairRequest.Engineer == null)
            {
                return new JsonResult("请补充对接工程师信息");
            }

            RepairRequest.IsConfirm = true;

            try
            {
                await _context.SaveChangesAsync();
                return new JsonResult("维修申请已批准！");
            }
            catch (Exception ex)
            {
                return new JsonResult($"维修申请未批准，错误信息：{ex}");
            }
        }
    }
}
