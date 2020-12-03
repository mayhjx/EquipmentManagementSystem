using EquipmentManagementSystem.Interfaces;
using EquipmentManagementSystem.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Threading.Tasks;

namespace EquipmentManagementSystem.Pages.Records.UsageRecords
{
    public class EditModel : PageModel
    {
        private readonly IUsageRecordRepository _repo;
        public EditModel(IUsageRecordRepository usageRecordRepository)
        {
            _repo = usageRecordRepository;
        }

        [BindProperty]
        public UsageRecord UsageRecord { get; set; }

        public void OnGet()
        {

        }
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            //var isAuthorized = await _authorizationService.AuthorizeAsync(User, UsageRecordToUpdate, Operations.Update);

            //if (!isAuthorized.Succeeded)
            //{
            //    return Forbid();
            //}

            await _repo.Update(UsageRecord);

            return RedirectToPage("../Index", new { instrumentId = UsageRecord.InstrumentId });
        }
    }
}
