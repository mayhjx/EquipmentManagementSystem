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

            //var isAuthorized = await _authorizationService.AuthorizeAsync(User, UsageRecordToUpdate, Operations.Update);

            //if (!isAuthorized.Succeeded)
            //{
            //    return Forbid();
            //}

            string message;
            try
            {
                await _repo.Update(UsageRecord);
                message = "修改成功";
            }
            catch
            {
                // create log
                message = "修改失败，请刷新后重试！";
            }

            return RedirectToPage("../Index", new { instrumentId = UsageRecord.InstrumentId, statusMessage = message });
        }
    }
}
