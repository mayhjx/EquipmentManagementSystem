using EquipmentManagementSystem.Data;
using EquipmentManagementSystem.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
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
                                    .FindAsync(id);

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
        public async Task<IActionResult> OnPostAsync(int id)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var UsageRecordToUpdate = await _context.UsageRecords
                                    .FindAsync(id);

            if (UsageRecordToUpdate == null)
            {
                return NotFound();
            }

            if (await TryUpdateModelAsync<UsageRecord>(
                UsageRecordToUpdate,
                "UsageRecord",
                i => i.InstrumentId, i => i.ProjectName, i => i.BeginTimeOfMaintain,
                i => i.ColumnPressure, i => i.BeginTimeOfTest, i => i.SampleNumber,
                i => i.TestNumber, i => i.EndTime, i => i.Creator))
            {
                await _context.SaveChangesAsync();
                return RedirectToPage("./Index");
            }
            return Page();
        }
    }
}
