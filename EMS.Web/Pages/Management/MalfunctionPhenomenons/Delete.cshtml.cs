using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using EquipmentManagementSystem.Data;
using EquipmentManagementSystem.Models;

namespace EquipmentManagementSystem.Pages.Management.MalfunctionPhenomenons
{
    public class DeleteModel : PageModel
    {
        private readonly EquipmentManagementSystem.Data.MalfunctionContext _context;

        public DeleteModel(EquipmentManagementSystem.Data.MalfunctionContext context)
        {
            _context = context;
        }

        [BindProperty]
        public MalfunctionPhenomenon MalfunctionPhenomenon { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            MalfunctionPhenomenon = await _context.MalfunctionPhenomenon.FirstOrDefaultAsync(m => m.ID == id);

            if (MalfunctionPhenomenon == null)
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

            MalfunctionPhenomenon = await _context.MalfunctionPhenomenon.FindAsync(id);

            if (MalfunctionPhenomenon != null)
            {
                _context.MalfunctionPhenomenon.Remove(MalfunctionPhenomenon);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
