using EquipmentManagementSystem.Authorization;
using EquipmentManagementSystem.Interfaces;
using EquipmentManagementSystem.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EquipmentManagementSystem.Pages.MaintenanceRecords
{
    public class EditModel : PageModel
    {
        private readonly IAuditTrailRepository _auditTrailRepository;
        private readonly IUserResolverService _userResolverService;
        private readonly IMaintenanceRecordRepository _maintenanceRecordRepository;
        private readonly IAuthorizationService _authorizationService;

        public EditModel(IAuditTrailRepository auditTrailRepository,
            IUserResolverService userResolverService,
            IMaintenanceRecordRepository maintenanceRecordRepository,
            IAuthorizationService authorizationService)
        {
            _auditTrailRepository = auditTrailRepository;
            _userResolverService = userResolverService;
            _maintenanceRecordRepository = maintenanceRecordRepository;
            _authorizationService = authorizationService;
        }

        [BindProperty]
        public MaintenanceRecord MaintenanceRecord { get; set; }
        public IList<AuditTrailLog> AuditTrailLogs { get; set; }

        [BindProperty]
        public string[] DailyMaintenanceContent { get; set; }
        [BindProperty]
        public string[] WeeklyMaintenanceContent { get; set; }
        [BindProperty]
        public string[] MonthlyMaintenanceContent { get; set; }
        [BindProperty]
        public string[] QuarterlyMaintenanceContent { get; set; }
        [BindProperty]
        public string[] YearlyMaintenanceContent { get; set; }
        [BindProperty]
        public string[] TemporaryMaintenanceContent { get; set; }
        [BindProperty]
        public string OtherMaintenanceContent { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            MaintenanceRecord = await _maintenanceRecordRepository.GetById(id);

            if (MaintenanceRecord == null)
            {
                return NotFound();
            }

            var isAuthorized = await _authorizationService.AuthorizeAsync(User, MaintenanceRecord, Operations.Update);

            if (!isAuthorized.Succeeded)
            {
                return Forbid();
            }

            AuditTrailLogs = await _auditTrailRepository.GetAuditTrailLogs(new MaintenanceRecord().GetType().Name, id);

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                return Page();
            }

            MaintenanceRecord = await _maintenanceRecordRepository.GetById(id);

            if (MaintenanceRecord == null)
            {
                return NotFound();
            }

            var isAuthorized = await _authorizationService.AuthorizeAsync(User, MaintenanceRecord, Operations.Update);

            if (!isAuthorized.Succeeded)
            {
                return Forbid();
            }

            if (await TryUpdateModelAsync(MaintenanceRecord, "MaintenanceRecord"))
            {
                MaintenanceRecord.GroupName = _userResolverService.GetUserGroup();
                MaintenanceRecord.SetDaily(DailyMaintenanceContent);
                MaintenanceRecord.SetWeekly(WeeklyMaintenanceContent);
                MaintenanceRecord.SetMonthly(MonthlyMaintenanceContent);
                MaintenanceRecord.SetQuarterly(QuarterlyMaintenanceContent);
                MaintenanceRecord.SetYearly(YearlyMaintenanceContent);
                MaintenanceRecord.SetTemporary(TemporaryMaintenanceContent);
                MaintenanceRecord.SetOther(OtherMaintenanceContent);

                await _maintenanceRecordRepository.Update(MaintenanceRecord);

                return RedirectToPage("../Index", new { instrumentId = MaintenanceRecord.InstrumentId, date = MaintenanceRecord.BeginTime });
            }

            return Page();
        }
    }
}
