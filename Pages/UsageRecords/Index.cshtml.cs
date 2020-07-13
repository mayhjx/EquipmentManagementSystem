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
                              .Include(record => record.Instrument)
                              select record;

            var isAuthorized = User.IsInRole(Constants.DirectorRole) ||
                                 User.IsInRole(Constants.ManagerRole);


            if (User.Identity.IsAuthenticated)
            {
                var currentUserGroup = (await _userManager.GetUserAsync(User)).Group;
                var projectsOfcurrentUserGroup = await _context.Projects
                                                                .AsNoTracking()
                                                                .Include(p => p.Group)
                                                                .Where(p => p.Group.Name == currentUserGroup)
                                                                .Select(p => p.Name)
                                                                .ToListAsync();

                if (!isAuthorized)
                {
                    // 显示当前用户所属项目组的使用登记s
                    usageRecord = from record in usageRecord
                                  where projectsOfcurrentUserGroup.Contains(record.ProjectName)
                                  select record;
                }
            }

            UsageRecord = await usageRecord.OrderByDescending(m => m.BeginTimeOfTest).ToListAsync();
        }
    }
}
