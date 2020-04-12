using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using EquipmentManagementSystem.Data;
using EquipmentManagementSystem.Models;

namespace EquipmentManagementSystem.Pages.Asserts
{
    public class CreateModel : PageModel
    {
        private readonly EquipmentManagementSystem.Data.EquipmentContext _context;

        public CreateModel(EquipmentManagementSystem.Data.EquipmentContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
        ViewData["instrumentId"] = new SelectList(_context.Instruments, "ID", "ID");
            return Page();
        }

        [BindProperty]
        public Assert Assert { get; set; }

        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.asserts.Add(Assert);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
