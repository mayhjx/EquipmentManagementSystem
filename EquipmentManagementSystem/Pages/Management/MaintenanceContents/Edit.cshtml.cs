using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EquipmentManagementSystem.Data;
using EquipmentManagementSystem.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace EquipmentManagementSystem.Pages.Management.MaintenanceContents
{
    public class EditModel : PageModel
    {
        private readonly EquipmentContext _context;

        public EditModel(EquipmentContext context)
        {
            _context = context;
        }

        [BindProperty]
        public MaintenanceContent MaintenanceContent { get; set; }

        public SelectList Platforms { get; set; }

        public List<SelectListItem> Types { get; } = new List<SelectListItem>
        {
            new SelectListItem { Value = "日常维护", Text = "日常维护" },
            new SelectListItem { Value = "周维护", Text = "周维护" },
            new SelectListItem { Value = "月度维护", Text = "月度维护"  },
            new SelectListItem { Value = "季度维护", Text = "季度维护"  },
            new SelectListItem { Value = "年度PM维护", Text = "年度PM维护"  },
            new SelectListItem { Value = "临时维护", Text = "临时维护"  },
        };

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            MaintenanceContent = await _context.MaintenanceContents
                .FirstOrDefaultAsync(m => m.Id == id);

            Platforms = new SelectList((from i in _context.Instruments
                                        select i.Platform).Distinct());

            if (MaintenanceContent == null)
            {
                return NotFound();
            }
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

            _context.Attach(MaintenanceContent).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MaintenanceContentExists(MaintenanceContent.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            Platforms = new SelectList((from i in _context.Instruments
                                        select i.Platform).Distinct());
            return RedirectToPage("./Index");
        }

        private bool MaintenanceContentExists(int id)
        {
            return _context.MaintenanceContents.Any(e => e.Id == id);
        }
    }
}
