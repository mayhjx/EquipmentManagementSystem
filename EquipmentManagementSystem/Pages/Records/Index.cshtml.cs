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
    public class IndexModel : BasePageModel
    {
        private readonly IUsageRecordRepository _usageRecordRepository;
        private readonly IMaintenanceRecordRepository _maintenanceRecordRepository;
        public IndexModel(EquipmentContext context,
            UserManager<User> userManager,
            IAuthorizationService authorizationService,
            IUsageRecordRepository usageRecordRepository,
            IMaintenanceRecordRepository maintenanceRecordRepository)
            : base(context, userManager, authorizationService)
        {
            _usageRecordRepository = usageRecordRepository;
            _maintenanceRecordRepository = maintenanceRecordRepository;
        }

        [BindProperty]
        public SearchForm Search { get; set; } = new SearchForm();

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
                instrumentId = Search.InstrumentSelectList[0].Text;
                Search.Instrument = Search.InstrumentSelectList[0].Text;
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

            UsageRecords = _usageRecordRepository.GetAllByInstrumentIdAndYearAndMonth(instrumentId, date);
            MaintenanceRecords = _maintenanceRecordRepository.GetAllByInstrumentIdAndYearAndMonth(instrumentId, date);

            //var usageRecord = from record in _context.UsageRecords
            //                .AsNoTracking()
            //                .Include(record => record.Instrument)
            //                .Include(record => record.Project)
            //                    .ThenInclude(p => p.Group)
            //                  select record;

            //var maintenanceRecord = from record in _context.MaintenanceRecords
            //                .AsNoTracking()
            //                .Include(record => record.Instrument)
            //                .Include(record => record.Project)
            //                    .ThenInclude(p => p.Group)
            //                        select record;

            // 显示使用中的记录
            //Search.Status = status;
            //if (Search.Status == Status.Using)
            //{
            //    //Search.Status = Status.Using;
            //    usageRecord = usageRecord.Where(record => record.EndTime == null);
            //    maintenanceRecord = maintenanceRecord.Where(record => record.EndTime == null);
            //}

            //var isAuthorized = User.IsInRole(Constants.DirectorRole) || User.IsInRole(Constants.ManagerRole);

            //var projects = _context.Projects.AsNoTracking().Include(p => p.Group);

            //if (User.Identity.IsAuthenticated && !isAuthorized)
            //{
            //    var currentUserGroup = (await _userManager.GetUserAsync(User)).Group;

            //    var projectsOfcurrentUserGroup = await projects.AsNoTracking().Where(p => p.Group.Name == currentUserGroup).Select(p => p.Name).ToListAsync();

            //    //ProjectSelectList = new SelectList(projects.AsNoTracking().Where(p => p.Group.Name == currentUserGroup).OrderBy(p => p.Name), "Name", "Name");

            //    // 显示当前用户所属项目组的使用登记
            //    usageRecord = from record in usageRecord
            //                  where projectsOfcurrentUserGroup.Contains(record.ProjectName)
            //                  select record;

            //    maintenanceRecord = from record in maintenanceRecord
            //                        where projectsOfcurrentUserGroup.Contains(record.ProjectName)
            //                        select record;
            //}
            //else
            //{
            //    ProjectSelectList = new SelectList(projects.AsNoTracking().OrderBy(p => p.Name), "Name", "Name");
            //}

            //UsageRecords = await usageRecord.OrderByDescending(m => m.BeginTimeOfTest).ToListAsync();
            //MaintenanceRecords = await maintenanceRecord.OrderByDescending(m => m.BeginTime).ToListAsync();

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

        /// <summary>
        /// 根据项目返回设备编号
        /// </summary>
        /// <param name="groupName">项目组名</param>
        /// <returns></returns>
        //public JsonResult OnGetInstrumentFilter(string projectName)
        //{
        //    var Result = new JsonResult(from m in _context.Instruments
        //                                where m.Projects.IndexOf(projectName) >= 0
        //                                select m.ID);
        //    return Result;
        //}

        public IActionResult OnPostSearch()
        {
            var selectedDate = Search.Date;
            var selectedInstrumentId = Search.Instrument;
            StatusMessage = $"仪器编号{selectedInstrumentId}，日期{selectedDate}";
            return RedirectToPage("./Index", new { instrumentId = selectedInstrumentId, date = selectedDate });
        }

        public class SearchForm
        {
            [Display(Name = "月份")]
            public DateTime Date { get; set; } = DateTime.Now;

            [Display(Name = "设备编号")]
            public string Instrument { get; set; }

            public List<SelectListItem> InstrumentSelectList { get; } = new List<SelectListItem>
            {
                new SelectListItem{Value = "FXS-YZ01", Text = "FXS-YZ01"},
                new SelectListItem{Value = "FXS-YZ02", Text = "FXS-YZ02"},
                new SelectListItem{Value = "FXS-YZ03", Text = "FXS-YZ03"},
                new SelectListItem{Value = "FXS-YZ17", Text = "FXS-YZ17"},
            };
        }
    }
}
