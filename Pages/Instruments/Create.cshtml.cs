﻿using EquipmentManagementSystem.Data;
using EquipmentManagementSystem.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Threading.Tasks;

namespace EquipmentManagementSystem.Pages.Instruments
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
        public Instrument Instrument { get; set; }

        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Instruments.Add(Instrument);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
