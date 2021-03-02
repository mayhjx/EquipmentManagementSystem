using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using EquipmentManagementSystem.Authorization;
using EquipmentManagementSystem.Interfaces;
using EquipmentManagementSystem.Models;

namespace EquipmentManagementSystem.Pages.Records.UsageRecordsOfYuanSu
{
    public class CreateModel : PageModel
    {
        private readonly IUsageRecordOfYuanSuRepository _usageRecordOfYuanSuRepo;
        private readonly IProjectRepository _projectRepo;

        public CreateModel(IUsageRecordOfYuanSuRepository usageRecordOfYuanSuRepository,
            IProjectRepository projectRepository)
        {
            _usageRecordOfYuanSuRepo = usageRecordOfYuanSuRepository;
            _projectRepo = projectRepository;
        }

        public void OnGet()
        {
        }

        [BindProperty]
        public UsageRecordOfYuanSu UsageRecordOfYuanSu { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            if(!ModelState.IsValid)
            {
                return Page();
            }

            UsageRecordOfYuanSu.GroupName = await _projectRepo.GetGroupNameByShortName(UsageRecordOfYuanSu.ProjectName);

            string message;
            try
            {
                await _usageRecordOfYuanSuRepo.Create(UsageRecordOfYuanSu);
                message = "新建成功";
            }
            catch
            {
                // create log
                message = "新建失败，请刷新后重试！";
            }

            return RedirectToPage("../Index", new { instrumentId = UsageRecordOfYuanSu.InstrumentID, statusMessage = message });
        }
    }
}
