using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using EquipmentManagementSystem.Data;
using EquipmentManagementSystem.Models;

namespace EquipmentManagementSystem.Pages.MalfunctionInfos
{
    public class DetailsModel : PageModel
    {
        private readonly MalfunctionContext _context;

        public DetailsModel(MalfunctionContext context)
        {
            _context = context;
        }

        public MalfunctionInfo MalfunctionInfo { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            MalfunctionInfo = await _context.MalfunctionInfo.FirstOrDefaultAsync(m => m.ID == id);

            if (MalfunctionInfo == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
