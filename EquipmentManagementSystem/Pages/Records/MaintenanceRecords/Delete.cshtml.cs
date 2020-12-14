using EquipmentManagementSystem.Interfaces;
using EquipmentManagementSystem.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace EquipmentManagementSystem.Pages.MaintenanceRecords
{
    public class DeleteModel : PageModel
    {
        private readonly IMaintenanceRecordRepository _maintenanceRecordRepository;
        public DeleteModel(IMaintenanceRecordRepository maintenanceRecordRepository)
        {
            _maintenanceRecordRepository = maintenanceRecordRepository;
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

            //var isAuthorized = await _authorizationService.AuthorizeAsync(User, MaintenanceRecord, Operations.Delete);

            //if (!isAuthorized.Succeeded)
            //{
            //    return Forbid();
            //}

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
