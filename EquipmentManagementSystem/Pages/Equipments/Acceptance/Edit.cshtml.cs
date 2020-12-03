using System;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Threading.Tasks;
using EquipmentManagementSystem.Authorization;
using EquipmentManagementSystem.Data;
using EquipmentManagementSystem.Models;
using EquipmentManagementSystem.Pages.Instruments;
using EquipmentManagementSystem.Utilities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace EquipmentManagementSystem.Pages.Equipments.Acceptance
{
    public class EditModel : BasePageModel
    {
        private readonly IWebHostEnvironment _env;
        private readonly long _fileSizeLimit;
        private readonly string _uploadFilePath;

        public EditModel(EquipmentContext context,
            IWebHostEnvironment env,
            UserManager<User> userManager,
            IAuthorizationService authorizationService,
            IConfiguration config)
            : base(context, userManager, authorizationService)
        {
            _env = env;
            _fileSizeLimit = config.GetValue<long>("FileSizeLimit");
            _uploadFilePath = Path.Combine(config.GetValue<string>("StoredFilesPath"), "InstrumentAcceptance");
            Directory.CreateDirectory(_uploadFilePath);
        }

        [BindProperty]
        public InstrumentAcceptance InstrumentAcceptance { get; set; }

        [BindProperty]
        public Upload FileUpload { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            InstrumentAcceptance = await _context.InstrumentAcceptances
                .FirstOrDefaultAsync(m => m.Id == id);

            if (InstrumentAcceptance == null)
            {
                return NotFound();
            }

            var isAuthorized = await _authorizationService.AuthorizeAsync(
                                                        User, InstrumentAcceptance,
                                                        Operations.Update);
            if (!isAuthorized.Succeeded)
            {
                return Forbid();
            }

            return Page();
        }

        //public IActionResult OnGetDownload(string filePath)
        //{
        //    var fileName = FileHelpers.RemoveGuidStringInFileName(filePath);
        //    return PhysicalFile(Path.Combine(_env.ContentRootPath, filePath), MediaTypeNames.Application.Octet, fileName);
        //}

        public async Task<IActionResult> OnPostAsync(int id)
        {
            InstrumentAcceptance = await _context.InstrumentAcceptances.FirstOrDefaultAsync(m => m.Id == id);

            if (InstrumentAcceptance == null)
            {
                return NotFound();
            }

            var isAuthorized = await _authorizationService.AuthorizeAsync(
                                                       User, InstrumentAcceptance,
                                                       Operations.Update);
            if (!isAuthorized.Succeeded)
            {
                return Forbid();
            }

            if (await TryUpdateModelAsync<InstrumentAcceptance>(
                InstrumentAcceptance,
                "InstrumentAcceptance",
                i => i.IsDemo, i => i.InstrumentID,
                i => i.EstimatedArrivalDate, i => i.ArrivalDate,
                i => i.IsInventoryComplete, i => i.InventoryRemark,
                i => i.InstallationDate, i => i.InstallationRemark,
                i => i.IsFactoryAcceptance, i => i.FactoryAcceptanceDate, i => i.FactoryAcceptanceRemark,
                i => i.IsTrainingUseAndMaintenance,
                i => i.IsSelfBuilt, i => i.IsEngineerAssistance, i => i.MethodConstructionRemark,
                i => i.IsAcceptance, i => i.ItemAcceptanceDate, i => i.EquipmentAcceptanceDate))
            {
                #region 报告上传
                // 可行性报告
                if (FileUpload.FeasibilityReportFile != null)
                {
                    var formFileContent = await FileHelpers.ProcessFormFile<Upload>(
                                            FileUpload.FeasibilityReportFile, ModelState, _fileSizeLimit);

                    if (!ModelState.IsValid)
                    {
                        return Page();
                    }

                    // 如果文件名包含+的话，前端查看文件时会出现404.11 双重转义错误
                    string fileName = Path.GetFileName(FileUpload.FeasibilityReportFile.FileName);
                    string filePath = FileHelpers.CreateFilePath(_uploadFilePath, fileName);

                    FileHelpers.SaveFile(formFileContent, filePath);
                    FileHelpers.DeleteOlderFile(InstrumentAcceptance.FeasibilityReportFilePath);

                    InstrumentAcceptance.FeasibilityReportFilePath = filePath;
                    InstrumentAcceptance.FeasibilityReportFileName = fileName;
                }


                // 设备配置清单
                if (FileUpload.ConfigurationListFile != null)
                {
                    var formFileContent = await FileHelpers.ProcessFormFile<Upload>(
                                            FileUpload.ConfigurationListFile, ModelState, _fileSizeLimit);

                    if (!ModelState.IsValid)
                    {
                        return Page();
                    }

                    string fileName = Path.GetFileName(FileUpload.ConfigurationListFile.FileName);
                    string filePath = FileHelpers.CreateFilePath(_uploadFilePath, fileName);

                    FileHelpers.SaveFile(formFileContent, filePath);
                    FileHelpers.DeleteOlderFile(InstrumentAcceptance.ConfigurationListFilePath);

                    InstrumentAcceptance.ConfigurationListFilePath = filePath;
                    InstrumentAcceptance.ConfigurationListFileName = fileName;
                }

                // 设备采购清单
                if (FileUpload.PuchaseRequisitionFile != null)
                {
                    var formFileContent = await FileHelpers.ProcessFormFile<Upload>(
                                            FileUpload.PuchaseRequisitionFile, ModelState, _fileSizeLimit);

                    if (!ModelState.IsValid)
                    {
                        return Page();
                    }

                    string fileName = Path.GetFileName(FileUpload.PuchaseRequisitionFile.FileName);
                    string filePath = FileHelpers.CreateFilePath(_uploadFilePath, fileName);

                    FileHelpers.SaveFile(formFileContent, filePath);
                    FileHelpers.DeleteOlderFile(InstrumentAcceptance.PuchaseRequisitionFilePath);

                    InstrumentAcceptance.PuchaseRequisitionFilePath = filePath;
                    InstrumentAcceptance.PuchaseRequisitionFileName = fileName;
                }

                // 设备安装说明
                if (FileUpload.InstallationNoteFile != null)
                {
                    var formFileContent = await FileHelpers.ProcessFormFile<Upload>(
                                            FileUpload.InstallationNoteFile, ModelState, _fileSizeLimit);

                    if (!ModelState.IsValid)
                    {
                        return Page();
                    }

                    string fileName = Path.GetFileName(FileUpload.InstallationNoteFile.FileName);
                    string filePath = FileHelpers.CreateFilePath(_uploadFilePath, fileName);

                    FileHelpers.SaveFile(formFileContent, filePath);
                    FileHelpers.DeleteOlderFile(InstrumentAcceptance.InstallationNoteFilePath);

                    InstrumentAcceptance.InstallationNoteFilePath = filePath;
                    InstrumentAcceptance.InstallationNoteFileName = fileName;
                }

                // 清点证明
                if (FileUpload.InventoryCertificateFile != null)
                {
                    var formFileContent = await FileHelpers.ProcessFormFile<Upload>(
                                            FileUpload.InventoryCertificateFile, ModelState, _fileSizeLimit);

                    if (!ModelState.IsValid)
                    {
                        return Page();
                    }

                    string fileName = Path.GetFileName(FileUpload.InventoryCertificateFile.FileName);
                    string filePath = FileHelpers.CreateFilePath(_uploadFilePath, fileName);

                    FileHelpers.SaveFile(formFileContent, filePath);
                    FileHelpers.DeleteOlderFile(InstrumentAcceptance.InventoryCertificateFilePath);

                    InstrumentAcceptance.InventoryCertificateFilePath = filePath;
                    InstrumentAcceptance.InventoryCertificateFileName = fileName;
                }

                // 设备调试验收证明
                if (FileUpload.FactoryAcceptanceReportFile != null)
                {
                    var formFileContent = await FileHelpers.ProcessFormFile<Upload>(
                                            FileUpload.FactoryAcceptanceReportFile, ModelState, _fileSizeLimit);

                    if (!ModelState.IsValid)
                    {
                        return Page();
                    }

                    string fileName = Path.GetFileName(FileUpload.FactoryAcceptanceReportFile.FileName);
                    string filePath = FileHelpers.CreateFilePath(_uploadFilePath, fileName);

                    FileHelpers.SaveFile(formFileContent, filePath);
                    FileHelpers.DeleteOlderFile(InstrumentAcceptance.FactoryAcceptanceReportFilePath);

                    InstrumentAcceptance.FactoryAcceptanceReportFilePath = filePath;
                    InstrumentAcceptance.FactoryAcceptanceReportFileName = fileName;
                }

                // 服务报告
                if (FileUpload.ServiceReportFile != null)
                {
                    var formFileContent = await FileHelpers.ProcessFormFile<Upload>(
                                            FileUpload.ServiceReportFile, ModelState, _fileSizeLimit);

                    if (!ModelState.IsValid)
                    {
                        return Page();
                    }

                    string fileName = Path.GetFileName(FileUpload.ServiceReportFile.FileName);
                    string filePath = FileHelpers.CreateFilePath(_uploadFilePath, fileName);

                    FileHelpers.SaveFile(formFileContent, filePath);
                    FileHelpers.DeleteOlderFile(InstrumentAcceptance.ServiceReportFilePath);

                    InstrumentAcceptance.ServiceReportFilePath = filePath;
                    InstrumentAcceptance.ServiceReportFileName = fileName;
                }

                // 培训签到表
                if (FileUpload.TrainingSignInFormFile != null)
                {
                    var formFileContent = await FileHelpers.ProcessFormFile<Upload>(
                                            FileUpload.TrainingSignInFormFile, ModelState, _fileSizeLimit);

                    if (!ModelState.IsValid)
                    {
                        return Page();
                    }

                    string fileName = Path.GetFileName(FileUpload.TrainingSignInFormFile.FileName);
                    string filePath = FileHelpers.CreateFilePath(_uploadFilePath, fileName);

                    FileHelpers.SaveFile(formFileContent, filePath);
                    FileHelpers.DeleteOlderFile(InstrumentAcceptance.TrainingSignInFormFilePath);

                    InstrumentAcceptance.TrainingSignInFormFilePath = filePath;
                    InstrumentAcceptance.TrainingSignInFormFileName = fileName;
                }

                // 评估报告
                if (FileUpload.EvaluationReportFile != null)
                {
                    var formFileContent = await FileHelpers.ProcessFormFile<Upload>(
                                            FileUpload.EvaluationReportFile, ModelState, _fileSizeLimit);

                    if (!ModelState.IsValid)
                    {
                        return Page();
                    }

                    string fileName = Path.GetFileName(FileUpload.EvaluationReportFile.FileName);
                    string filePath = FileHelpers.CreateFilePath(_uploadFilePath, fileName);

                    FileHelpers.SaveFile(formFileContent, filePath);
                    FileHelpers.DeleteOlderFile(InstrumentAcceptance.EvaluationReportFilePath);

                    InstrumentAcceptance.EvaluationReportFilePath = filePath;
                    InstrumentAcceptance.EvaluationReportFileName = fileName;
                }

                // 仪器设备履历表
                if (FileUpload.EquipmentResumeFile != null)
                {
                    var formFileContent = await FileHelpers.ProcessFormFile<Upload>(
                                            FileUpload.EquipmentResumeFile, ModelState, _fileSizeLimit);

                    if (!ModelState.IsValid)
                    {
                        return Page();
                    }

                    string fileName = Path.GetFileName(FileUpload.EquipmentResumeFile.FileName);
                    string filePath = FileHelpers.CreateFilePath(_uploadFilePath, fileName);

                    FileHelpers.SaveFile(formFileContent, filePath);
                    FileHelpers.DeleteOlderFile(InstrumentAcceptance.EquipmentResumeFilePath);

                    InstrumentAcceptance.EquipmentResumeFilePath = filePath;
                    InstrumentAcceptance.EquipmentResumeFileName = fileName;
                }

                // 仪器设备档案目录表
                if (FileUpload.EquipmentFilesListFile != null)
                {
                    var formFileContent = await FileHelpers.ProcessFormFile<Upload>(
                                            FileUpload.EquipmentFilesListFile, ModelState, _fileSizeLimit);

                    if (!ModelState.IsValid)
                    {
                        return Page();
                    }

                    string fileName = Path.GetFileName(FileUpload.EquipmentFilesListFile.FileName);
                    string filePath = FileHelpers.CreateFilePath(_uploadFilePath, fileName);

                    FileHelpers.SaveFile(formFileContent, filePath);
                    FileHelpers.DeleteOlderFile(InstrumentAcceptance.EquipmentFilesListFilePath);

                    InstrumentAcceptance.EquipmentFilesListFilePath = filePath;
                    InstrumentAcceptance.EquipmentFilesListFileName = fileName;
                }

                // 设备合格证
                if (FileUpload.EquipmentCertificateFile != null)
                {
                    var formFileContent = await FileHelpers.ProcessFormFile<Upload>(
                                            FileUpload.EquipmentCertificateFile, ModelState, _fileSizeLimit);

                    if (!ModelState.IsValid)
                    {
                        return Page();
                    }

                    string fileName = Path.GetFileName(FileUpload.EquipmentCertificateFile.FileName);
                    string filePath = FileHelpers.CreateFilePath(_uploadFilePath, fileName);

                    FileHelpers.SaveFile(formFileContent, filePath);
                    FileHelpers.DeleteOlderFile(InstrumentAcceptance.EquipmentCertificateFilePath);

                    InstrumentAcceptance.EquipmentCertificateFilePath = filePath;
                    InstrumentAcceptance.EquipmentCertificateFileName = fileName;
                }

                // 厂家生产许可证
                if (FileUpload.FactoryProductionLicenseFile != null)
                {
                    var formFileContent = await FileHelpers.ProcessFormFile<Upload>(
                                            FileUpload.FactoryProductionLicenseFile, ModelState, _fileSizeLimit);

                    if (!ModelState.IsValid)
                    {
                        return Page();
                    }

                    string fileName = Path.GetFileName(FileUpload.FactoryProductionLicenseFile.FileName);
                    string filePath = FileHelpers.CreateFilePath(_uploadFilePath, fileName);

                    FileHelpers.SaveFile(formFileContent, filePath);
                    FileHelpers.DeleteOlderFile(InstrumentAcceptance.FactoryProductionLicenseFilePath);

                    InstrumentAcceptance.FactoryProductionLicenseFilePath = filePath;
                    InstrumentAcceptance.FactoryProductionLicenseFileName = fileName;
                }

                // 营业执照
                if (FileUpload.BusinessLicenseFile != null)
                {
                    var formFileContent = await FileHelpers.ProcessFormFile<Upload>(
                                            FileUpload.BusinessLicenseFile, ModelState, _fileSizeLimit);

                    if (!ModelState.IsValid)
                    {
                        return Page();
                    }

                    string fileName = Path.GetFileName(FileUpload.BusinessLicenseFile.FileName);
                    string filePath = FileHelpers.CreateFilePath(_uploadFilePath, fileName);

                    FileHelpers.SaveFile(formFileContent, filePath);
                    FileHelpers.DeleteOlderFile(InstrumentAcceptance.BusinessLicenseFilePath);

                    InstrumentAcceptance.BusinessLicenseFilePath = filePath;
                    InstrumentAcceptance.BusinessLicenseFileName = fileName;
                }

                // 医疗器械注册证
                if (FileUpload.MedicalDeviceRegistrationCertificateFile != null)
                {
                    var formFileContent = await FileHelpers.ProcessFormFile<Upload>(
                                            FileUpload.MedicalDeviceRegistrationCertificateFile, ModelState, _fileSizeLimit);

                    if (!ModelState.IsValid)
                    {
                        return Page();
                    }

                    string fileName = Path.GetFileName(FileUpload.MedicalDeviceRegistrationCertificateFile.FileName);
                    string filePath = FileHelpers.CreateFilePath(_uploadFilePath, fileName);

                    FileHelpers.SaveFile(formFileContent, filePath);
                    FileHelpers.DeleteOlderFile(InstrumentAcceptance.MedicalDeviceRegistrationCertificateFilePath);

                    InstrumentAcceptance.MedicalDeviceRegistrationCertificateFilePath = filePath;
                    InstrumentAcceptance.MedicalDeviceRegistrationCertificateFileName = fileName;
                }

                // 设备校准报告
                if (FileUpload.EquipmentCalibrationReportFile != null)
                {
                    var formFileContent = await FileHelpers.ProcessFormFile<Upload>(
                                            FileUpload.EquipmentCalibrationReportFile, ModelState, _fileSizeLimit);

                    if (!ModelState.IsValid)
                    {
                        return Page();
                    }

                    string fileName = Path.GetFileName(FileUpload.EquipmentCalibrationReportFile.FileName);
                    string filePath = FileHelpers.CreateFilePath(_uploadFilePath, fileName);

                    FileHelpers.SaveFile(formFileContent, filePath);
                    FileHelpers.DeleteOlderFile(InstrumentAcceptance.EquipmentCalibrationReportFilePath);

                    InstrumentAcceptance.EquipmentCalibrationReportFilePath = filePath;
                    InstrumentAcceptance.EquipmentCalibrationReportFileName = fileName;
                }

                // 设备验收报告
                if (FileUpload.EquipmentAcceptanceReportFile != null)
                {
                    var formFileContent = await FileHelpers.ProcessFormFile<Upload>(
                                            FileUpload.EquipmentAcceptanceReportFile, ModelState, _fileSizeLimit);

                    if (!ModelState.IsValid)
                    {
                        return Page();
                    }

                    string fileName = Path.GetFileName(FileUpload.EquipmentAcceptanceReportFile.FileName);
                    string filePath = FileHelpers.CreateFilePath(_uploadFilePath, fileName);

                    FileHelpers.SaveFile(formFileContent, filePath);
                    FileHelpers.DeleteOlderFile(InstrumentAcceptance.EquipmentAcceptanceReportFilePath);

                    InstrumentAcceptance.EquipmentAcceptanceReportFilePath = filePath;
                    InstrumentAcceptance.EquipmentAcceptanceReportFileName = fileName;
                }
                #endregion

                await _context.SaveChangesAsync();
                return RedirectToPage("Edit", new { id = InstrumentAcceptance.Id });
            }

            return Page();
        }

        public async Task<IActionResult> OnPostArchiveAsync(int id)
        {
            var instanceToArchived = await _context.InstrumentAcceptances.FirstOrDefaultAsync(m => m.Id == id);

            if (instanceToArchived == null)
            {
                return new JsonResult("未找到该记录");
            }

            var isAuthorized = await _authorizationService.AuthorizeAsync(User, InstrumentAcceptance, Operations.Update);

            if (!isAuthorized.Succeeded)
            {
                return new JsonResult("权限不足");
            }

            try
            {
                instanceToArchived.IsArchived = true;
                await _context.SaveChangesAsync();
                return new JsonResult("归档成功！");
            }
            catch (Exception ex)
            {
                return new JsonResult($"归档失败，请重试。错误信息：{ex}");
            }
        }

        public class Upload
        {
            [Display(Name = "设备请购可行性报告")]
            public IFormFile FeasibilityReportFile { get; set; }

            [Display(Name = "设备配置清单")]
            public IFormFile ConfigurationListFile { get; set; }

            [Display(Name = "设备采购申请单")]
            public IFormFile PuchaseRequisitionFile { get; set; }

            [Display(Name = "设备安装说明")]
            public IFormFile InstallationNoteFile { get; set; }

            [Display(Name = "设备清点证明")]
            public IFormFile InventoryCertificateFile { get; set; }

            [Display(Name = "设备调试验收报告")]
            public IFormFile FactoryAcceptanceReportFile { get; set; }

            [Display(Name = "设备安装服务报告")]
            public IFormFile ServiceReportFile { get; set; }

            [Display(Name = "培训签到表")]
            public IFormFile TrainingSignInFormFile { get; set; }

            [Display(Name = "评估报告")]
            public IFormFile EvaluationReportFile { get; set; }

            [Display(Name = "仪器设备履历表")]
            public IFormFile EquipmentResumeFile { get; set; }

            [Display(Name = "仪器设备档案目录表")]
            public IFormFile EquipmentFilesListFile { get; set; }

            [Display(Name = "产品合格证")]
            public IFormFile EquipmentCertificateFile { get; set; }

            [Display(Name = "厂家生产许可证")]
            public IFormFile FactoryProductionLicenseFile { get; set; }

            [Display(Name = "营业执照")]
            public IFormFile BusinessLicenseFile { get; set; }

            [Display(Name = "医疗器械注册证")]
            public IFormFile MedicalDeviceRegistrationCertificateFile { get; set; }

            [Display(Name = "校准/检定证书")]
            public IFormFile EquipmentCalibrationReportFile { get; set; }

            [Display(Name = "设备验收报告")]
            public IFormFile EquipmentAcceptanceReportFile { get; set; }
        }
    }
}
