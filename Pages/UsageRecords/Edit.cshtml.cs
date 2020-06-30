using EquipmentManagementSystem.Data;
using EquipmentManagementSystem.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
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
            UsageRecord = await _context.UsageRecords.FindAsync(id);

            if (UsageRecord == null)
            {
                return NotFound();
            }

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

            var UsageRecordToUpdate = await _context.UsageRecords.FindAsync(id);

            if (UsageRecordToUpdate == null)
            {
                return NotFound();
            }

            if (await TryUpdateModelAsync<UsageRecord>(
                UsageRecordToUpdate,
                "UsageRecord",
                i => i.BeginTimeOfMaintain, i => i.BeginTimeOfTest,
                i => i.ColumnNumber, i => i.ColumnPressure, i => i.PressureUnit,
                i => i.ColumnTwoNumber, i => i.ColumnTwoPressure,
                i => i.SampleNumber, i => i.TestNumber, i => i.EndTime, i => i.Remark))
            {
                await _context.SaveChangesAsync();
                return RedirectToPage("./Index");
            }
            return Page();
        }
    }
}
