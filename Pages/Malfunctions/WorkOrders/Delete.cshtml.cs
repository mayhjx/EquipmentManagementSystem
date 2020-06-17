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
    public class DeleteModel : BasePageModel
    {
        public DeleteModel(MalfunctionContext context,
            IAuthorizationService authorizationService,
            UserManager<User> userManager)
            : base(context, authorizationService, userManager)
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

            var isAuthorized = await _authorizationService.AuthorizeAsync(User, MalfunctionWorkOrder, Operations.Delete);

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

            var isAuthorized = await _authorizationService.AuthorizeAsync(User, MalfunctionWorkOrder, Operations.Delete);

            if (!isAuthorized.Succeeded)
            {
                return Forbid();
            }

            // 改变故障设备的状态
            MalfunctionWorkOrder.Instrument = await _context.Set<Instrument>().FindAsync(MalfunctionWorkOrder.InstrumentID);
            MalfunctionWorkOrder.Instrument.Status = InstrumentStatus.Using;

            _context.MalfunctionWorkOrder.Remove(MalfunctionWorkOrder);
            await _context.SaveChangesAsync();

            return RedirectToPage("../WorkOrders/Details", new { id = MalfunctionWorkOrder.ID });
        }

        public async Task<IActionResult> OnPutDeleteAsync(int id)
        {
            MalfunctionWorkOrder = await _context.MalfunctionWorkOrder
                                        .Include(m => m.Instrument)
                                        .FirstOrDefaultAsync(m => m.ID == id);

            if (MalfunctionWorkOrder == null)
            {
                return new JsonResult("未找到该记录");
            }

            var isAuthorized = await _authorizationService.AuthorizeAsync(User, MalfunctionWorkOrder, Operations.Delete);

            if (!isAuthorized.Succeeded)
            {
                return new JsonResult("权限不足");
            }

            try
            {
                // 改变故障设备的状态
                MalfunctionWorkOrder.Instrument = await _context.Set<Instrument>().FindAsync(MalfunctionWorkOrder.InstrumentID);
                MalfunctionWorkOrder.Instrument.Status = InstrumentStatus.Using;

                _context.MalfunctionWorkOrder.Remove(MalfunctionWorkOrder);
                await _context.SaveChangesAsync();
                return new JsonResult("删除成功！");
            }
            catch (Exception ex)
            {
                return new JsonResult($"删除失败，错误信息：{ex}");
            }

        }
    }
}
