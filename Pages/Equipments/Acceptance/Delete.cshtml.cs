using System;
using System.Threading.Tasks;
using EquipmentManagementSystem.Models;
using EquipmentManagementSystem.Utilities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace EquipmentManagementSystem.Pages.Equipments.Acceptance
{
    public class DeleteModel : PageModel
    {
        private readonly EquipmentManagementSystem.Data.EquipmentContext _context;

        public DeleteModel(EquipmentManagementSystem.Data.EquipmentContext context)
        {
            _context = context;
        }

        [BindProperty]
        public InstrumentAcceptance InstrumentAcceptance { get; set; }

        public IActionResult OnGet(int? id)
        {
            return RedirectToPage("Index");
        }

        public IActionResult OnPost(int? id)
        {
            return RedirectToPage("Index");
        }

        public async Task<IActionResult> OnPostDeleteAsync(int id)
        {
            var instrumentAcceptanceToDelete = await _context.InstrumentAcceptances
                                        .FirstOrDefaultAsync(m => m.Id == id);

            if (instrumentAcceptanceToDelete == null)
            {
                return new JsonResult("未找到该记录");
            }

            //var isAuthorized = await _authorizationService.AuthorizeAsync(User, InstrumentAcceptance, Operations.Delete);

            //if (!isAuthorized.Succeeded)
            //{
            //    return new JsonResult("权限不足");
            //}

            try
            {
                // 删除已上传的文件
                FileHelpers.DeleteOlderFile(instrumentAcceptanceToDelete.FeasibilityReportFilePath);
                FileHelpers.DeleteOlderFile(instrumentAcceptanceToDelete.ConfigurationListFilePath);
                FileHelpers.DeleteOlderFile(instrumentAcceptanceToDelete.PuchaseRequisitionFilePath);
                FileHelpers.DeleteOlderFile(instrumentAcceptanceToDelete.InstallationNoteFilePath);
                FileHelpers.DeleteOlderFile(instrumentAcceptanceToDelete.InventoryCertificateFilePath);
                FileHelpers.DeleteOlderFile(instrumentAcceptanceToDelete.FactoryAcceptanceReportFilePath);
                FileHelpers.DeleteOlderFile(instrumentAcceptanceToDelete.ServiceReportFilePath);
                FileHelpers.DeleteOlderFile(instrumentAcceptanceToDelete.TrainingSignInFormFilePath);
                FileHelpers.DeleteOlderFile(instrumentAcceptanceToDelete.EvaluationReportFilePath);
                FileHelpers.DeleteOlderFile(instrumentAcceptanceToDelete.EquipmentResumeFilePath);
                FileHelpers.DeleteOlderFile(instrumentAcceptanceToDelete.EquipmentFilesListFilePath);
                FileHelpers.DeleteOlderFile(instrumentAcceptanceToDelete.EquipmentCertificateFilePath);
                FileHelpers.DeleteOlderFile(instrumentAcceptanceToDelete.FactoryProductionLicenseFilePath);
                FileHelpers.DeleteOlderFile(instrumentAcceptanceToDelete.BusinessLicenseFilePath);
                FileHelpers.DeleteOlderFile(instrumentAcceptanceToDelete.MedicalDeviceRegistrationCertificateFilePath);
                FileHelpers.DeleteOlderFile(instrumentAcceptanceToDelete.EquipmentCalibrationReportFilePath);
                FileHelpers.DeleteOlderFile(instrumentAcceptanceToDelete.EquipmentAcceptanceReportFilePath);

                _context.InstrumentAcceptances.Remove(instrumentAcceptanceToDelete);
                await _context.SaveChangesAsync();
                return new JsonResult("删除成功！");
            }
            catch (Exception ex)
            {
                return new JsonResult($"删除失败，请重试。错误信息：{ex}");
            }

        }

    }
}
