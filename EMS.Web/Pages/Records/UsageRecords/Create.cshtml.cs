using EquipmentManagementSystem.Authorization;
using EquipmentManagementSystem.Data;
using EquipmentManagementSystem.Models;
using EquipmentManagementSystem.Models.Record;
using EquipmentManagementSystem.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EquipmentManagementSystem.Pages.Records.UsageRecords
{
    public class CreateModel : BasePageModel
    {
        private readonly IUsageRecordService _usageRecordService;
        public CreateModel(EquipmentContext context,
            UserManager<User> userManager,
            IAuthorizationService authorizationService,
            IUsageRecordService usageRecordService)
            : base(context, userManager, authorizationService)
        {
            _usageRecordService = usageRecordService;
        }

        [BindProperty]
        public List<Column> Columns { get; set; }

        [BindProperty]
        public List<VacuumDegree> VacuumDegrees { get; set; }

        [BindProperty]
        public List<Test> Tests { get; set; }

        public IActionResult OnGet()
        {
            // 获取项目名选项
            // 获取选择的项目的色谱柱，test，真空度的个数
            PopulateProjectDropDownList(_context);

            return Page();
        }

        public async Task<JsonResult> OnGetFillLatestRecord(string projectName, string instrumentId)
        {
            UsageRecord = await _usageRecordService.GetLastestRecordByProjectAndInstrumentAsync(projectName, instrumentId);
            return new JsonResult(UsageRecord);
        }

        // 用户选择一个项目后
        // 通过Ajax取得该项目对应的仪器编号，色谱柱数量
        // 更新页面

        //public JsonResult OnGetProjectConfig(string projectName)
        //{
        //      _projectService.GetColumnNumber(projectName);
        //    return;
        //}

        public JsonResult OnGetInstrumentFilter(string projectName)
        {
            var result = new JsonResult(from m in _context.Instruments
                                        where m.Projects.IndexOf(projectName) >= 0
                                        select m.ID);
            return result;
        }

        [BindProperty]
        public UsageRecord UsageRecord { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                PopulateProjectDropDownList(_context);
                return Page();
            }

            var isAuthorized = await _authorizationService.AuthorizeAsync(User, UsageRecord, Operations.Create);

            if (!isAuthorized.Succeeded)
            {
                return Forbid();
            }

            UsageRecord.SetColumnInfo(Columns);
            UsageRecord.SetTestInfo(Tests);
            UsageRecord.SetVacuumDegreeInfo(VacuumDegrees);

            await _usageRecordService.AddAsync(UsageRecord);

            return RedirectToPage("../Index");
        }
    }
}
