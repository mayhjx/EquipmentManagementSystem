using System.Collections.Generic;
using System.Threading.Tasks;
using EquipmentManagementSystem.Data;
using EquipmentManagementSystem.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace EquipmentManagementSystem.Pages.Equipments.Acceptance
{
    public class IndexModel : PageModel
    {
        private readonly EquipmentContext _context;

        public IndexModel(EquipmentContext context)
        {
            _context = context;
        }

        public IList<InstrumentAcceptance> InstrumentAcceptance { get; set; }

        public async Task OnGetAsync()
        {
            InstrumentAcceptance = await _context.InstrumentAcceptances
                                            .AsNoTracking()
                                            .ToListAsync();
        }
    }
}
