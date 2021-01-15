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
        public SelectList ProjectsSelectList { get; private set; }

        public List<UsageRecord> UsageRecords { get; private set; }
        public List<MaintenanceRecord> MaintenanceRecords { get; private set; }

        [BindProperty]
        public UsageRecord UsageRecord { get; set; }
        public MaintenanceRecord MaintenanceRecord { get; private set; } = new MaintenanceRecord(); // for authorization

        public string Platform { get; private set; }
        public string InstrumentModel { get; private set; }

        #region 使用记录表相关属性
        public string MobilePhaseOrCarrierGas { get; private set; }
        public string ColumnPressureUnit { get; private set; }
        public string VacuumDegreeUnit { get; private set; }
        public Dictionary<char, string> MobilePhaseList { get; private set; }
        public Dictionary<char, string> ColumnTypeList { get; private set; }
        public Dictionary<char, string> IonSourceList { get; private set; }
        public Dictionary<char, string> DetectorList { get; private set; }
        public double TotalHours { get; private set; }
        public int TotalSampleNumber { get; private set; }
        public int TotalBatchNumber { get; private set; }
        public int TotalS1BatchNumber { get; private set; }
        public int TotalS2BatchNumber { get; private set; }
        #endregion

        #region 维护记录表相关属性
        public List<string> RecordsIdOfMonth { get; private set; }
        public List<string> DailyMaintenanceContent { get; private set; }
        public List<string> WeeklyMaintenanceContent { get; private set; }
        public List<string> DailyMaintenanceOperator { get; private set; }
        public List<string> WeeklyMaintenanceOperator { get; private set; }
        public Dictionary<string, List<string>> DailyMaintenanceSituation { get; private set; }
        public Dictionary<string, List<string>> WeekyMaintenanceSituation { get; private set; }
        public List<string> TemporaryMaintenanceRecord { get; private set; }
        public string MonthlyMaintenanceRecord { get; private set; }
        public string QuarterlyMaintenanceRecord { get; private set; }
        public string YearlyMaintenanceRecord { get; private set; }
        #endregion

        #region 操作日志
        public IEnumerable<IGrouping<string, AuditTrailLog>> UsageAuditTrailLogs { get; private set; }
        public IEnumerable<IGrouping<string, AuditTrailLog>> MaintenanceAuditTrailLogs { get; private set; }
        #endregion

        public async Task<IActionResult> OnGetAsync(string instrumentId, DateTime? date, string statusMessage)
        {
            if (instrumentId == null)
            {
                instrumentId = Search.InstrumentSelectList.FirstOrDefault();
                if (instrumentId == null)
                {
                    return Page();
                }
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

            // 新的使用记录实例
            UsageRecord = new UsageRecord
            {
                InstrumentID = instrumentId,
                Operator = _userResolverService.GetUserName()
            };

            Platform = (await _instrumentRepository.GetById(instrumentId))?.Platform;
            InstrumentModel = await _instrumentRepository.GetModelById(instrumentId);

            #region 使用记录表相关
            UsageRecords = _usageRecordRepository.GetAllByInstrumentIdAndMonthOfBeginTime(instrumentId, date.GetValueOrDefault());
            MobilePhaseOrCarrierGas = Platform == "GCMS" ? "gas" : "mobilephase";
            ColumnPressureUnit = UsageRecords.FirstOrDefault()?.SystemOneColumnPressureUnit ?? "";
            VacuumDegreeUnit = UsageRecords.FirstOrDefault()?.LowVacuumDegreeUnit.Split(" ")[1] ?? "";
            MobilePhaseList = _usageRecordRepository.GetMobilePhaseOrCarrierGasOfRecord(instrumentId, date.GetValueOrDefault());
            ColumnTypeList = _usageRecordRepository.GetColumnTypeOfRecord(instrumentId, date.GetValueOrDefault());
            IonSourceList = _usageRecordRepository.GetIonSourceOfRecord(instrumentId, date.GetValueOrDefault());
            DetectorList = _usageRecordRepository.GetDetectorOfRecord(instrumentId, date.GetValueOrDefault());
            TotalHours = _usageRecordRepository.GetTotalUsageHoursOfRecords(UsageRecords);
            TotalSampleNumber = _usageRecordRepository.GetTotalSampleNumberOfRecords(UsageRecords);
            TotalBatchNumber = _usageRecordRepository.GetTotalBatchNumberOfRecords(UsageRecords);
            TotalS1BatchNumber = _usageRecordRepository.GetTotalS1BatchNumberOfRecords(UsageRecords);
            TotalS2BatchNumber = _usageRecordRepository.GetTotalS2BatchNumberOfRecords(UsageRecords);
            #endregion

            #region 维护记录表相关
            MaintenanceRecords = _maintenanceRecordRepository.GetAllByInstrumentIdAndYearAndMonth(instrumentId, date);
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

            // “其他”维护内容
            TemporaryMaintenanceRecord.AddRange(MaintenanceRecords.Where(i => !string.IsNullOrEmpty(i.Other))
                .OrderBy(i => i.BeginTime)
                .Select(i => $"{i.Other} {i.Operator}/{i.BeginTime.GetValueOrDefault():yyyy-MM-dd}")
                .ToList());
            #endregion

            // 当前仪器和月份的操作日志
            UsageAuditTrailLogs = _auditTrailRepository.GetAuditTrailLogsGroupingByPKOfInstrumentId(new UsageRecord().GetType().Name,Search.Instrument, Search.Date);
            MaintenanceAuditTrailLogs = _auditTrailRepository.GetAuditTrailLogsGroupingByPKOfInstrumentId(new MaintenanceRecord().GetType().Name, Search.Instrument, Search.Date);

            return Page();
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
            // 默认管理者无GRoup,返回所有设备编号
            var group = userResolverService.GetUserGroup();
            if (!string.IsNullOrEmpty(group))
            {
                InstrumentSelectList = instrumentService.GetInstrumentIdRelateToProjectsOfGroup(group); //  跟用户关联
            }
            else
            {
                InstrumentSelectList = instrumentRepository.GetAllInstrumentId();
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
