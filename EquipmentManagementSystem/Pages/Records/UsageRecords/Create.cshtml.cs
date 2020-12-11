﻿using EquipmentManagementSystem.Interfaces;
using EquipmentManagementSystem.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Threading.Tasks;

namespace EquipmentManagementSystem.Pages.Records.UsageRecords
{
    public class CreateModel : PageModel
    {
        private readonly IUsageRecordRepository _usageRecordRepo;
        private readonly IProjectRepository _projectRepo;
        public CreateModel(IUsageRecordRepository usageRecordRepository, IProjectRepository projectRepository)
        {
            _usageRecordRepo = usageRecordRepository;
            _projectRepo = projectRepository;
        }

        public void OnGet()
        {
        }

        [BindProperty]
        public UsageRecord UsageRecord { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            //UsageRecord.ProjectId = _context.Projects.FirstOrDefaultAsync(p => p.Name == UsageRecord.ProjectName).Result.Id;
            //var isAuthorized = await _authorizationService.AuthorizeAsync(User, UsageRecord, Operations.Create);
            //if (!isAuthorized.Succeeded)
            //{
            //    return Forbid();
            //}

            UsageRecord.GroupName = await _projectRepo.GetGroupNameByShortName(UsageRecord.ProjectName);
            UsageRecord.MobilePhase = await _projectRepo.GetMobilePhasesByShortName(UsageRecord.ProjectName);
            UsageRecord.ColumnType = await _projectRepo.GetColumnTypesByShortName(UsageRecord.ProjectName);
            UsageRecord.IonSource = await _projectRepo.GetIonSourcesByShortName(UsageRecord.ProjectName);
            UsageRecord.Detector = await _projectRepo.GetDetectorsByShortName(UsageRecord.ProjectName);

            string message;
            try
            {
                await _usageRecordRepo.Create(UsageRecord);
                message = "新建成功";
            }
            catch (Exception ex)
            {
                // create log
                message = "新建失败，请刷新后重试！";
            }

            return RedirectToPage("../Index", new { instrumentId = UsageRecord.InstrumentId, statusMessage = message });
        }
    }
}
