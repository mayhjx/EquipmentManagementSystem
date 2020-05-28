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
                // 如果进度在已保修之前则更新已报修
                if (AccessoriesOrder.MalfunctionWorkOrder.Progress < WorkOrderProgress.Waiting)
                {
                    AccessoriesOrder.MalfunctionWorkOrder.Progress = WorkOrderProgress.Waiting;
                }
                await _context.SaveChangesAsync();
                return RedirectToPage("../WorkOrders/Details", new { id = AccessoriesOrder.MalfunctionWorkOrderID });
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

        //    _context.Attach(AccessoriesOrder).State = EntityState.Modified;

        //    try
        //    {
        //        await _context.SaveChangesAsync();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!AccessoriesOrderExists(AccessoriesOrder.ID))
        //        {
        //            return NotFound();
        //        }
        //        else
        //        {
        //            throw;
        //        }
        //    }
        //    return RedirectToPage("../WorkOrders/Details", new { id = AccessoriesOrder.MalfunctionWorkOrderID });

        //    //return RedirectToPage("./Index");
        //}

        //private bool AccessoriesOrderExists(int id)
        //{
        //    return _context.AccessoriesOrder.Any(e => e.ID == id);
        //}
    }
}
