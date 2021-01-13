using EquipmentManagementSystem.Data;
using EquipmentManagementSystem.Interfaces;
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
        private readonly EquipmentContext _context;
        private readonly IUserResolverService _userResolverService;

        public DoneModel(EquipmentContext context, IUserResolverService userResolverService)
        {
            _context = context;
            _userResolverService = userResolverService;
        }

        public IList<MalfunctionWorkOrder> MalfunctionWorkOrder { get; set; }

        public async Task OnGetAsync()
        {
            var userGroup = _userResolverService.GetUserGroup();

            if (!string.IsNullOrEmpty(userGroup))
            {
                MalfunctionWorkOrder = await _context.MalfunctionWorkOrder
                                    .AsNoTracking()
                                    .Include(m => m.Instrument)
                                    .Where(m => m.Instrument.Group == userGroup)
                                    .Include(m => m.MalfunctionInfo)
                                    .Where(m => m.Progress == WorkOrderProgress.Completed)
                                    .ToListAsync();
            }
            else
            {
                MalfunctionWorkOrder = await _context.MalfunctionWorkOrder
                                    .AsNoTracking()
                                    .Include(m => m.Instrument)
                                    .Include(m => m.MalfunctionInfo)
                                    .Where(m => m.Progress == WorkOrderProgress.Completed)
                                    .ToListAsync();
            }
        }
    }
}
