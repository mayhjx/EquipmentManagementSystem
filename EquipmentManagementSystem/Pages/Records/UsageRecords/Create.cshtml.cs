using EquipmentManagementSystem.Interfaces;
using EquipmentManagementSystem.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Threading.Tasks;

namespace EquipmentManagementSystem.Pages.Records.UsageRecords
{
    public class CreateModel : PageModel
    {
        private readonly IUsageRecordRepository _repo;
        public CreateModel(IUsageRecordRepository usageRecordRepository)
        {
            _repo = usageRecordRepository;
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

            await _repo.Create(UsageRecord);

            return RedirectToPage("../Index", new { instrumentId = UsageRecord.InstrumentId });
        }
    }
}
