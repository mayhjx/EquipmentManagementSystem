using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using EquipmentManagementSystem.Data;
using EquipmentManagementSystem.Models;

namespace EquipmentManagementSystem.Pages.Malfunctions.RepairRequests
{
    public class DeleteModel : PageModel
    {
        private readonly EquipmentManagementSystem.Data.MalfunctionContext _context;

        public DeleteModel(EquipmentManagementSystem.Data.MalfunctionContext context)
        {
            _context = context;
        }

        [BindProperty]
        public RepairRequest RepairRequest { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            RepairRequest = await _context.RepairRequest.FirstOrDefaultAsync(m => m.ID == id);

            if (RepairRequest == null)
            {
                return NotFound();
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            RepairRequest = await _context.RepairRequest.FindAsync(id);

            if (RepairRequest != null)
            {
                _context.RepairRequest.Remove(RepairRequest);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
