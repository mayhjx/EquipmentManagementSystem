using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EquipmentManagementSystem.Data;
using EquipmentManagementSystem.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace EquipmentManagementSystem.Pages.Equipments.Instruments
{
    [AllowAnonymous]
    public class IndexModel : PageModel
    {
        private readonly EquipmentContext _context;

        public IndexModel(EquipmentContext context)
        {
            _context = context;
        }

        public IList<Instrument> Instruments { get; set; }

        // 用来在Index Page确认新建权限
        public Instrument Instrument { get; } = new Instrument();

        public async Task OnGetAsync()
        {
            Instruments = await _context.Instruments.OrderBy(m => m.ID)
                                                    .AsNoTracking()
                                                    .Include(m => m.Calibrations)
                                                    .ToListAsync();
        }
    }
}
