using EquipmentManagementSystem.Authorization;
using EquipmentManagementSystem.Data;
using EquipmentManagementSystem.Models;
using EquipmentManagementSystem.Models.Record;
using EquipmentManagementSystem.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EquipmentManagementSystem.Pages.Records
{
    public class IndexModel : BasePageModel
    {
        private readonly IUsageRecordService _usageRecordService;
        private readonly IMaintenanceRecordService _maintenanceRecordService;
        private readonly IUserResolverService _userResolverService;
        private readonly IAuditTrailService _auditTrailService;

        public IndexModel(EquipmentContext context,
            UserManager<User> userManager,
            IAuthorizationService authorizationService,
            IUsageRecordService usageRecordService,
            IMaintenanceRecordService maintenanceRecordService,
            IUserResolverService userResolverService,
            IAuditTrailService auditTrailService)
            : base(context, userManager, authorizationService)
        {
            _usageRecordService = usageRecordService;
            _maintenanceRecordService = maintenanceRecordService;
            _userResolverService = userResolverService;
            _auditTrailService = auditTrailService;
        }

        public IList<MaintenanceRecord> MaintenanceRecords { get; set; }
        public IList<UsageRecord> UsageRecords { get; set; }
        public IList<AuditTrailLog> MaintenanceRecordAuditTrailLogs { get; set; }
        public IList<AuditTrailLog> UsageRecordAuditTrailLogs { get; set; }

        public async Task OnGetAsync()
        {
            var isAuthorized = User.IsInRole(Constants.DirectorRole) || User.IsInRole(Constants.ManagerRole);

            if (isAuthorized)
            {
                // 用户为管理员，显示所有记录
                UsageRecords = await _usageRecordService.ListAllAsync();
                MaintenanceRecords = await _maintenanceRecordService.ListAllAsync();
            }
            else
            {
                //  用户非管理员，显示用户所属项目组的使用登记
                var groupOfUserGroup = _userResolverService.GetUserGroup();
                UsageRecords = await _usageRecordService.ListAllByGroupAsync(groupOfUserGroup);
                MaintenanceRecords = await _maintenanceRecordService.ListAllByGroupAsync(groupOfUserGroup);
            }

            MaintenanceRecordAuditTrailLogs = await _auditTrailService.GetAuditTrailLogsAsync(new MaintenanceRecord().GetType().Name);
            UsageRecordAuditTrailLogs = await _auditTrailService.GetAuditTrailLogsAsync(new UsageRecord().GetType().Name);
        }
    }
}
