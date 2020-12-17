using EquipmentManagementSystem.Interfaces;
using EquipmentManagementSystem.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EquipmentManagementSystem.Pages.Records
{
    public class IndexModel : PageModel
    {
        private readonly IAuditTrailRepository _auditTrailRepository;
        private readonly IUserResolverService _userResolverService;
        private readonly IProjectRepository _projectRepository;
        private readonly IInstrumentRepository _instrumentRepository;
        private readonly IUsageRecordRepository _usageRecordRepository;
        private readonly IMaintenanceRecordRepository _maintenanceRecordRepository;
        private readonly IMaintenanceRecordService _maintenanceRecordService;
        private readonly IMaintenanceContentRepository _maintenanceContentRepository;

        public IndexModel(IAuditTrailRepository auditTrailRepository,
            IUserResolverService userResolverService,
            IProjectRepository projectRepository,
            IInstrumentRepository instrumentRepository,
            IUsageRecordRepository usageRecordRepository,
            IMaintenanceRecordRepository maintenanceRecordRepository,
            IMaintenanceRecordService maintenanceRecordService,
            IMaintenanceContentRepository maintenanceContentRepository,
            IInstrumentService instrumentService)
        {
            _auditTrailRepository = auditTrailRepository;
            _userResolverService = userResolverService;
            _projectRepository = projectRepository;
            _instrumentRepository = instrumentRepository;
            _usageRecordRepository = usageRecordRepository;
            _maintenanceRecordRepository = maintenanceRecordRepository;
            _maintenanceRecordService = maintenanceRecordService;
            _maintenanceContentRepository = maintenanceContentRepository;

            Search = new SearchForm(instrumentService, instrumentRepository, _userResolverService);
        }

        [TempData]
        public string StatusMessage { get; set; }

        [BindProperty]
        public SearchForm Search { get; set; }
        public SelectList ProjectsSelectList { get; set; }

        public IList<UsageRecord> UsageRecords { get; set; }
        public IList<MaintenanceRecord> MaintenanceRecords { get; set; }

        [BindProperty]
        public UsageRecord UsageRecord { get; set; }
        public string Platform { get; set; }
        public string InstrumentModel { get; set; }

        #region 维护记录表相关属性
        public string MobilePhaseOrCarrierGas { get; set; }
        public string ColumnPressureUnit { get; set; }
        public string VacuumDegreeUnit { get; set; }
        public Dictionary<char, string> MobilePhaseList { get; set; }
        public Dictionary<char, string> ColumnTypeList { get; set; }
        public Dictionary<char, string> IonSourceList { get; set; }
        public Dictionary<char, string> DetectorList { get; set; }
        #endregion

        #region 维护记录表相关属性
        public List<string> RecordsIdOfMonth { get; set; }
        public List<string> DailyMaintenanceContent { get; set; }
        public List<string> WeeklyMaintenanceContent { get; set; }
        public List<string> DailyMaintenanceOperator { get; set; }
        public List<string> WeeklyMaintenanceOperator { get; set; }
        public Dictionary<string, List<string>> DailyMaintenanceSituation { get; set; }
        public Dictionary<string, List<string>> WeekyMaintenanceSituation { get; set; }
        public List<string> TemporaryMaintenanceRecord { get; set; }
        public string MonthlyMaintenanceRecord { get; set; }
        public string QuarterlyMaintenanceRecord { get; set; }
        public string YearlyMaintenanceRecord { get; set; }
        #endregion

        #region 操作日志
        public IList<AuditTrailLog> UsageAuditTrailLogs { get; set; }
        public IList<AuditTrailLog> MaintenanceAuditTrailLogs { get; set; }
        #endregion

        public async Task OnGetAsync(string instrumentId, DateTime? date, string statusMessage)
        {
            if (instrumentId == null)
            {
                instrumentId = Search.InstrumentSelectList.FirstOrDefault();
            }
            else
            {
                Search.Instrument = instrumentId;
            }

            if (date == null)
            {
                date = Search.Date;
            }
            else
            {
                Search.Date = date.GetValueOrDefault();
            }

            if (!string.IsNullOrEmpty(statusMessage))
            {
                StatusMessage = statusMessage;
            }

            List<string> projectShortNameList = new List<string>();

            if (instrumentId != null)
            {
                List<string> testProjectList = _instrumentRepository.GetTestProjectsById(instrumentId);
                projectShortNameList = await _projectRepository.GetShortNamesByNames(testProjectList);
            }

            ProjectsSelectList = new SelectList(projectShortNameList);

            // 记录列表
            UsageRecords = _usageRecordRepository.GetAllByInstrumentIdAndBeginTime(instrumentId, date);
            MaintenanceRecords = _maintenanceRecordRepository.GetAllByInstrumentIdAndYearAndMonth(instrumentId, date);

            // 新的使用记录实例
            UsageRecord = new UsageRecord
            {
                InstrumentId = instrumentId,
                Operator = _userResolverService.GetUserName()
            };

            Platform = (await _instrumentRepository.GetById(instrumentId)).Platform;
            InstrumentModel = await _instrumentRepository.GetModelById(instrumentId);

            #region 使用记录表相关
            MobilePhaseOrCarrierGas = Platform == "GCMS" ? "gas" : "mobilephase";
            ColumnPressureUnit = UsageRecords.FirstOrDefault()?.SystemOneColumnPressureUnit ?? "";
            VacuumDegreeUnit = UsageRecords.FirstOrDefault()?.LowVacuumDegreeUnit ?? "";
            MobilePhaseList = _usageRecordRepository.GetMobilePhaseOrCarrierGasOfRecord(instrumentId, date.GetValueOrDefault());
            ColumnTypeList = _usageRecordRepository.GetColumnTypeOfRecord(instrumentId, date.GetValueOrDefault());
            IonSourceList = _usageRecordRepository.GetIonSourceOfRecord(instrumentId, date.GetValueOrDefault());
            DetectorList = _usageRecordRepository.GetDetectorOfRecord(instrumentId, date.GetValueOrDefault());
            #endregion

            #region 维护记录表相关
            RecordsIdOfMonth = _maintenanceRecordService.GetRecordIdOfMonth(instrumentId, date.GetValueOrDefault());
            DailyMaintenanceContent = _maintenanceContentRepository.GetDailyContentByInstrumentPlatform(Platform);
            WeeklyMaintenanceContent = _maintenanceContentRepository.GetWeeklyContentByInstrumentPlatform(Platform);
            DailyMaintenanceOperator = _maintenanceRecordService.GetDailyMaintenanceOperatorOfMonth(instrumentId, date.GetValueOrDefault());
            DailyMaintenanceSituation = await _maintenanceRecordService.GetDailyMaintenanceSituationOfMonth(instrumentId, date.GetValueOrDefault());
            WeeklyMaintenanceOperator = _maintenanceRecordService.GetWeeklyMaintenanceOperatorOfMonth(instrumentId, date.GetValueOrDefault());
            WeekyMaintenanceSituation = await _maintenanceRecordService.GetWeeklyMaintenanceSituationOfMonth(instrumentId, date.GetValueOrDefault());

            MonthlyMaintenanceRecord = MaintenanceRecords.Where(i => !string.IsNullOrEmpty(i.Monthly))
                .Select(i => $"{i.Monthly} {i.Operator}/{i.BeginTime.GetValueOrDefault():yyyy-MM-dd}")
                .FirstOrDefault();

            QuarterlyMaintenanceRecord = MaintenanceRecords.Where(i => !string.IsNullOrEmpty(i.Quarterly))
                .Select(i => $"{i.Quarterly} {i.Operator}/{i.BeginTime.GetValueOrDefault():yyyy-MM-dd}")
                .FirstOrDefault();

            YearlyMaintenanceRecord = MaintenanceRecords.Where(i => !string.IsNullOrEmpty(i.Yearly))
                .Select(i => $"{i.Yearly} {i.Operator}/{i.BeginTime.GetValueOrDefault():yyyy-MM-dd}")
                .FirstOrDefault();

            TemporaryMaintenanceRecord = MaintenanceRecords.Where(i => !string.IsNullOrEmpty(i.Temporary))
                .OrderBy(i => i.BeginTime)
                .Select(i => $"{i.Temporary} {i.Operator}/{i.BeginTime.GetValueOrDefault():yyyy-MM-dd}")
                .ToList();

            // 其他维护内容
            TemporaryMaintenanceRecord.AddRange(MaintenanceRecords.Where(i => !string.IsNullOrEmpty(i.Other))
                .OrderBy(i => i.BeginTime)
                .Select(i => $"{i.Other} {i.Operator}/{i.BeginTime.GetValueOrDefault():yyyy-MM-dd}")
                .ToList());
            #endregion

            // 当前仪器和月份的操作日志
            UsageAuditTrailLogs = await _auditTrailRepository.GetAuditTrailLogs(new UsageRecord().GetType().Name, null, Search.Date);
            MaintenanceAuditTrailLogs = await _auditTrailRepository.GetAuditTrailLogs(new MaintenanceRecord().GetType().Name, null, Search.Date);
        }

        public JsonResult OnGetLatestRecordOfProject(string project, string instrumentId)
        {
            // 如果没有记录，返回null
            var latestRecord = _usageRecordRepository.GetLatestRecordOfProject(project, instrumentId);
            return new JsonResult(latestRecord);
        }

        public IActionResult OnPostSearch()
        {
            var selectedDate = Search.Date;
            var selectedInstrumentId = Search.Instrument;
            return RedirectToPage("./Index", new { instrumentId = selectedInstrumentId, date = selectedDate });
        }
    }

    public class SearchForm
    {
        public SearchForm() { }
        public SearchForm(IInstrumentService instrumentService,
            IInstrumentRepository instrumentRepository,
            IUserResolverService userResolverService)
        {
            var group = userResolverService.GetUserGroup();
            if (group == "质谱中心")
            {
                InstrumentSelectList = instrumentRepository.GetAllInstrumentId();
            }
            else
            {
                InstrumentSelectList = instrumentService.GetInstrumentIdRelateToProjectsOfGroup(group); //  跟用户关联
            }
            Instrument = InstrumentSelectList.FirstOrDefault() ?? "";
        }

        [Display(Name = "月份")]
        public DateTime Date { get; set; } = DateTime.Now;

        [Display(Name = "设备编号")]
        public string Instrument { get; set; }

        public List<string> InstrumentSelectList { get; set; }
    }
}
