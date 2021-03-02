using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using EquipmentManagementSystem.Interfaces;
using EquipmentManagementSystem.Models;
using Microsoft.AspNetCore.Authorization;

namespace EquipmentManagementSystem.Pages.Records.UsageRecordsOfYuanSu
{
    public class EditModel : PageModel
    {
        private readonly IUsageRecordOfYuanSuRepository _usageRecordOfYuanSuRepo;
        private readonly IProjectRepository _projectRepo;
        private readonly IAuthorizationService _authorizationService;
        public EditModel(IAuthorizationService authorizationService,
            IProjectRepository projectRepository,
            IUsageRecordOfYuanSuRepository usageRecordOfYuanSuRepository)
        {
            _authorizationService = authorizationService;
            _projectRepo = projectRepository;
            _usageRecordOfYuanSuRepo = usageRecordOfYuanSuRepository;
        }

        public void OnGet()
        {
        }

        [BindProperty]
        public UsageRecordOfYuanSu UsageRecordOfYuanSu { get; set; }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            UsageRecordOfYuanSu = await _usageRecordOfYuanSuRepo.GetById(id);

            if (UsageRecordOfYuanSu == null)
            {
                return NotFound();
            }

            string message;
            if (await TryUpdateModelAsync<UsageRecordOfYuanSu>(UsageRecordOfYuanSu, "usageRecord"))
            {
                await _usageRecordOfYuanSuRepo.Update(UsageRecordOfYuanSu);
                message = "修改成功";

                return RedirectToPage("../Index", new { instrumentId = UsageRecordOfYuanSu.InstrumentID, statusMessage = message });
            }

            message = "修改失败，请刷新后重试！";
            return RedirectToPage("../Index", new { instrumentId = UsageRecordOfYuanSu.InstrumentID, statusMessage = message });
        }
    }
}
