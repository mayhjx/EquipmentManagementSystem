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
                            .Include(m => m.MalfunctionWorkOrder)
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
                // 如果进度在已保修之前则更新已报修
                if (RepairRequest.MalfunctionWorkOrder.Progress < WorkOrderProgress.RepairRequested)
                {
                    RepairRequest.MalfunctionWorkOrder.Progress = WorkOrderProgress.RepairRequested;
                }
                await _context.SaveChangesAsync();
                return RedirectToPage("../WorkOrders/Details", new { id = RepairRequest.MalfunctionWorkOrderID });
            }

            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://aka.ms/RazorPagesCRUD.
        //public async Task<IActionResult> OnPostAsync()
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return Page();
        //    }

        //    _context.Attach(RepairRequest).State = EntityState.Modified;
        //    _context.Attach(RepairRequest.MalfunctionWorkOrder).State = EntityState.Modified;

        //    try
        //    {
        //        await _context.SaveChangesAsync();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!RepairRequestExists(RepairRequest.ID))
        //        {
        //            return NotFound();
        //        }
        //        else
        //        {
        //            throw;
        //        }
        //    }

        //    return RedirectToPage("../WorkOrders/Details", new { id = RepairRequest.MalfunctionWorkOrderID });
        //    //return RedirectToPage("./Index");
        //}

        //private bool RepairRequestExists(int id)
        //{
        //    return _context.RepairRequest.Any(e => e.ID == id);
        //}

    }
}
