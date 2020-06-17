using EquipmentManagementSystem.Authorization;
using EquipmentManagementSystem.Data;
using EquipmentManagementSystem.Models;
using EquipmentManagementSystem.Pages.Instruments;
using EquipmentManagementSystem.Utilities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Net.Mime;
using System.Threading.Tasks;

namespace EquipmentManagementSystem.Pages.Malfunctions.Validate
{
    public class EditModel : BasePageModel
    {
        private readonly long _fileSizeLimit;

        public EditModel(MalfunctionContext context,
            IAuthorizationService authorization,
            UserManager<User> userManager,
            IConfiguration config)
            : base(context, authorization, userManager)
        {
            _fileSizeLimit = config.GetValue<long>("FileSizeLimit");
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

            if (requestFile == null)
            {
                return Page();
            }

            // Don't display the untrusted file name in the UI. HTML-encode the value.
            return File(requestFile.Attachment, MediaTypeNames.Application.Octet, WebUtility.HtmlEncode(requestFile.AttachmentName));
        }

        public async Task<IActionResult> OnGetDownloadPerformanceReportFileAsync(int id)
        {
            var requestFile = await _context.Validation.FindAsync(id);

            if (requestFile == null)
            {
                return Page();
            }

            // Don't display the untrusted file name in the UI. HTML-encode the value.
            return File(requestFile.PerformanceReportFile, MediaTypeNames.Application.Octet, WebUtility.HtmlEncode(requestFile.PerformanceReportFileName));
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

                    Validation.PerformanceReportFile = formFileContent;
                    Validation.PerformanceReportFileName = FileUpload.PerformanceReportFile.FileName;
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

                    Validation.Attachment = formFileContent;
                    Validation.AttachmentName = FileUpload.Attachment.FileName;
                }

                await _context.SaveChangesAsync();
                return RedirectToPage("../WorkOrders/Details", new { id = Validation.MalfunctionWorkOrderID });
            }
            return Page();
        }

        // 批准性能验证
        public async Task<IActionResult> OnPutComfirmAsync(int id)
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
