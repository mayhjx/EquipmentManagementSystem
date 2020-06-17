﻿using EquipmentManagementSystem.Data;
using EquipmentManagementSystem.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Threading.Tasks;

namespace EquipmentManagementSystem.Pages.Components
{
    [Authorize(Roles = "设备管理员, 设备主任")]
    public class CreateModel : PageModel
    {
        private readonly EquipmentContext _context;

        public CreateModel(EquipmentContext context)
        {
            _context = context;
        }

        public IActionResult OnGet(string id)
        {
            ViewData["InstrumentID"] = new SelectList(_context.Instruments, "ID", "ID", id);
            return Page();
        }

        [BindProperty]
        public Component Component { get; set; }

        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Components.Add(Component);
            await _context.SaveChangesAsync();

            return RedirectToPage("../Instruments/Details", new { id = Component.InstrumentID });
        }
    }
}
