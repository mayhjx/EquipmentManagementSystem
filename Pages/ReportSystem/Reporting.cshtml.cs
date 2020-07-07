using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using EquipmentManagementSystem.Authorization;
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
            var isAdmin = User.IsInRole(Constants.ManagerRole) || User.IsInRole(Constants.DirectorRole);
            var userGroup = _userManager.GetUserAsync(User).Result?.Group;

            if (isAdmin)
            {
                GroupSelectList = new SelectList(_context.Groups.OrderBy(m => m.Name), "Name", "Name");
            }
            else
            {
                GroupSelectList = new SelectList(_context.Groups.Where(m => m.Name == userGroup), "Name", "Name", userGroup);
            }
        }

        /// <summary>
        /// 根据项目组返回设备编号
        /// </summary>
        /// <param name="groupName">项目组名</param>
        /// <returns></returns>
        public JsonResult OnGetInstrumentFilter(string groupName)
        {
            var Result = new JsonResult(from m in _context.Instruments
                                        where (m.Group == groupName)
                                        select m.ID);
            return Result;
        }

        /// <summary>
        /// 根据项目组返回检测项目    
        /// </summary>
        /// <param name="groupName">项目组名</param>
        /// <returns>JSON</returns>
        public JsonResult OnGetProjectFilter(string groupName)
        {
            var Result = new JsonResult(from project in _context.Projects
                                        .AsNoTracking()
                                        .Include(project => project.Group)
                                        where (project.Group.Name == groupName)
                                        select project.Name);
            return Result;
        }

        [BindProperty]
        public SearchForm Search { get; set; }

        [BindProperty]
        public IList<UsageRecord> UsageRecords { get; set; }

        public SelectList InstrumentSelectList { get; set; }

        public SelectList GroupSelectList { get; set; }

        public SelectList ProjectSelectList { get; set; }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            UsageRecords = (from record in _context.UsageRecords
                            .Include(record => record.Instrument)
                            .Include(record => record.Project)
                            where record.Instrument.Group == Search.Group
                            where record.InstrumentId == Search.Instrument
                            where record.ProjectName == Search.Project
                            where record.BeginTimeOfTest > Search.BeginTime
                            where record.BeginTimeOfTest < Search.EndTime
                            select record)
                            .ToList();

            var isAdmin = User.IsInRole(Constants.ManagerRole) || User.IsInRole(Constants.DirectorRole);
            var userGroup = _userManager.GetUserAsync(User).Result?.Group;

            if (isAdmin)
            {
                GroupSelectList = new SelectList(_context.Groups.OrderBy(m => m.Name), "Name", "Name");
            }
            else
            {
                GroupSelectList = new SelectList(_context.Groups.Where(m => m.Name == userGroup), "Name", "Name", userGroup);
            }

            InstrumentSelectList = new SelectList(_context.Instruments.Where(m => m.Group == Search.Group), "ID", "ID");
            ProjectSelectList = new SelectList(_context.Instruments.Find(Search.Instrument).Projects.Split(", ").ToList());

            return Page();
        }

        public class SearchForm
        {
            [Required(ErrorMessage = "请输入起始时间")]
            [Display(Name = "起始时间")]
            [DataType(DataType.Date)]
            public DateTime BeginTime { get; set; }

            [Required(ErrorMessage = "请输入结束时间")]
            [Display(Name = "结束时间")]
            [DataType(DataType.Date)]
            public DateTime EndTime { get; set; }

            [Required(ErrorMessage = "请选择一个项目组")]
            [Display(Name = "项目组")]
            public string Group { get; set; }

            [Required(ErrorMessage = "请选择一个仪器编号")]
            [Display(Name = "仪器编号")]
            public string Instrument { get; set; }

            [Required(ErrorMessage = "请选择一个项目名称")]
            [Display(Name = "项目名称")]
            public string Project { get; set; }
        }
    }
}
