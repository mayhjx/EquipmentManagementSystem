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
        private readonly EquipmentManagementSystem.Data.EquipmentContext _context;

        public IndexModel(EquipmentManagementSystem.Data.EquipmentContext context)
        {
            _context = context;
        }

        public IList<Instrument> Instrument { get;set; }

        [BindProperty(SupportsGet=true)]
        public string SearchString { get; set; }

        public async Task OnGetAsync()
        {
            // Instrument = await _context.Instruments.OrderBy(n => n.ID).ToListAsync();
            var instruments = from i in _context.Instruments
                            select i;
            if (!string.IsNullOrEmpty(SearchString))
            {
                instruments = instruments.Where(s => s.ID.Contains(SearchString));
            }
            Instrument = await instruments.OrderBy(m => m.ID).ToListAsync();
        }
    }
}
