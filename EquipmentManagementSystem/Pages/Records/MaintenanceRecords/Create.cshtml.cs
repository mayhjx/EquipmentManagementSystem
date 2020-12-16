using EquipmentManagementSystem.Interfaces;
using EquipmentManagementSystem.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EquipmentManagementSystem.Pages.MaintenanceRecords
{
    public class CreateModel : PageModel
    {
        private readonly IInstrumentRepository _instrumentRepository;
        private readonly IUserResolverService _userResolverService;
        private readonly IMaintenanceContentRepository _maintenanceContentRepository;
        private readonly IMaintenanceRecordRepository _maintenanceRecordRepository;

        public CreateModel(IInstrumentRepository instrumentRepository,
            IUserResolverService userResolverService,
            IMaintenanceContentRepository maintenanceContentRepository,
            IMaintenanceRecordRepository maintenanceRecordRepository)
        {
            _instrumentRepository = instrumentRepository;
            _userResolverService = userResolverService;
            _maintenanceContentRepository = maintenanceContentRepository;
            _maintenanceRecordRepository = maintenanceRecordRepository;
        }

        public string CurrentUserName { get; set; }
        public string SelectedInstrumentId { get; set; } = "";
        public DateTime BeginTime { get; set; }
        public List<string> InstrumentSelectList { get; set; }

        [BindProperty]
        public MaintenanceRecord MaintenanceRecord { get; set; }
        [BindProperty]
        public List<string> SelectedInstruments { get; set; }
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

        public IActionResult OnGet(string instrumentId, DateTime beginTime)
        {
            InstrumentSelectList = _instrumentRepository.GetAllInstrumentId();
            CurrentUserName = _userResolverService.GetUserName();
            BeginTime = beginTime;

            if (!string.IsNullOrEmpty(instrumentId))
            {
                SelectedInstrumentId = instrumentId;
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                InstrumentSelectList = _instrumentRepository.GetAllInstrumentId();
                CurrentUserName = _userResolverService.GetUserName();
                return Page();
            }

            MaintenanceRecord.GroupName = _userResolverService.GetUserGroup();
            MaintenanceRecord.SetDaily(DailyMaintenanceContent);
            MaintenanceRecord.SetWeekly(WeeklyMaintenanceContent);
            MaintenanceRecord.SetMonthly(MonthlyMaintenanceContent);
            MaintenanceRecord.SetQuarterly(QuarterlyMaintenanceContent);
            MaintenanceRecord.SetYearly(YearlyMaintenanceContent);
            MaintenanceRecord.SetTemporary(TemporaryMaintenanceContent);
            MaintenanceRecord.SetOther(OtherMaintenanceContent);

            await _maintenanceRecordRepository.Create(MaintenanceRecord);

            return RedirectToPage("../Index", new { instrumentId = MaintenanceRecord.InstrumentId, date = MaintenanceRecord.BeginTime });
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
