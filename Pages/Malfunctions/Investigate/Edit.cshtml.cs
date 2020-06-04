using EquipmentManagementSystem.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace EquipmentManagementSystem.Pages.Malfunctions.Investigate
{
    public class EditModel : PageModel
    {
        private readonly EquipmentManagementSystem.Data.MalfunctionContext _context;

        public EditModel(EquipmentManagementSystem.Data.MalfunctionContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Investigation Investigation { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Investigation = await _context.Investigation
                            .FirstOrDefaultAsync(m => m.ID == id);

            if (Investigation == null)
            {
                return NotFound();
            }
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Investigation = await _context.Investigation
                                .Include(m => m.MalfunctionWorkOrder)
                                .FirstAsync(m => m.ID == id);

            if (Investigation == null)
            {
                return NotFound();
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
