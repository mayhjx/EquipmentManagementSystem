using EquipmentManagementSystem.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace EquipmentManagementSystem.Pages.Records.UsageRecords
{
    public class AjaxHelperModel : PageModel
    {
        private readonly IUsageRecordRepository _usageRecordRepository;

        public AjaxHelperModel(IUsageRecordRepository usageRecordRepository)
        {
            _usageRecordRepository = usageRecordRepository;
        }

        public void OnGet()
        {
        }

        public JsonResult OnGetLatestRecordOfProject(string project, string instrumentId)
        {
            // 如果没有记录，返回null
            var latestRecord = _usageRecordRepository.GetLatestRecordOfProject(project, instrumentId);
            return new JsonResult(latestRecord);
        }
    }
}
