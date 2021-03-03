using EquipmentManagementSystem.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Threading.Tasks;

namespace EquipmentManagementSystem.Pages.Records.MaintenanceRecordsOfYuanSu
{
    public class AjaxHelperModel : PageModel
    {
        private readonly IInstrumentRepository _instrumentRepository;
        private readonly IMaintenanceContentRepository _maintenanceContentRepository;

        public AjaxHelperModel(IInstrumentRepository instrumentRepository, IMaintenanceContentRepository maintenanceContentRepository)
        {
            _instrumentRepository = instrumentRepository;
            _maintenanceContentRepository = maintenanceContentRepository;
        }

        public void OnGet()
        {
        }

        /// <summary>
        /// 返回对应设备平台的维护内容
        /// </summary>
        public async Task<JsonResult> OnGetMaintenanceContents(string instrument)
        {
            var platform = (await _instrumentRepository.GetById(instrument)).Platform;
            if (platform == null)
            {
                return new JsonResult("该仪器未设置平台");
            }

            var contents = _maintenanceContentRepository.GetByInstrumentPlatform(platform);

            if (contents.Count == 0)
            {
                return new JsonResult("该设备平台未设置维护内容");
            }

            var result = new JsonResult(contents);

            return result;
        }
    }
}
