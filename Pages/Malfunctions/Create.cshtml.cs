﻿using EquipmentManagementSystem.Data;
using EquipmentManagementSystem.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Threading.Tasks;

namespace EquipmentManagementSystem.Pages.Malfunctions
{
    public class CreateModel : PageModel
    {
        private readonly EquipmentContext _context;

        public CreateModel(EquipmentContext context)
        {
            _context = context;
        }

        public SelectList InstrumentOptions { get; set; }
        public SelectList ComponentOptions { get; set; }


        public IActionResult OnGet()
        {
            InstrumentOptions = new SelectList(_context.Instruments, "ID", "ID");
            ComponentOptions = new SelectList(_context.components, "ID", "Name");
            return Page();
        }

        [BindProperty]
        public Malfunction Malfunction { get; set; }


        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Malfunction.Add(Malfunction);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
