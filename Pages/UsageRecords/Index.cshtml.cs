using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EquipmentManagementSystem.Authorization;
using EquipmentManagementSystem.Data;
using EquipmentManagementSystem.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace EquipmentManagementSystem.Pages.UsageRecords
{
    [AllowAnonymous]
    public class IndexModel : BasePageModel
    {

        public IndexModel(EquipmentContext context,
            UserManager<User> userManager,
            IAuthorizationService authorizationService)
            : base(context, userManager, authorizationService)
        {
        }

        public IList<UsageRecord> UsageRecord { get; set; }

        public async Task OnGetAsync()
        {
            var usageRecord = from record in _context.UsageRecords
                              .AsNoTracking()
                              .Include(i => i.Instrument)
                              select record;

            var isAuthorized = User.IsInRole(Constants.DirectorRole) ||
                                 User.IsInRole(Constants.ManagerRole);


            if (User.Identity.IsAuthenticated)
            {
                var currentUserGroup = (await _userManager.GetUserAsync(User)).Group;
                if (!isAuthorized)
                {
                    // 显示当前用户所属项目组的使用登记
                    usageRecord = usageRecord.Where(record => record.Instrument.Group == currentUserGroup);
                }
            }

            UsageRecord = await usageRecord.OrderByDescending(m => m.BeginTimeOfTest).ToListAsync();
        }
    }
}
