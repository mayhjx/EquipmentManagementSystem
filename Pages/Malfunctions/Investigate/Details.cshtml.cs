using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using EquipmentManagementSystem.Data;
using EquipmentManagementSystem.Models;

namespace EquipmentManagementSystem.Pages.Malfunctions.Investigate
{
    public class DetailsModel : PageModel
    {
        private readonly EquipmentManagementSystem.Data.MalfunctionContext _context;

        public DetailsModel(EquipmentManagementSystem.Data.MalfunctionContext context)
        {
            _context = context;
        }

        public Investigation Investigation { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Investigation = await _context.Investigation.FirstOrDefaultAsync(m => m.ID == id);

            if (Investigation == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
