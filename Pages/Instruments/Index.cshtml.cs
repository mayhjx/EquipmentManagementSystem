using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using EquipmentManagementSystem.Data;
using EquipmentManagementSystem.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace EquipmentManagementSystem.Pages.Instruments
{
    public class IndexModel : PageModel
    {
        private readonly EquipmentContext _context;

        public IndexModel(EquipmentContext context)
        {
            _context = context;
        }

        public IList<Instrument> Instrument { get;set; }

        public async Task OnGetAsync()
        {
            //Instrument = await _context.Instruments.OrderBy(n => n.ID).ToListAsync();
            var instruments = from i in _context.Instruments
                              .Include(i => i.Calibrations)
                              select i;

            Instrument = await instruments.OrderBy(m => m.ID).ToListAsync();
        }
    }
}
