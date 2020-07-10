﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using EquipmentManagementSystem.Data;
using EquipmentManagementSystem.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace EquipmentManagementSystem.Pages.ReportSystem
{
    public class ReportingModel : PageModel
    {
        private readonly EquipmentContext _context;
        private readonly UserManager<User> _userManager;

        public ReportingModel(EquipmentContext context, UserManager<User> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public void OnGet()
        {
            //ProjectSelectList = new SelectList(_context.Projects.AsNoTracking(), "Name", "Name");
            InstrumentSelectList = new SelectList(_context.Instruments.AsNoTracking(), "ID", "ID");

            //var isAdmin = User.IsInRole(Constants.ManagerRole) || User.IsInRole(Constants.DirectorRole);

            //if (isAdmin)
            //{
            //    ProjectSelectList = new SelectList(_context.Projects.AsNoTracking(), "Name", "Name");
            //    InstrumentSelectList = new SelectList(_context.Instruments.AsNoTracking(), "ID", "ID");
            //}
            //else
            //{
            //    var userGroup = _userManager.GetUserAsync(User).Result?.Group;
            //    ProjectSelectList = new SelectList(_context.Projects.AsNoTracking().Include(m => m.Group).Where(m => m.Group.Name == userGroup), "Name", "Name");
            //    InstrumentSelectList = new SelectList(_context.Instruments.AsNoTracking().Where(m => m.Group == userGroup), "ID", "ID");
            //}

            // 初始化时间范围
            Search = new SearchForm();
        }

        /// <summary>
        /// 根据项目组返回设备编号
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
        /// 根据设备编号返回检测项目    
        /// </summary>
        /// <param name="instrumentId">设备编号</param>
        /// <returns></returns>
        public JsonResult OnGetProjectFilter(string instrumentId)
        {
            var Result = new JsonResult(_context.Instruments.FindAsync(instrumentId).Result.Projects.Split(", ").ToList());
            return Result;
        }

        [BindProperty]
        public SearchForm Search { get; set; }

        public IList<UsageRecord> UsageRecords { get; set; }
        public IList<MalfunctionWorkOrder> MalfunctionWorkOrders { get; set; }

        public SelectList InstrumentSelectList { get; set; }

        //public SelectList GroupSelectList { get; set; }

        public SelectList ProjectSelectList { get; set; }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            if (Search.Category == Category.Usage)
            {
                UsageRecords = (from record in _context.UsageRecords
                                .AsNoTracking()
                                .Include(record => record.Instrument)
                                .Include(record => record.Project)
                                where record.InstrumentId == Search.Instrument
                                where record.ProjectName == Search.Project
                                where record.BeginTimeOfTest >= Search.BeginTime
                                where record.BeginTimeOfTest < Search.EndTime.AddDays(1)
                                select record)
                                .ToList();
            }

            if (Search.Category == Category.Malfunction)
            {
                MalfunctionWorkOrders = (from record in _context.Set<MalfunctionWorkOrder>()
                                        .AsNoTracking()
                                        .Include(m => m.MalfunctionInfo)
                                        .Include(m => m.Investigation)
                                        .Include(m => m.RepairRequest)
                                        .Include(m => m.AccessoriesOrder)
                                        .Include(m => m.Maintenance)
                                        .Include(m => m.Validation)
                                        .Include(m => m.Instrument)
                                         where record.InstrumentID == Search.Instrument
                                         where record.CreatedTime >= Search.BeginTime
                                         where record.CreatedTime < Search.EndTime.AddDays(1)
                                         select record)
                                        .ToList();
            }

            InstrumentSelectList = new SelectList(_context.Instruments.AsNoTracking(), "ID", "ID", Search.Instrument);
            ProjectSelectList = new SelectList(_context.Instruments.FindAsync(Search.Instrument).Result.Projects.Split(", "), Search.Project);

            //var isAdmin = User.IsInRole(Constants.ManagerRole) || User.IsInRole(Constants.DirectorRole);

            //if (isAdmin)
            //{
            //    ProjectSelectList = new SelectList(_context.Projects.AsNoTracking(), "Name", "Name", Search.Project);
            //    InstrumentSelectList = new SelectList(_context.Instruments.AsNoTracking(), "ID", "ID", Search.Instrument);
            //}
            //else
            //{
            //    var userGroup = _userManager.GetUserAsync(User).Result?.Group;
            //    ProjectSelectList = new SelectList(_context.Projects.AsNoTracking().Include(m => m.Group).Where(m => m.Group.Name == userGroup), "Name", "Name", Search.Project);
            //    InstrumentSelectList = new SelectList(_context.Instruments.AsNoTracking().Where(m => m.Group == userGroup), "ID", "ID", Search.Instrument);
            //}

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

            //[Required(ErrorMessage = "请选择一个项目组")]
            //[Display(Name = "项目组")]
            //public string Group { get; set; }

            [Required(ErrorMessage = "请选择设备编号")]
            [Display(Name = "设备编号")]
            public string Instrument { get; set; }

            [Display(Name = "项目名称")]
            public string Project { get; set; }
        }

        public enum Category
        {
            [Display(Name = "使用登记")]
            Usage,
            [Display(Name = "故障工单")]
            Malfunction,
        }
    }
}
