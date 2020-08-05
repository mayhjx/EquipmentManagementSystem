using System.Collections.Generic;
using System.Threading.Tasks;
using EquipmentManagementSystem.Data;
using EquipmentManagementSystem.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace EquipmentManagementSystem.Pages.Management.MaintenanceTypes
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
            return Page();
        }

        [BindProperty]
        public MaintenanceType MaintenanceType { get; set; }

        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync(List<string> content)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.MaintenanceTypes.Add(MaintenanceType);

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

            return RedirectToPage("./Index");
        }
    }
}
