using EquipmentManagementSystem.Data;
using EquipmentManagementSystem.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EquipmentManagementSystem.Pages.ReportSystem
{
    public class ReportingModel : PageModel
    {
        private readonly EquipmentContext _context;

        public ReportingModel(EquipmentContext context)
        {
            _context = context;
        }

        public void OnGet()
        {
            PlatformSelectList = new SelectList((from i in _context.Instruments
                                                 select i.Platform)
                                                 .AsNoTracking()
                                                 .Distinct());

            GroupSelectList = new SelectList(_context.Groups.AsNoTracking().Where(g => g.Name != "质谱中心").OrderBy(p => p.Name), "Name", "Name");

            ProjectSelectList = new SelectList(_context.Projects.AsNoTracking().OrderBy(p => p.Name), "Name", "Name");

            InstrumentSelectList = new SelectList(_context.Instruments.AsNoTracking().OrderBy(p => p.ID), "ID", "ID");

            // 初始化时间范围
            Search = new SearchForm();
        }

        /// <summary>
        /// 根据项目返回设备编号
        /// </summary>
        /// <param name="groupName">项目组名</param>
        /// <returns></returns>
        public JsonResult OnGetInstrumentFilter(string projectName)
        {
            var Result = new JsonResult(from m in _context.Instruments
                                        where m.Projects.IndexOf(projectName) >= 0
                                        select m.ID);
            return Result;
        }

        /// <summary>
        /// 根据项目组返回检测项目    
        /// </summary>
        /// <param name="Group">项目组</param>
        /// <returns></returns>
        public JsonResult OnGetProjectFilter(string Group)
        {
            Group = Uri.UnescapeDataString(Group);

            var Result = new JsonResult(_context.Projects
                .AsNoTracking()
                .Select(p => p.Name));

            return Result;
        }

        [BindProperty]
        public SearchForm Search { get; set; }

        public IList<UsageRecord> UsageRecords { get; set; }

        public IList<MaintenanceRecord> MaintenanceRecords { get; set; }
        public Dictionary<string, Dictionary<string, string>> MaintenanceContentOfType { get; set; }

        public IList<MalfunctionWorkOrder> MalfunctionWorkOrders { get; set; }

        public SelectList PlatformSelectList { get; set; }

        public SelectList GroupSelectList { get; set; }

        public SelectList ProjectSelectList { get; set; }

        public SelectList InstrumentSelectList { get; set; }

        public string InstrumentModel { get; set; }
        public string InstrumentPlatform { get; set; }

        public async Task<IActionResult> OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            if (Search.Category == Category.Record)
            {
                var usageRecords = from r in _context.UsageRecords
                                    .AsNoTracking()
                                    .Include(r => r.Instrument)
                                       //.Include(r => r.Project)
                                       //.ThenInclude(r => r.Group)

                                   where r.BeginTime >= Search.BeginTime
                                   where r.BeginTime < Search.EndTime.AddDays(1)
                                   select r;

                var maintenanceRecords = from r in _context.MaintenanceRecords
                                        .AsNoTracking()
                                        .Include(r => r.Instrument)
                                         where r.BeginTime >= Search.BeginTime
                                         where r.BeginTime < Search.EndTime.AddDays(1)
                                         select r;

                if (Search.Instrument != null)
                {
                    InstrumentModel = (await _context.Instruments
                        .AsNoTracking()
                        .Include(m => m.Components)
                        .FirstOrDefaultAsync(m => m.ID == Search.Instrument))
                        .Components.FirstOrDefault(c => c.Name.Contains("主机"))?.Model;

                    InstrumentPlatform = (await _context.Instruments.AsNoTracking().FirstOrDefaultAsync(m => m.ID == Search.Instrument))?.Platform;

                    usageRecords = from r in usageRecords
                                   where r.InstrumentId == Search.Instrument
                                   select r;

                    maintenanceRecords = from r in maintenanceRecords
                                         where r.InstrumentId == Search.Instrument
                                         select r;
                }

                if (Search.Project != null)
                {
                    usageRecords = from r in usageRecords
                                   where r.ProjectName == Search.Project
                                   select r;

                    maintenanceRecords = from r in maintenanceRecords
                                         where r.ProjectName == Search.Project
                                         select r;
                }

                if (Search.Group != null)
                {
                    usageRecords = from r in usageRecords
                                   where r.GroupName == Search.Group
                                   select r;

                    maintenanceRecords = from r in maintenanceRecords
                                         where r.GroupName == Search.Group
                                         select r;
                }

                if (Search.Platform != null)
                {
                    usageRecords = from r in usageRecords
                                   where r.Instrument.Platform == Search.Platform
                                   select r;

                    maintenanceRecords = from r in maintenanceRecords
                                         where r.Instrument.Platform == Search.Platform
                                         select r;
                }

                UsageRecords = usageRecords.OrderBy(record => record.BeginTime).ToList();
                MaintenanceRecords = maintenanceRecords.OrderBy(record => record.BeginTime).ToList();

                // 不同维护类型的维护内容和维护周期
                var targetPlatformInfo = _context.MaintenanceContents.Where(m => m.InstrumentPlatform == InstrumentPlatform).ToList();
                if (targetPlatformInfo != null)
                {
                    MaintenanceContentOfType = new Dictionary<string, Dictionary<string, string>>();
                    foreach (var item in targetPlatformInfo)
                    {
                        var type = item.Type;
                        var content = item.Text;
                        var translate = item.Translation;

                        if (!MaintenanceContentOfType.ContainsKey(type))
                        {
                            MaintenanceContentOfType.Add(type, new Dictionary<string, string>());
                        }
                        MaintenanceContentOfType[type].Add(content, translate);
                    }
                }
            }

            //if (Search.Category == Category.Malfunction)
            //{
            //    MalfunctionWorkOrders = (from record in _context.Set<MalfunctionWorkOrder>()
            //                            .AsNoTracking()
            //                            .Include(m => m.MalfunctionInfo)
            //                            .Include(m => m.Investigation)
            //                            .Include(m => m.RepairRequest)
            //                            .Include(m => m.AccessoriesOrder)
            //                            .Include(m => m.Maintenance)
            //                            .Include(m => m.Validation)
            //                            .Include(m => m.Instrument)
            //                             where record.InstrumentID == Search.Instrument
            //                             where record.CreatedTime >= Search.BeginTime
            //                             where record.CreatedTime < Search.EndTime.AddDays(1)
            //                             select record)
            //                            .ToList();
            //}

            // 下拉选项
            PlatformSelectList = new SelectList((from i in _context.Instruments
                                                 select i.Platform)
                                                 .AsNoTracking()
                                                 .Distinct(), Search.Platform);

            GroupSelectList = new SelectList(_context.Groups.AsNoTracking().Where(g => g.Name != "质谱中心").OrderBy(p => p.Name), "Name", "Name", Search.Group);
            InstrumentSelectList = new SelectList(_context.Instruments.AsNoTracking().OrderBy(p => p.ID), "ID", "ID", Search.Instrument);
            ProjectSelectList = new SelectList(_context.Projects.AsNoTracking().OrderBy(p => p.Name), "Name", "Name", Search.Project);

            return Page();
        }

        public class SearchForm
        {
            [Required(ErrorMessage = "请选择类别")]
            [Display(Name = "类别")]
            public Category Category { get; set; }

            [Required(ErrorMessage = "请输入起始时间")]
            [Display(Name = "起始时间")]
            [DataType(DataType.Date)]
            public DateTime BeginTime { get; set; } = DateTime.Now.AddDays(-30);

            [Required(ErrorMessage = "请输入结束时间")]
            [Display(Name = "结束时间")]
            [DataType(DataType.Date)]
            public DateTime EndTime { get; set; } = DateTime.Now;

            //[Required(ErrorMessage = "请先选择平台")]
            [Display(Name = "设备平台")]
            public string Platform { get; set; }

            //[Required(ErrorMessage = "请选择一个项目组")]
            [Display(Name = "项目组")]
            public string Group { get; set; }

            //[Required(ErrorMessage = "请选择设备编号")]
            [Display(Name = "设备编号")]
            public string Instrument { get; set; }

            [Display(Name = "检测项目")]
            public string Project { get; set; }
        }

        public enum Category
        {
            [Display(Name = "维护使用")]
            Record,
            //[Display(Name = "故障工单")]
            //Malfunction,
        }
    }
}
