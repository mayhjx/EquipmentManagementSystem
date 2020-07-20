using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using EquipmentManagementSystem.Data;
using EquipmentManagementSystem.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

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
                .Include(p => p.Group)
                .Where(g => g.Group.Name == Group)
                .Select(p => p.Name));

            return Result;
        }

        [BindProperty]
        public SearchForm Search { get; set; }

        public IList<UsageRecord> UsageRecords { get; set; }
        public IList<MalfunctionWorkOrder> MalfunctionWorkOrders { get; set; }

        public SelectList PlatformSelectList { get; set; }

        public SelectList GroupSelectList { get; set; }

        public SelectList ProjectSelectList { get; set; }

        public SelectList InstrumentSelectList { get; set; }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }


            if (Search.Category == Category.Usage)
            {
                var records = from record in _context.UsageRecords
                            .AsNoTracking()
                            .Include(record => record.Instrument)
                            .Include(record => record.Project)
                                .ThenInclude(record => record.Group)
                              where record.BeginTimeOfTest >= Search.BeginTime
                              where record.BeginTimeOfTest < Search.EndTime.AddDays(1)
                              select record;

                if (Search.Instrument != null)
                {
                    records = from r in records
                              where r.InstrumentId == Search.Instrument
                              select r;
                }
                if (Search.Project != null)
                {
                    records = from r in records
                              where r.ProjectName == Search.Project
                              select r;
                }
                if (Search.Group != null)
                {
                    records = from r in records
                              where r.Project.Group.Name == Search.Group
                              select r;
                }
                if (Search.Platform != null)
                {
                    records = from r in records
                              where r.Instrument.Platform == Search.Platform
                              select r;
                }

                UsageRecords = records.ToList();
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

            PlatformSelectList = new SelectList((from i in _context.Instruments
                                                 select i.Platform)
                                                 .Distinct(), Search.Platform);

            GroupSelectList = new SelectList(_context.Groups.AsNoTracking(), "Name", "Name", Search.Group);
            InstrumentSelectList = new SelectList(_context.Instruments.AsNoTracking(), "ID", "ID", Search.Instrument);
            ProjectSelectList = new SelectList(_context.Projects.AsNoTracking(), "Name", "Name", Search.Project);

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
            [Display(Name = "使用登记")]
            Usage,
            //[Display(Name = "故障工单")]
            //Malfunction,
        }
    }
}
