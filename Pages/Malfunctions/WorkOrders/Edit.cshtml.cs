using EquipmentManagementSystem.Authorization;
using EquipmentManagementSystem.Data;
using EquipmentManagementSystem.Models;
using EquipmentManagementSystem.Pages.Instruments;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace EquipmentManagementSystem.Pages.Malfunctions.WorkOrders
{
    public class EditModel : BasePageModel
    {
        public EditModel(MalfunctionContext context,
            IAuthorizationService authorization,
            UserManager<User> userManager)
            : base(context, authorization, userManager)
        {
        }

        [BindProperty]
        public MalfunctionWorkOrder MalfunctionWorkOrder { get; set; }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            MalfunctionWorkOrder = await _context.MalfunctionWorkOrder
                                    .Include(m => m.Instrument)
                                    .FirstOrDefaultAsync(m => m.ID == id);

            if (MalfunctionWorkOrder == null)
            {
                return NotFound();
            }

            var isAuthorized = await _authorizationService.AuthorizeAsync(User, MalfunctionWorkOrder, Operations.Update);

            if (!isAuthorized.Succeeded)
            {
                return Forbid();
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int id)
        {
            MalfunctionWorkOrder = await _context.MalfunctionWorkOrder
                                .Include(m => m.Instrument)
                                .FirstOrDefaultAsync(m => m.ID == id);

            if (MalfunctionWorkOrder == null)
            {
                return NotFound();
            }

            var isAuthorized = await _authorizationService.AuthorizeAsync(User, MalfunctionWorkOrder, Operations.Update);

            if (!isAuthorized.Succeeded)
            {
                return Forbid();
            }

            if (await TryUpdateModelAsync<MalfunctionWorkOrder>(
                    MalfunctionWorkOrder,
                    "MalfunctionWorkOrder",
                    i => i.InstrumentID, i => i.Progress, i => i.CreatedTime, i => i.Creator))
            {
                if (MalfunctionWorkOrder.Progress < WorkOrderProgress.Completed)
                    MalfunctionWorkOrder.Progress = WorkOrderProgress.Completed;

                if (MalfunctionWorkOrder.Instrument.Status == InstrumentStatus.Malfunction)
                    MalfunctionWorkOrder.Instrument.Status = InstrumentStatus.Using;

                await _context.SaveChangesAsync();

                return RedirectToPage("../WorkOrders/Index");
            }

            return Page();
        }

        // 更新工单进度为已完成
        public async Task<IActionResult> OnPutCompleteWorkOrderAsync(int id)
        {
            MalfunctionWorkOrder = await _context.MalfunctionWorkOrder
                                .Include(m => m.Instrument)
                                .FirstOrDefaultAsync(m => m.ID == id);

            if (MalfunctionWorkOrder == null)
            {
                return new JsonResult("未找到该记录");
            }

            var isAuthorized = await _authorizationService.AuthorizeAsync(User, MalfunctionWorkOrder, Operations.Update);

            if (!isAuthorized.Succeeded)
            {
                return new JsonResult("权限不足");
            }

            try
            {
                if (MalfunctionWorkOrder.Progress < WorkOrderProgress.Completed)
                    MalfunctionWorkOrder.Progress = WorkOrderProgress.Completed;

                if (MalfunctionWorkOrder.Instrument.Status == InstrumentStatus.Malfunction)
                    MalfunctionWorkOrder.Instrument.Status = InstrumentStatus.Using;

                await _context.SaveChangesAsync();
                return new JsonResult("工单已完成！");
            }
            catch (Exception ex)
            {
                return new JsonResult($"工单未完成，错误信息：{ex}");
            }
        }
    }
}
