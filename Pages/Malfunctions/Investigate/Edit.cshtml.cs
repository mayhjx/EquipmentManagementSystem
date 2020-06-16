using EquipmentManagementSystem.Authorization;
using EquipmentManagementSystem.Data;
using EquipmentManagementSystem.Models;
using EquipmentManagementSystem.Pages.Instruments;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace EquipmentManagementSystem.Pages.Malfunctions.Investigate
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
        public Investigation Investigation { get; set; }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            Investigation = await _context.Investigation
                                .Include(m => m.MalfunctionWorkOrder)
                                .ThenInclude(m => m.Instrument)
                                .FirstOrDefaultAsync(m => m.ID == id);

            if (Investigation == null)
            {
                return NotFound();
            }

            // 如果工单已完成则跳转到工单详情页
            if (Investigation.MalfunctionWorkOrder.Progress == WorkOrderProgress.Completed)
            {
                return RedirectToPage("../WorkOrders/Details", new { id = Investigation.MalfunctionWorkOrderID });
            }

            var isAuthorized = await _authorizationService.AuthorizeAsync(User, Investigation.MalfunctionWorkOrder, Operations.Update);

            if (!isAuthorized.Succeeded)
            {
                return Forbid();
            }

            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync(int id)
        {
            Investigation = await _context.Investigation
                                .Include(m => m.MalfunctionWorkOrder)
                                .ThenInclude(m => m.Instrument)
                                .FirstOrDefaultAsync(m => m.ID == id);

            if (Investigation == null)
            {
                return NotFound();
            }

            var isAuthorized = await _authorizationService.AuthorizeAsync(User, Investigation.MalfunctionWorkOrder, Operations.Update);

            if (!isAuthorized.Succeeded)
            {
                return Forbid();
            }

            if (await TryUpdateModelAsync<Investigation>(
                    Investigation,
                    "Investigation",
                    i => i.BeginTime, i => i.EndTime, i => i.Operator, i => i.Measures))
            {
                // 如果进度在排查中之前则更新已排查
                if (Investigation.MalfunctionWorkOrder.Progress < WorkOrderProgress.Investigated)
                {
                    Investigation.MalfunctionWorkOrder.Progress = WorkOrderProgress.Investigated;
                }

                await _context.SaveChangesAsync();
                return RedirectToPage("../WorkOrders/Details", new { id = Investigation.MalfunctionWorkOrderID });
            }

            return Page();
        }
    }
}
