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

        public IndexModel(IAuditTrailRepository auditTrailRepository,
            IUserResolverService userResolverService,
            IProjectRepository projectRepository,
            IInstrumentRepository instrumentRepository,
            IUsageRecordRepository usageRecordRepository,
            IMaintenanceRecordRepository maintenanceRecordRepository)
        {
            _auditTrailRepository = auditTrailRepository;
            _userResolverService = userResolverService;
            _projectRepository = projectRepository;
            _instrumentRepository = instrumentRepository;
            _usageRecordRepository = usageRecordRepository;
            _maintenanceRecordRepository = maintenanceRecordRepository;

            Search = new SearchForm(_instrumentRepository);
        }

        [TempData]
        public string StatusMessage { get; set; }

        [BindProperty]
        public SearchForm Search { get; set; }

        public SelectList ProjectsSelectList { get; set; }

        public IList<UsageRecord> UsageRecords { get; set; }
        public IList<MaintenanceRecord> MaintenanceRecords { get; set; }

        public IList<AuditTrailLog> UsageAuditTrailLogs { get; set; }
        public IList<AuditTrailLog> MaintenanceAuditTrailLogs { get; set; }

        [BindProperty]
        public UsageRecord UsageRecord { get; set; }

        [BindProperty]
        public MaintenanceRecord MaintenanceRecord { get; set; }

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

            UsageRecords = _usageRecordRepository.GetAllByInstrumentIdAndBeginTime(instrumentId, date);
            MaintenanceRecords = _maintenanceRecordRepository.GetAllByInstrumentIdAndYearAndMonth(instrumentId, date);

            UsageRecord = new UsageRecord
            {
                InstrumentId = instrumentId,
                Operator = _userResolverService.GetUserName()
            };

            MaintenanceAuditTrailLogs = await _auditTrailRepository.GetAuditTrailLogs(new MaintenanceRecord().GetType().Name);
            UsageAuditTrailLogs = await _auditTrailRepository.GetAuditTrailLogs(new UsageRecord().GetType().Name);
        }

        public JsonResult OnGetLatestRecordOfProject(string project)
        {
            // 如果没有记录，返回null
            var latestRecord = _usageRecordRepository.GetLatestRecordOfProject(project);
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
        public SearchForm(IInstrumentRepository instrumentRepository)
        {
            InstrumentSelectList = instrumentRepository.GetAllInstrumentId(); // TODO 跟用户关联
            Instrument = InstrumentSelectList.FirstOrDefault() ?? "";
        }

        [Display(Name = "月份")]
        public DateTime Date { get; set; } = DateTime.Now;

        [Display(Name = "设备编号")]
        public string Instrument { get; set; }

        public List<string> InstrumentSelectList { get; set; }
    }
}
