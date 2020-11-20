using EquipmentManagementSystem.Data;
using EquipmentManagementSystem.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EquipmentManagementSystem.Pages.Malfunctions.WorkOrders
{
    [AllowAnonymous]
    public class DoneModel : PageModel
    {
        private readonly MalfunctionContext _context;

        public DoneModel(MalfunctionContext context)
        {
            _context = context;
        }

        public IList<MalfunctionWorkOrder> MalfunctionWorkOrder { get; set; }

        public async Task OnGetAsync()
        {
            MalfunctionWorkOrder = await _context.MalfunctionWorkOrder
                                    .AsNoTracking()
                                    .Include(m => m.MalfunctionInfo)
                                    .Where(m => m.Progress == WorkOrderProgress.Completed)
                                    .ToListAsync();
        }
    }
}
