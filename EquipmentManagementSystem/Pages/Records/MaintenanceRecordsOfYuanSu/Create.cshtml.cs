using EquipmentManagementSystem.Authorization;
using EquipmentManagementSystem.Interfaces;
using EquipmentManagementSystem.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EquipmentManagementSystem.Pages.MaintenanceRecordsOfYuanSu
{
    public class CreateModel : PageModel
    {
        private readonly IInstrumentRepository _instrumentRepository;
        private readonly IUserResolverService _userResolverService;
        private readonly IMaintenanceContentRepository _maintenanceContentRepository;
        private readonly IMaintenanceRecordOfYuanSuRepository _maintenanceRecordOfYuanSuRepository;
        private readonly IAuthorizationService _authorizationService;

        public CreateModel(IInstrumentRepository instrumentRepository,
            IUserResolverService userResolverService,
            IMaintenanceContentRepository maintenanceContentRepository,
            IMaintenanceRecordOfYuanSuRepository maintenanceRecordOfYuanSuRepository,
            IAuthorizationService authorizationService)
        {
            _instrumentRepository = instrumentRepository;
            _userResolverService = userResolverService;
            _maintenanceContentRepository = maintenanceContentRepository;
            _maintenanceRecordOfYuanSuRepository = maintenanceRecordOfYuanSuRepository;
            _authorizationService = authorizationService;
        }

        public string CurrentUserName { get; set; }
        public string SelectedInstrumentId { get; set; } = "";
        public DateTime BeginTime { get; set; }
        public List<string> InstrumentSelectList { get; set; }

        [BindProperty]
        public MaintenanceRecordOfYuanSu MaintenanceRecord { get; set; }
        [BindProperty]
        public List<string> SelectedInstruments { get; set; }
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

            var isAuthorized = await _authorizationService.AuthorizeAsync(User, MaintenanceRecord, Operations.Create);

            if (!isAuthorized.Succeeded)
            {
                return Forbid();
            }

            MaintenanceRecord.GroupName = (await _instrumentRepository.GetById(MaintenanceRecord.InstrumentID)).Group;
            MaintenanceRecord.SetDaily(DailyMaintenanceContent);
            //MaintenanceRecord.SetWeekly(WeeklyMaintenanceContent);
            MaintenanceRecord.SetMonthly(MonthlyMaintenanceContent);
            //MaintenanceRecord.SetQuarterly(QuarterlyMaintenanceContent);
            MaintenanceRecord.SetHalfYearly(HalfYearlyMaintenanceContent);
            MaintenanceRecord.SetTemporary(TemporaryMaintenanceContent);
            //MaintenanceRecord.SetOther(OtherMaintenanceContent);

            await _maintenanceRecordOfYuanSuRepository.Create(MaintenanceRecord);

            return RedirectToPage("../Index", new { instrumentId = MaintenanceRecord.InstrumentID, date = MaintenanceRecord.BeginTime });
        }
    }
}
