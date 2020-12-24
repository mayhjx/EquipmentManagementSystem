using EquipmentManagementSystem.Authorization;
using EquipmentManagementSystem.Interfaces;
using EquipmentManagementSystem.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Threading.Tasks;

namespace EquipmentManagementSystem.Pages.Records.UsageRecords
{
    public class CreateModel : PageModel
    {
        private readonly IUsageRecordRepository _usageRecordRepo;
        private readonly IProjectRepository _projectRepo;
        private readonly IAuthorizationService _authorizationService;

        public CreateModel(
            IUsageRecordRepository usageRecordRepository,
            IProjectRepository projectRepository,
            IAuthorizationService authorizationHandler)
        {
            _usageRecordRepo = usageRecordRepository;
            _projectRepo = projectRepository;
            _authorizationService = authorizationHandler;
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

            UsageRecord.GroupName = await _projectRepo.GetGroupNameByShortName(UsageRecord.ProjectName);
            UsageRecord.MobilePhase = await _projectRepo.GetMobilePhasesByShortName(UsageRecord.ProjectName);
            UsageRecord.ColumnType = await _projectRepo.GetColumnTypesByShortName(UsageRecord.ProjectName);
            UsageRecord.IonSource = await _projectRepo.GetIonSourcesByShortName(UsageRecord.ProjectName);
            UsageRecord.Detector = await _projectRepo.GetDetectorsByShortName(UsageRecord.ProjectName);

            var isAuthorized = await _authorizationService.AuthorizeAsync(User, UsageRecord, Operations.Create);
            if (!isAuthorized.Succeeded)
            {
                return Forbid();
            }

            string message;
            try
            {
                await _usageRecordRepo.Create(UsageRecord);
                message = "新建成功";
            }
            catch
            {
                // create log
                message = "新建失败，请刷新后重试！";
            }

            return RedirectToPage("../Index", new { instrumentId = UsageRecord.InstrumentId, statusMessage = message });
        }
    }
}
