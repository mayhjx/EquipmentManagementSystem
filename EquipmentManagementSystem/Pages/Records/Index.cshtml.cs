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
        private readonly IUsageRecordOfYuanSuRepository _usageRecordOfYuanSuRepository;
        private readonly IMaintenanceRecordRepository _maintenanceRecordRepository;
        private readonly IMaintenanceRecordOfYuanSuRepository _maintenanceRecordOfYuanSuRepository;
        private readonly IMaintenanceRecordService _maintenanceRecordService;
        private readonly IMaintenanceContentRepository _maintenanceContentRepository;

        public IndexModel(IAuditTrailRepository auditTrailRepository,
            IUserResolverService userResolverService,
            IProjectRepository projectRepository,
            IInstrumentRepository instrumentRepository,
            IUsageRecordRepository usageRecordRepository,
            IUsageRecordOfYuanSuRepository usageRecordOfYuanSuRepository,
            IMaintenanceRecordRepository maintenanceRecordRepository,
            IMaintenanceRecordOfYuanSuRepository maintenanceRecordOfYuanSuRepository,
            IMaintenanceRecordService maintenanceRecordService,
            IMaintenanceContentRepository maintenanceContentRepository,
            IInstrumentService instrumentService)
        {
            _auditTrailRepository = auditTrailRepository;
            _userResolverService = userResolverService;
            _projectRepository = projectRepository;
            _instrumentRepository = instrumentRepository;
            _usageRecordRepository = usageRecordRepository;
            _usageRecordOfYuanSuRepository = usageRecordOfYuanSuRepository;
            _maintenanceRecordRepository = maintenanceRecordRepository;
            _maintenanceRecordOfYuanSuRepository = maintenanceRecordOfYuanSuRepository;
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

        #region 液质使用记录表相关
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

        #region 维护记录表相关
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

        #region 元素项目使用记录相关
        public UsageRecordOfYuanSu UsageRecordOfYuanSu { get; set; }
        public List<UsageRecordOfYuanSu> ListOfYuanSuUsageRecord { get; private set; }
        #endregion

        #region 元素项目维护记录相关
        public MaintenanceRecordOfYuanSu MaintenanceRecordOfYuanSu { get; set; }
        public List<MaintenanceRecordOfYuanSu> ListOfYuanSuMaintenanceRecord { get; private set; }
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

            Platform = (await _instrumentRepository.GetById(instrumentId))?.Platform;

            // 选择了ICP-MS仪器编号
            if (Search.SelectedICPMSInstrument)
            {
                #region 元素使用登记表相关
                ListOfYuanSuUsageRecord = _usageRecordOfYuanSuRepository.GetAllByInstrumentIdAndMonthOfBeginTime(instrumentId, date.GetValueOrDefault());

                // 新的使用记录实例
                UsageRecordOfYuanSu = new UsageRecordOfYuanSu
                {
                    InstrumentID = instrumentId,
                    ProjectName = (await _projectRepository.GetShortNamesByNames(_instrumentRepository.GetTestProjectsById(instrumentId))).FirstOrDefault(),
                    Operator = _userResolverService.GetUserName()
                };
                #endregion

                #region 元素维护登记表相关
                ListOfYuanSuMaintenanceRecord = _maintenanceRecordOfYuanSuRepository.GetAllByInstrumentIdAndYearAndMonth(instrumentId, date);
                RecordsIdOfMonth = _maintenanceRecordService.GetRecordIdOfMonth(ListOfYuanSuMaintenanceRecord);
                #endregion

                // 当前仪器和月份的操作日志
                UsageAuditTrailLogs = _auditTrailRepository.GetAuditTrailLogsGroupingByPKOfInstrumentId(new UsageRecordOfYuanSu().GetType().Name, Search.Instrument, Search.Date);
                MaintenanceAuditTrailLogs = _auditTrailRepository.GetAuditTrailLogsGroupingByPKOfInstrumentId(new MaintenanceRecord().GetType().Name, Search.Instrument, Search.Date);
            }
            else
            {
                #region 液质使用记录表相关
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

                InstrumentModel = await _instrumentRepository.GetModelById(instrumentId);

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

                #region 液质维护记录表相关
                MaintenanceRecords = _maintenanceRecordRepository.GetAllByInstrumentIdAndYearAndMonth(instrumentId, date);
                RecordsIdOfMonth = _maintenanceRecordService.GetRecordIdOfMonth(MaintenanceRecords);
                DailyMaintenanceContent = _maintenanceContentRepository.GetDailyContentByInstrumentPlatform(Platform);
                WeeklyMaintenanceContent = _maintenanceContentRepository.GetWeeklyContentByInstrumentPlatform(Platform);
                DailyMaintenanceOperator = _maintenanceRecordService.GetDailyMaintenanceOperatorOfMonth(instrumentId, date.GetValueOrDefault());
                DailyMaintenanceSituation = await _maintenanceRecordService.GetDailyMaintenanceSituationOfMonth(instrumentId, date.GetValueOrDefault());
                WeeklyMaintenanceOperator = _maintenanceRecordService.GetWeeklyMaintenanceOperatorOfMonth(instrumentId, date.GetValueOrDefault());
                WeekyMaintenanceSituation = await _maintenanceRecordService.GetWeeklyMaintenanceSituationOfMonth(instrumentId, date.GetValueOrDefault());

                MonthlyMaintenanceRecord = string.Join("            ",MaintenanceRecords.Where(i => !string.IsNullOrEmpty(i.Monthly))
                    .Select(i => $"{i.Monthly} {i.Operator}/{i.BeginTime.GetValueOrDefault():yyyy-MM-dd}"));

                QuarterlyMaintenanceRecord = string.Join("            ", MaintenanceRecords.Where(i => !string.IsNullOrEmpty(i.Quarterly))
                    .Select(i => $"{i.Quarterly} {i.Operator}/{i.BeginTime.GetValueOrDefault():yyyy-MM-dd}"));

                YearlyMaintenanceRecord = string.Join("            ", MaintenanceRecords.Where(i => !string.IsNullOrEmpty(i.Yearly))
                    .Select(i => $"{i.Yearly} {i.Operator}/{i.BeginTime.GetValueOrDefault():yyyy-MM-dd}"));

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
                UsageAuditTrailLogs = _auditTrailRepository.GetAuditTrailLogsGroupingByPKOfInstrumentId(new UsageRecord().GetType().Name, Search.Instrument, Search.Date);
                MaintenanceAuditTrailLogs = _auditTrailRepository.GetAuditTrailLogsGroupingByPKOfInstrumentId(new MaintenanceRecord().GetType().Name, Search.Instrument, Search.Date);
            }

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
            // 默认管理者无Group,返回所有设备编号
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
        public bool SelectedICPMSInstrument => Instrument.Contains("MS"); 
        public List<string> InstrumentSelectList { get; set; }
    }
}
