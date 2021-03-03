using EquipmentManagementSystem.Authorization;
using EquipmentManagementSystem.Interfaces;
using EquipmentManagementSystem.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace EquipmentManagementSystem.Pages.MaintenanceRecordsOfYuanSu
{
    public class DeleteModel : PageModel
    {
        private readonly IMaintenanceRecordRepository _maintenanceRecordRepository;
        private readonly IAuthorizationService _authorizationService;

        public DeleteModel(IMaintenanceRecordRepository maintenanceRecordRepository,
            IAuthorizationService authorizationService)
        {
            _maintenanceRecordRepository = maintenanceRecordRepository;
            _authorizationService = authorizationService;

        }

        public void OnGet()
        {

        }

        [BindProperty]
        public MaintenanceRecord MaintenanceRecord { get; set; }

        public async Task<JsonResult> OnPostAsync(int id)
        {
            MaintenanceRecord = await _maintenanceRecordRepository.GetById(id);

            if (MaintenanceRecord == null)
            {
                return new JsonResult("记录未找到，请刷新确认！");
            }

            var isAuthorized = await _authorizationService.AuthorizeAsync(User, MaintenanceRecord, Operations.Delete);

            if (!isAuthorized.Succeeded)
            {
                return new JsonResult("无权限！");
            }

            try
            {
                await _maintenanceRecordRepository.Delete(MaintenanceRecord);
                return new JsonResult("删除成功！");
            }
            catch (DbUpdateException)
            {
                return new JsonResult("删除失败，请刷新后重试！");
            }
        }
    }
}
