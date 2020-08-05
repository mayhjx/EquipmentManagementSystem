using System;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using EquipmentManagementSystem.Authorization;
using EquipmentManagementSystem.Data;
using EquipmentManagementSystem.Models;
using EquipmentManagementSystem.Utilities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;

namespace EquipmentManagementSystem.Pages.Malfunctions.Validate
{
    public class EditModel : BasePageModel
    {
        private readonly long _fileSizeLimit;
        private readonly string _uploadFilePath;
        private readonly IHostEnvironment _env;

        public EditModel(MalfunctionContext context,
            IAuthorizationService authorization,
            UserManager<User> userManager,
            IConfiguration config,
            IHostEnvironment env)
            : base(context, authorization, userManager)
        {
            _fileSizeLimit = config.GetValue<long>("FileSizeLimit");
            _uploadFilePath = config.GetValue<string>("StoredFilesPath");
            _env = env;
        }

        [BindProperty]
        public Validation Validation { get; set; }

        [BindProperty]
        public Upload FileUpload { get; set; }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            Validation = await _context.Validation
                            .Include(m => m.MalfunctionWorkOrder)
                            .ThenInclude(m => m.Instrument)
                            .FirstOrDefaultAsync(m => m.ID == id);

            if (Validation == null)
            {
                return NotFound();
            }

            // 如果信息已确认或工单已完成则跳转到工单详情页
            if (Validation.IsConfirm || Validation.MalfunctionWorkOrder.Progress == WorkOrderProgress.Completed)
            {
                return RedirectToPage("../WorkOrders/Details", new { id = Validation.MalfunctionWorkOrderID });
            }

            var isAuthorized = await _authorizationService.AuthorizeAsync(User, Validation.MalfunctionWorkOrder, Operations.Update);

            if (!isAuthorized.Succeeded)
            {
                return Forbid();
            }

            return Page();
        }

        public async Task<IActionResult> OnGetDownloadAttachmentAsync(int id)
        {
            var requestFile = await _context.Validation.FindAsync(id);

            if (requestFile == null || !System.IO.File.Exists(requestFile.AttachmentFilePath))
            {
                return Page();
            }

            var memoryStream = new MemoryStream();
            using (var stream = new FileStream(requestFile.AttachmentFilePath, FileMode.Open))
            {
                await stream.CopyToAsync(memoryStream);
            }
            memoryStream.Position = 0;

            // Don't display the untrusted file name in the UI. HTML-encode the value.
            return File(memoryStream, FileHelpers.GetContentType(requestFile.AttachmentFilePath), WebUtility.HtmlEncode(requestFile.AttachmentName));
        }

        public async Task<IActionResult> OnGetDownloadPerformanceReportFileAsync(int id)
        {
            var requestFile = await _context.Validation.FindAsync(id);

            if (requestFile == null || !System.IO.File.Exists(requestFile.PerformanceReportFilePath))
            {
                return Page();
            }

            var memoryStream = new MemoryStream();
            using (var stream = new FileStream(requestFile.PerformanceReportFilePath, FileMode.Open))
            {
                await stream.CopyToAsync(memoryStream);
            }
            memoryStream.Position = 0;

            // Don't display the untrusted file name in the UI. HTML-encode the value.
            return File(memoryStream, FileHelpers.GetContentType(requestFile.PerformanceReportFilePath), WebUtility.HtmlEncode(requestFile.PerformanceReportFileName));
        }

        public async Task<IActionResult> OnPostAsync(int id)
        {
            Validation = await _context.Validation
                            .Include(m => m.MalfunctionWorkOrder)
                            .ThenInclude(m => m.Instrument)
                            .FirstOrDefaultAsync(m => m.ID == id);

            if (Validation == null)
            {
                return NotFound();
            }

            // 如果信息已确认或工单已完成则跳转到工单详情页
            if (Validation.IsConfirm || Validation.MalfunctionWorkOrder.Progress == WorkOrderProgress.Completed)
            {
                return RedirectToPage("../WorkOrders/Details", new { id = Validation.MalfunctionWorkOrderID });
            }

            var isAuthorized = await _authorizationService.AuthorizeAsync(User, Validation.MalfunctionWorkOrder, Operations.Update);

            if (!isAuthorized.Succeeded)
            {
                return Forbid();
            }

            if (await TryUpdateModelAsync<Validation>(
                    Validation,
                    "Validation",
                    i => i.FinishedTime, i => i.IsConfirm, i => i.Summary))
            {
                // 更新进度
                if (Validation.MalfunctionWorkOrder.Progress < WorkOrderProgress.Validated)
                {
                    Validation.MalfunctionWorkOrder.Progress = WorkOrderProgress.Validated;
                }

                // 上传故障修复后性能验证报告
                if (FileUpload.PerformanceReportFile != null && FileUpload.PerformanceReportFile.Length > 0)
                {
                    var formFileContent =
                        await FileHelpers.ProcessFormFile<Upload>(
                            FileUpload.PerformanceReportFile, ModelState, _fileSizeLimit);

                    if (!ModelState.IsValid)
                    {
                        return Page();
                    }

                    var filename = Path.GetFileName(FileUpload.PerformanceReportFile.FileName);
                    var filepath = Path.Combine(_env.ContentRootPath, _uploadFilePath, Guid.NewGuid().ToString() + "_" + filename);

                    // 保存文件
                    using (var fileStream = new FileStream(filepath, FileMode.Create))
                    {
                        await FileUpload.PerformanceReportFile.CopyToAsync(fileStream);
                    }

                    // 删除已上传的文件
                    if (System.IO.File.Exists(Validation.PerformanceReportFilePath))
                    {
                        System.IO.File.Delete(Validation.PerformanceReportFilePath);
                    }

                    Validation.PerformanceReportFilePath = filepath;
                    Validation.PerformanceReportFileName = filename;
                }

                // 上传附件
                if (FileUpload.Attachment != null && FileUpload.Attachment.Length > 0)
                {
                    var formFileContent =
                        await FileHelpers.ProcessFormFile<Upload>(
                            FileUpload.Attachment, ModelState, _fileSizeLimit);

                    if (!ModelState.IsValid)
                    {
                        return Page();
                    }

                    var filename = Path.GetFileName(FileUpload.Attachment.FileName);
                    var filepath = Path.Combine(_env.ContentRootPath, _uploadFilePath, Guid.NewGuid().ToString() + "_" + filename);

                    // 保存文件
                    using (var fileStream = new FileStream(filepath, FileMode.Create))
                    {
                        await FileUpload.Attachment.CopyToAsync(fileStream);
                    }

                    // 删除已上传的文件
                    if (System.IO.File.Exists(Validation.AttachmentFilePath))
                    {
                        System.IO.File.Delete(Validation.AttachmentFilePath);
                    }

                    Validation.AttachmentFilePath = filepath;
                    Validation.AttachmentName = filename;
                }

                await _context.SaveChangesAsync();
                return RedirectToPage("../WorkOrders/Details", new { id = Validation.MalfunctionWorkOrderID });
            }
            return Page();
        }

        // 批准性能验证
        public async Task<IActionResult> OnPostComfirmAsync(int id)
        {
            Validation = await _context.Validation
                                .Include(m => m.MalfunctionWorkOrder)
                                .ThenInclude(m => m.Instrument)
                                .FirstOrDefaultAsync(m => m.ID == id);

            if (Validation == null)
            {
                return new JsonResult("未找到该记录");
            }

            var isAuthorized = await _authorizationService.AuthorizeAsync(User, Validation.MalfunctionWorkOrder, Operations.Update);

            if (!isAuthorized.Succeeded)
            {
                return new JsonResult("权限不足");
            }

            if (Validation.PerformanceReportFileName == null ||
                Validation.FinishedTime == null)
            {
                return new JsonResult("请补充完成时间或验证报告");
            }

            Validation.IsConfirm = true;

            try
            {
                await _context.SaveChangesAsync();
                return new JsonResult("性能验证报告已批准！");
            }
            catch (Exception ex)
            {
                return new JsonResult($"性能验证报告未批准，错误信息：{ex}");
            }
        }

        public class Upload
        {
            [Display(Name = "性能验证报告")]
            public IFormFile PerformanceReportFile { get; set; }

            [Display(Name = "附件")]
            public IFormFile Attachment { get; set; }
        }
    }
}
