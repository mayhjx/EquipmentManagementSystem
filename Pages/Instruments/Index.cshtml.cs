using EquipmentManagementSystem.Data;
using EquipmentManagementSystem.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EquipmentManagementSystem.Pages.Instruments
{
    [AllowAnonymous]
    public class IndexModel : PageModel
    {
        private readonly EquipmentContext _context;

        public IndexModel(EquipmentContext context)
        {
            _context = context;
        }

        public IList<Instrument> Instrument { get; set; }

        public async Task OnGetAsync()
        {
            Instrument = await _context.Instruments.OrderBy(m => m.ID)
                                                    .AsNoTracking()
                                                    .Include(m => m.Calibrations)
                                                    .ToListAsync();
        }
    }
}
