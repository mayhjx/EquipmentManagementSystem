using EquipmentManagementSystem.Data;
using EquipmentManagementSystem.Interfaces;
using EquipmentManagementSystem.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EquipmentManagementSystem.Pages.Records
{
    [AllowAnonymous]
    public class IndexModel : BasePageModel
    {
        private readonly IUserResolverService _userResolverService;
        private readonly IInstrumentRepository _instrumentRepository;
        private readonly IUsageRecordRepository _usageRecordRepository;
        private readonly IMaintenanceRecordRepository _maintenanceRecordRepository;

        public IndexModel(EquipmentContext context,
            UserManager<User> userManager,
            IAuthorizationService authorizationService,
            IUserResolverService userResolverService,
            IInstrumentRepository instrumentRepository,
            IUsageRecordRepository usageRecordRepository,
            IMaintenanceRecordRepository maintenanceRecordRepository)
            : base(context, userManager, authorizationService)
        {
            _userResolverService = userResolverService;
            _instrumentRepository = instrumentRepository;
            _usageRecordRepository = usageRecordRepository;
            _maintenanceRecordRepository = maintenanceRecordRepository;

            Search = new SearchForm(_instrumentRepository);
        }

        [BindProperty]
        public SearchForm Search { get; set; }

        public IList<MaintenanceRecord> MaintenanceRecords { get; set; }
        public IList<UsageRecord> UsageRecords { get; set; }

        //public MaintenanceRecord MaintenanceRecord { get; private set; } = new MaintenanceRecord();
        //public UsageRecord UsageRecord { get; private set; } = new UsageRecord();
        public IList<AuditTrailLog> MaintenanceAuditTrailLogs { get; set; }
        public IList<AuditTrailLog> UsageAuditTrailLogs { get; set; }

        [BindProperty]
        public UsageRecord UsageRecord { get; set; }

        [TempData]
        public string StatusMessage { get; set; }

        public void OnGetAsync(string instrumentId, DateTime? date, string statusMessage)
        {
            if (instrumentId == null)
            {
                instrumentId = Search.InstrumentSelectList[0];
                Search.Instrument = Search.InstrumentSelectList[0];
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

            ProjectsSelectList = new SelectList(_instrumentRepository.GetTestProjectsById(instrumentId), instrumentId);

            UsageRecords = _usageRecordRepository.GetAllByInstrumentIdAndBeginTime(instrumentId, date);
            MaintenanceRecords = _maintenanceRecordRepository.GetAllByInstrumentIdAndYearAndMonth(instrumentId, date);

            UsageRecord = new UsageRecord { Creator = _userResolverService.GetUserName() };

            //MaintenanceAuditTrailLogs = await _context.AuditTrailLogs
            //    .AsNoTracking()
            //    .Where(l => l.EntityName == MaintenanceRecord.GetType().Name)
            //    .OrderByDescending(l => l.DateChanged)
            //    .ToListAsync();

            //UsageAuditTrailLogs = await _context.AuditTrailLogs
            //    .AsNoTracking()
            //    .Where(l => l.EntityName == UsageRecord.GetType().Name)
            //    .OrderByDescending(l => l.DateChanged)
            //    .ToListAsync();
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
            InstrumentSelectList = instrumentRepository.GetAllInstrumentId();
        }

        [Display(Name = "月份")]
        public DateTime Date { get; set; } = DateTime.Now;

        [Display(Name = "设备编号")]
        public string Instrument { get; set; }

        public List<string> InstrumentSelectList { get; set; }
    }
}
