using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using EquipmentManagementSystem.Authorization;
using EquipmentManagementSystem.Data;
using EquipmentManagementSystem.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace EquipmentManagementSystem.Pages.UsageRecords
{
    [AllowAnonymous]
    public class IndexModel : BasePageModel
    {
        public IndexModel(EquipmentContext context,
            UserManager<User> userManager,
            IAuthorizationService authorizationService)
            : base(context, userManager, authorizationService)
        {
        }

        [BindProperty]
        public SearchForm Search { get; set; }

        public IList<UsageRecord> UsageRecords { get; set; }

        public SelectList ProjectSelectList { get; set; }

        public async Task OnGetAsync()
        {
            var usageRecord = from record in _context.UsageRecords
                            .AsNoTracking()
                            .Include(record => record.Instrument)
                            .Include(record => record.Project)
                                .ThenInclude(p => p.Group)
                              select record;

            var isAuthorized = User.IsInRole(Constants.DirectorRole) || User.IsInRole(Constants.ManagerRole);

            var projects = _context.Projects.AsNoTracking().Include(p => p.Group);

            if (User.Identity.IsAuthenticated && !isAuthorized)
            {
                var currentUserGroup = (await _userManager.GetUserAsync(User)).Group;

                var projectsOfcurrentUserGroup = await projects.Where(p => p.Group.Name == currentUserGroup).Select(p => p.Name).ToListAsync();

                ProjectSelectList = new SelectList(projects.Where(p => p.Group.Name == currentUserGroup).OrderBy(p => p.Name), "Name", "Name");

                // 显示当前用户所属项目组的使用登记
                usageRecord = from record in usageRecord
                              where projectsOfcurrentUserGroup.Contains(record.ProjectName)
                              select record;
            }
            else
            {
                ProjectSelectList = new SelectList(projects.OrderBy(p => p.Name), "Name", "Name");
            }

            UsageRecords = await usageRecord.OrderBy(m => m.BeginTimeOfTest).ToListAsync();
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

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var usageRecord = _context.UsageRecords
                                        .AsNoTracking()
                                        .Include(record => record.Instrument)
                                        .Include(record => record.Project)
                                            .ThenInclude(p => p.Group)
                                        .Where(u => u.InstrumentId == Search.Instrument)
                                        .Where(u => u.ProjectName == Search.Project);

            var isAuthorized = User.IsInRole(Constants.DirectorRole) || User.IsInRole(Constants.ManagerRole);

            // 生成项目选项
            if (User.Identity.IsAuthenticated)
            {
                var currentUserGroup = (await _userManager.GetUserAsync(User)).Group;

                ProjectSelectList = new SelectList(_context.Projects.AsNoTracking()
                                                                    .Include(p => p.Group)
                                                                    .Where(p => p.Group.Name == currentUserGroup)
                                                                    .OrderBy(p => p.Name), "Name", "Name");
            }
            else
            {
                ProjectSelectList = new SelectList(_context.Projects.AsNoTracking().OrderBy(p => p.Name), "Name", "Name");
            }

            UsageRecords = await usageRecord.OrderBy(m => m.BeginTimeOfTest).ToListAsync();

            return Page();
        }

        public class SearchForm
        {
            [Display(Name = "年月")]
            public string Month { get; set; }

            [Display(Name = "检测项目")]
            public string Project { get; set; }

            [Display(Name = "设备编号")]
            public string Instrument { get; set; }
        }
    }
}
