﻿using EquipmentManagementSystem.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace EquipmentManagementSystem.Pages.Malfunctions.Validate
{
    public class EditModel : PageModel
    {
        private readonly EquipmentManagementSystem.Data.MalfunctionContext _context;

        public EditModel(EquipmentManagementSystem.Data.MalfunctionContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Validation Validation { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Validation = await _context.Validation.FirstOrDefaultAsync(m => m.ID == id);

            if (Validation == null)
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

            Validation = await _context.Validation
                                .Include(m => m.MalfunctionWorkOrder)
                                .FirstAsync(m => m.ID == id);

            if (Validation == null)
            {
                return NotFound();
            }

            if (await TryUpdateModelAsync<Validation>(
                    Validation,
                    "Validation",
                    i => i.EndTime, i => i.PerformanceReportFile, i => i.EffectReportFile, i => i.IsConfirm, i => i.Summary, i => i.Attachment))
            {
                // 如果进度在已保修之前则更新已报修，设备状态更新为正常
                if (Validation.MalfunctionWorkOrder.Progress < WorkOrderProgress.Validated)
                {
                    Validation.MalfunctionWorkOrder.Progress = WorkOrderProgress.Validated;
                }
                await _context.SaveChangesAsync();
                return RedirectToPage("../WorkOrders/Details", new { id = Validation.MalfunctionWorkOrderID });
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

        //    _context.Attach(Validation).State = EntityState.Modified;

        //    try
        //    {
        //        await _context.SaveChangesAsync();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!ValidationExists(Validation.ID))
        //        {
        //            return NotFound();
        //        }
        //        else
        //        {
        //            throw;
        //        }
        //    }

        //    return RedirectToPage("../WorkOrders/Details", new { id = Validation.MalfunctionWorkOrderID });
        //    //return RedirectToPage("./Index");
        //}

        //private bool ValidationExists(int id)
        //{
        //    return _context.Validation.Any(e => e.ID == id);
        //}
    }
}
