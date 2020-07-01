using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using EquipmentManagementSystem.Authorization;
using EquipmentManagementSystem.Data;
using EquipmentManagementSystem.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

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

            if (isAdmin || userGroup == null)
            {
                GroupSelectList = new SelectList(_context.Groups.OrderBy(m => m.Name), "Name", "Name");
                InstrumentSelectList = new SelectList(_context.Instruments.OrderBy(m => m.ID), "ID", "ID");
            }
            else
            {
                GroupSelectList = new SelectList(_context.Groups.Where(m => m.Name == userGroup), "Name", "Name", userGroup);
                InstrumentSelectList = new SelectList(_context.Instruments.Where(m => m.Group == userGroup).OrderBy(m => m.ID), "ID", "ID");
            }
        }

        /// <summary>
        /// 根据设备编号返回该设备的检测项目    
        /// </summary>
        /// <param name="instrumentId">设备编号</param>
        /// <returns>JSON</returns>
        public JsonResult OnGetProjectFilter(string instrumentId)
        {
            return new JsonResult(_context.Instruments.Find(instrumentId).Projects.Split(", "));
        }

        [BindProperty]
        public SearchForm Search { get; set; }

        public SelectList InstrumentSelectList { get; set; }

        public SelectList GroupSelectList { get; set; }

        public SelectList ProjectSelectList { get; set; }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

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

            [Required(ErrorMessage = "请选择一个设备编号")]
            [Display(Name = "设备编号")]
            public string Instrument { get; set; }

            [Required(ErrorMessage = "请选择一个检测项目")]
            [Display(Name = "检测项目")]
            public string Project { get; set; }
        }
    }
}
