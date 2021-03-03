using EquipmentManagementSystem.Authorization;
using EquipmentManagementSystem.Interfaces;
using EquipmentManagementSystem.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EquipmentManagementSystem.Pages.MaintenanceRecordsOfYuanSu
{
    public class EditModel : PageModel
    {
        private readonly IInstrumentRepository _instrumentRepository;
        private readonly IAuditTrailRepository _auditTrailRepository;
        private readonly IMaintenanceRecordOfYuanSuRepository _maintenanceRecordOfYuanSuRepository;
        private readonly IAuthorizationService _authorizationService;

        public EditModel(IInstrumentRepository instrumentRepository,
            IAuditTrailRepository auditTrailRepository,
            IMaintenanceRecordOfYuanSuRepository maintenanceRecordOfYuanSuRepository,
            IAuthorizationService authorizationService)
        {
            _instrumentRepository = instrumentRepository;
            _auditTrailRepository = auditTrailRepository;
            _maintenanceRecordOfYuanSuRepository = maintenanceRecordOfYuanSuRepository;
            _authorizationService = authorizationService;
        }

        [BindProperty]
        public MaintenanceRecordOfYuanSu MaintenanceRecord { get; set; }
        public IList<AuditTrailLog> AuditTrailLogs { get; set; }

        [BindProperty]
        public string[] DailyMaintenanceContent { get; set; }
        //[BindProperty]
        //public string[] WeeklyMaintenanceContent { get; set; }
        [BindProperty]
        public string[] MonthlyMaintenanceContent { get; set; }
        //[BindProperty]
        //public string[] QuarterlyMaintenanceContent { get; set; }
        [BindProperty]
        public string[] HalfYearlyMaintenanceContent { get; set; }
        [BindProperty]
        public string[] TemporaryMaintenanceContent { get; set; }
        //[BindProperty]
        //public string OtherMaintenanceContent { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            MaintenanceRecord = await _maintenanceRecordOfYuanSuRepository.GetById(id);

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

            MaintenanceRecord = await _maintenanceRecordOfYuanSuRepository.GetById(id);

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
                MaintenanceRecord.GroupName = (await _instrumentRepository.GetById(MaintenanceRecord.InstrumentID)).Group;
                MaintenanceRecord.SetDaily(DailyMaintenanceContent);
                //MaintenanceRecord.SetWeekly(WeeklyMaintenanceContent);
                MaintenanceRecord.SetMonthly(MonthlyMaintenanceContent);
                //MaintenanceRecord.SetQuarterly(QuarterlyMaintenanceContent);
                MaintenanceRecord.SetHalfYearly(HalfYearlyMaintenanceContent);
                MaintenanceRecord.SetTemporary(TemporaryMaintenanceContent);
                //MaintenanceRecord.SetOther(OtherMaintenanceContent);

                await _maintenanceRecordOfYuanSuRepository.Update(MaintenanceRecord);

                return RedirectToPage("../Index", new { instrumentId = MaintenanceRecord.InstrumentID, date = MaintenanceRecord.BeginTime });
            }

            return Page();
        }
    }
}
