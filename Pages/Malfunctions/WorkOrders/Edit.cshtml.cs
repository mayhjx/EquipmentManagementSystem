﻿using EquipmentManagementSystem.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace EquipmentManagementSystem.Pages.Malfunctions.WorkOrders
{
    public class EditModel : PageModel
    {
        private readonly EquipmentManagementSystem.Data.MalfunctionContext _context;

        public EditModel(EquipmentManagementSystem.Data.MalfunctionContext context)
        {
            _context = context;
        }

        [BindProperty]
        public MalfunctionWorkOrder MalfunctionWorkOrder { get; set; }

        [BindProperty]
        public MalfunctionInfo MalfunctionInfo { get; set; }


        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            MalfunctionWorkOrder = await _context.MalfunctionWorkOrder
                                    .FirstOrDefaultAsync(m => m.ID == id);

            if (MalfunctionWorkOrder == null)
            {
                return NotFound();
            }

            //return RedirectToPage("Edit");
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            MalfunctionWorkOrder = await _context.MalfunctionWorkOrder
                                .Include(m => m.Instrument)
                                .FirstAsync(m => m.ID == id);

            if (MalfunctionWorkOrder == null)
            {
                return NotFound();
            }

            if (await TryUpdateModelAsync<MalfunctionWorkOrder>(
                    MalfunctionWorkOrder,
                    "MalfunctionWorkOrder",
                    i => i.InstrumentID, i => i.Progress, i => i.CreatedTime, i => i.Creator))
            {
                if (MalfunctionWorkOrder.Progress == WorkOrderProgress.Validated)
                    MalfunctionWorkOrder.Progress = WorkOrderProgress.Completed;

                if (MalfunctionWorkOrder.Instrument.Status == InstrumentStatus.Malfunction)
                    MalfunctionWorkOrder.Instrument.Status = InstrumentStatus.Using;

                await _context.SaveChangesAsync();
                return RedirectToPage("../WorkOrders/Index");
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

        //    _context.Attach(MalfunctionWorkOrder).State = EntityState.Modified;

        //    try
        //    {
        //        await _context.SaveChangesAsync();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!MalfunctionWorkOrderExists(MalfunctionWorkOrder.ID))
        //        {
        //            return NotFound();
        //        }
        //        else
        //        {
        //            throw;
        //        }
        //    }

        //    return RedirectToPage("./Index");
        //}

        //private bool MalfunctionWorkOrderExists(int id)
        //{
        //    return _context.MalfunctionWorkOrder.Any(e => e.ID == id);
        //}
    }
}
