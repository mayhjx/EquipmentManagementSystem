using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EquipmentManagementSystem.Data;
using EquipmentManagementSystem.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace EquipmentManagementSystem.Pages.Management.MaintenanceTypes
{
    public class EditModel : PageModel
    {
        private readonly EquipmentContext _context;

        public EditModel(EquipmentContext context)
        {
            _context = context;
        }

        [BindProperty]
        public MaintenanceType MaintenanceType { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            MaintenanceType = await _context.MaintenanceTypes
                .Include(m => m.Content)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (MaintenanceType == null)
            {
                return NotFound();
            }
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync(int id, List<string> content)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var MaintenanceType = await _context.MaintenanceTypes.Include(m => m.Content).FirstOrDefaultAsync(m => m.Id == id);

            try
            {
                MaintenanceType.Content.Clear();
                for (int i = 0; i < content.Count; i++)
                {
                    if (content[i] == null)
                        continue;
                    var maintenanceContent = new MaintenanceContent
                    {
                        MaintenanceType = MaintenanceType,
                        Text = content[i]
                    };
                    _context.MaintenanceContents.Add(maintenanceContent);
                }

                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MaintenanceTypeExists(MaintenanceType.Id))
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

        private bool MaintenanceTypeExists(int id)
        {
            return _context.MaintenanceTypes.Any(e => e.Id == id);
        }
    }
}
