using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using EquipmentManagementSystem.Data;
using EquipmentManagementSystem.Models;

namespace EquipmentManagementSystem.Pages.Components
{
    public class DetailsModel : PageModel
    {
        private readonly EquipmentManagementSystem.Data.EquipmentContext _context;

        public DetailsModel(EquipmentManagementSystem.Data.EquipmentContext context)
        {
            _context = context;
        }

        public Component Component { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Component = await _context.components
                .Include(c => c.instrument).FirstOrDefaultAsync(m => m.ID == id);

            if (Component == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
