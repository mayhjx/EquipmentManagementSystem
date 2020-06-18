﻿using EquipmentManagementSystem.Data;
using EquipmentManagementSystem.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace EquipmentManagementSystem.Pages.UsageRecords
{
    [AllowAnonymous]
    public class EditModel : PageModel
    {
        private readonly EquipmentContext _context;

        public EditModel(EquipmentContext context)
        {
            _context = context;
        }

        [BindProperty]
        public UsageRecord UsageRecord { get; set; }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            UsageRecord = await _context.UsageRecords
                                    //.Include(u => u.Instrument)
                                    .FirstOrDefaultAsync(m => m.Id == id);

            if (UsageRecord == null)
            {
                return NotFound();
            }

            ViewData["InstrumentId"] = new SelectList(_context.Instruments, "ID", "ID", UsageRecord.InstrumentId);
            ViewData["Project"] = new SelectList(_context.Projects, "Name", "Name", UsageRecord.ProjectName);

            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(UsageRecord).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UsageRecordExists(UsageRecord.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool UsageRecordExists(int id)
        {
            return _context.UsageRecords.Any(e => e.Id == id);
        }
    }
}
