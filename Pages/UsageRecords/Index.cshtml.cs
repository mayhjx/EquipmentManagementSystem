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
                            // 默认显示使用中的记录
                            .Where(record => record.EndTime == null)
                              select record;

            var isAuthorized = User.IsInRole(Constants.DirectorRole) || User.IsInRole(Constants.ManagerRole);

            var projects = _context.Projects.AsNoTracking().Include(p => p.Group);

            if (User.Identity.IsAuthenticated && !isAuthorized)
            {
                var currentUserGroup = (await _userManager.GetUserAsync(User)).Group;

                var projectsOfcurrentUserGroup = await projects.AsNoTracking().Where(p => p.Group.Name == currentUserGroup).Select(p => p.Name).ToListAsync();

                ProjectSelectList = new SelectList(projects.AsNoTracking().Where(p => p.Group.Name == currentUserGroup).OrderBy(p => p.Name), "Name", "Name");

                // 显示当前用户所属项目组的使用登记
                usageRecord = from record in usageRecord
                              where projectsOfcurrentUserGroup.Contains(record.ProjectName)
                              select record;
            }
            else
            {
                ProjectSelectList = new SelectList(projects.AsNoTracking().OrderBy(p => p.Name), "Name", "Name");
            }

            UsageRecords = await usageRecord.OrderByDescending(m => m.BeginTimeOfTest).ToListAsync();
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

            var usageRecord = from record in _context.UsageRecords
                            .AsNoTracking()
                            .Include(record => record.Instrument)
                            .Include(record => record.Project)
                                .ThenInclude(p => p.Group)
                              select record;

            // 显示使用中的记录
            if (Search.Status == Status.Using)
            {
                usageRecord = usageRecord.Where(record => record.EndTime == null);
            }

            if (Search.Instrument != null)
            {
                usageRecord = usageRecord.Where(u => u.InstrumentId == Search.Instrument);
            }

            if (Search.Project != null)
            {
                usageRecord = usageRecord.Where(u => u.ProjectName == Search.Project);
            }

            var projects = _context.Projects.AsNoTracking().Include(p => p.Group);

            var isAuthorized = User.IsInRole(Constants.DirectorRole) || User.IsInRole(Constants.ManagerRole);

            if (User.Identity.IsAuthenticated && !isAuthorized)
            {
                var currentUserGroup = (await _userManager.GetUserAsync(User)).Group;

                var projectsOfcurrentUserGroup = await projects.AsNoTracking().Where(p => p.Group.Name == currentUserGroup).Select(p => p.Name).ToListAsync();

                ProjectSelectList = new SelectList(projects.AsNoTracking().Where(p => p.Group.Name == currentUserGroup).OrderBy(p => p.Name), "Name", "Name");

                // 显示当前用户所属项目组的使用登记
                usageRecord = from record in usageRecord
                              where projectsOfcurrentUserGroup.Contains(record.ProjectName)
                              select record;
            }
            else
            {
                ProjectSelectList = new SelectList(projects.AsNoTracking().OrderBy(p => p.Name), "Name", "Name");
            }

            UsageRecords = await usageRecord.OrderByDescending(m => m.BeginTimeOfTest).ToListAsync();

            return Page();
        }

        public class SearchForm
        {
            [Display(Name = "")]
            public Status Status { get; set; }

            [Display(Name = "检测项目")]
            public string Project { get; set; }

            [Display(Name = "设备编号")]
            public string Instrument { get; set; }
        }

        public enum Status
        {
            [Display(Name = "使用中")]
            Using,
            [Display(Name = "所有")]
            All,
        }
    }
}
