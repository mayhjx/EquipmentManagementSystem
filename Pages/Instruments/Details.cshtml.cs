using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using EquipmentManagementSystem.Data;
using EquipmentManagementSystem.Models;

namespace EquipmentManagementSystem.Pages.Instruments
{
    public class DetailsModel : PageModel
    {
        private readonly EquipmentManagementSystem.Data.EquipmentContext _context;

        public DetailsModel(EquipmentManagementSystem.Data.EquipmentContext context)
        {
            _context = context;
        }

        public Instrument Instrument { get; set; }

        public async Task<IActionResult> OnGetAsync(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            // Instrument = await _context.Instruments.FirstOrDefaultAsync(m => m.ID == id);
            Instrument = await _context.Instruments
                        .Include(a => a.assert)
                        .Include(c => c.calibrations)
                        .Include(c => c.components)
                        .AsNoTracking()
                        .FirstOrDefaultAsync(m => m.ID == id);
                            

            if (Instrument == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
