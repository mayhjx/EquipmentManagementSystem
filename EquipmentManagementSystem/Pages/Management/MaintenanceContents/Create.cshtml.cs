using EquipmentManagementSystem.Data;
using EquipmentManagementSystem.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EquipmentManagementSystem.Pages.Management.MaintenanceContents
{
    public class CreateModel : PageModel
    {
        private readonly EquipmentContext _context;

        public CreateModel(EquipmentContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            Platforms = new SelectList((from i in _context.Instruments
                                        select i.Platform).Distinct());
            return Page();
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
            new SelectListItem { Value = "年度维护", Text = "年度维护"  },
            new SelectListItem { Value = "临时维护", Text = "临时维护"  },
        };

        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.MaintenanceContents.Add(MaintenanceContent);

            await _context.SaveChangesAsync();

            Platforms = new SelectList((from i in _context.Instruments
                                        select i.Platform).Distinct());

            return RedirectToPage("./Index");
        }
    }
}
