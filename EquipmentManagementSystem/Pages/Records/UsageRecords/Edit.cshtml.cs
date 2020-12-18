using EquipmentManagementSystem.Authorization;
using EquipmentManagementSystem.Interfaces;
using EquipmentManagementSystem.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Threading.Tasks;

namespace EquipmentManagementSystem.Pages.Records.UsageRecords
{
    public class EditModel : PageModel
    {
        private readonly IProjectRepository _projectRepo;
        private readonly IUsageRecordRepository _usageRecordRepo;
        private readonly IAuthorizationService _authorizationService;

        public EditModel(IUsageRecordRepository usageRecordRepository,
            IProjectRepository projectRepository,
            IAuthorizationService authorizationService)
        {
            _projectRepo = projectRepository;
            _usageRecordRepo = usageRecordRepository;
            _authorizationService = authorizationService;
        }

        public void OnGet()
        {
        }

        [BindProperty]
        public UsageRecord UsageRecord { get; set; }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            //if (!ModelState.IsValid)
            //{
            //    return Page();
            //}

            UsageRecord = await _usageRecordRepo.GetById(id);

            if (UsageRecord == null)
            {
                return NotFound();
            }

            var isAuthorized = await _authorizationService.AuthorizeAsync(User, UsageRecord, Operations.Update);

            if (!isAuthorized.Succeeded)
            {
                return Forbid();
            }

            string message;
            if (await TryUpdateModelAsync(UsageRecord, "usageRecord"))
            {
                UsageRecord.GroupName = await _projectRepo.GetGroupNameByShortName(UsageRecord.ProjectName);
                UsageRecord.MobilePhase = await _projectRepo.GetMobilePhasesByShortName(UsageRecord.ProjectName);
                UsageRecord.ColumnType = await _projectRepo.GetColumnTypesByShortName(UsageRecord.ProjectName);
                UsageRecord.IonSource = await _projectRepo.GetIonSourcesByShortName(UsageRecord.ProjectName);
                UsageRecord.Detector = await _projectRepo.GetDetectorsByShortName(UsageRecord.ProjectName);

                await _usageRecordRepo.Update(UsageRecord);
                 message = "修改成功";

                return RedirectToPage("../Index", new { instrumentId = UsageRecord.InstrumentId, statusMessage = message });
            }

             message = "修改失败，请刷新后重试！";
            return RedirectToPage("../Index", new { instrumentId = UsageRecord.InstrumentId, statusMessage = message });
        }
    }
}
