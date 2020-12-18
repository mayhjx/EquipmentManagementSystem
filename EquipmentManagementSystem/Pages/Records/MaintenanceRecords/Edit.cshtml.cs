﻿using EquipmentManagementSystem.Authorization;
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
        private readonly IInstrumentRepository _instrumentRepository;
        private readonly IUserResolverService _userResolverService;
        private readonly IMaintenanceContentRepository _maintenanceContentRepository;
        private readonly IMaintenanceRecordRepository _maintenanceRecordRepository;
        private readonly IAuthorizationService _authorizationService;

        public EditModel(IAuditTrailRepository auditTrailRepository,
            IInstrumentRepository instrumentRepository,
            IUserResolverService userResolverService,
            IMaintenanceContentRepository maintenanceContentRepository,
            IMaintenanceRecordRepository maintenanceRecordRepository,
            IAuthorizationService authorizationService)
        {
            _auditTrailRepository = auditTrailRepository;
            _instrumentRepository = instrumentRepository;
            _userResolverService = userResolverService;
            _maintenanceContentRepository = maintenanceContentRepository;
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

        /// <summary>
        /// 返回对应设备平台的维护内容
        /// </summary>
        public async Task<JsonResult> OnGetMaintenanceContents(string instrument)
        {
            var platform = (await _instrumentRepository.GetById(instrument)).Platform;
            if (platform == null)
            {
                return new JsonResult("该仪器未设置平台");
            }

            var contents = _maintenanceContentRepository.GetByInstrumentPlatform(platform);

            if (contents.Count == 0)
            {
                return new JsonResult("该设备平台未设置维护内容");
            }

            var result = new JsonResult(contents);

            return result;
        }
    }
}
