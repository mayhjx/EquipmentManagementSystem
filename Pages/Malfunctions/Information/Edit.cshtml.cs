using EquipmentManagementSystem.Authorization;
using EquipmentManagementSystem.Data;
using EquipmentManagementSystem.Models;
using EquipmentManagementSystem.Utilities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Net.Mime;
using System.Threading.Tasks;

namespace EquipmentManagementSystem.Pages.Malfunctions.Information
{
    public class EditModel : BasePageModel
    {
        private readonly long _fileSizeLimit;

        public EditModel(MalfunctionContext context,
            IAuthorizationService authorizationService,
            UserManager<User> userManager,
            IConfiguration config)
            : base(context, authorizationService, userManager)
        {
            _fileSizeLimit = config.GetValue<long>("FileSizeLimit");
        }

        public async Task<IActionResult> OnGetAsync(int id)
        {

            MalfunctionInfo = await _context.MalfunctionInfo
                                        .Include(m => m.MalfunctionWorkOrder)
                                        .ThenInclude(m => m.Instrument)
                                        .FirstOrDefaultAsync(m => m.ID == id);

            if (MalfunctionInfo == null)
            {
                return NotFound();
            }

            // 如果信息已确认或工单已完成则跳转到工单详情页
            if (MalfunctionInfo.IsConfirm || MalfunctionInfo.MalfunctionWorkOrder.Progress == WorkOrderProgress.Completed)
            {
                return RedirectToPage("../WorkOrders/Details", new { id = MalfunctionInfo.MalfunctionWorkOrderID });
            }

            var isAuthorized = await _authorizationService.AuthorizeAsync(User, MalfunctionInfo.MalfunctionWorkOrder, Operations.Update);

            if (!isAuthorized.Succeeded)
            {
                return Forbid();
            }

            MalfunctionPartSelectList = new SelectList(_context.MalfunctionParts, "Name", "Name");
            MalfunctionPhenomenonSelectList = new SelectList(_context.MalfunctionPhenomenon, "Phenomenon", "Phenomenon", MalfunctionInfo.Phenomenon);
            MalfunctionReasonSelectList = new SelectList(_context.MalfunctionReason, "Reason", "Reason");

            return Page();
        }

        public async Task<IActionResult> OnGetDownloadAsync(int id)
        {
            // 下载附件
            var requestFile = await _context.MalfunctionInfo.FindAsync(id);

            if (requestFile == null)
            {
                return Page();
            }

            // Don't display the untrusted file name in the UI. HTML-encode the value.
            return File(requestFile.Attachment, MediaTypeNames.Application.Octet, WebUtility.HtmlEncode(requestFile.FileName));
        }

        [BindProperty]
        public MalfunctionInfo MalfunctionInfo { get; set; }

        [BindProperty]
        public Upload FileUpload { get; set; }

        public SelectList MalfunctionPhenomenonSelectList { get; set; }
        public SelectList MalfunctionPartSelectList { get; set; }
        public SelectList MalfunctionReasonSelectList { get; set; }

        public async Task<IActionResult> OnPostAsync(int id)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            MalfunctionInfo = await _context.MalfunctionInfo
                                        .Include(m => m.MalfunctionWorkOrder)
                                        .ThenInclude(m => m.Instrument)
                                        .FirstOrDefaultAsync(m => m.ID == id);

            if (MalfunctionInfo == null)
            {
                return NotFound();
            }

            // 如果信息已确认或工单已完成则跳转到工单详情页
            if (MalfunctionInfo.IsConfirm || MalfunctionInfo.MalfunctionWorkOrder.Progress == WorkOrderProgress.Completed)
            {
                return RedirectToPage("../WorkOrders/Details", new { id = MalfunctionInfo.MalfunctionWorkOrderID });
            }

            var isAuthorized = await _authorizationService.AuthorizeAsync(User, MalfunctionInfo.MalfunctionWorkOrder, Operations.Update);

            if (!isAuthorized.Succeeded)
            {
                return Forbid();
            }

            if (await TryUpdateModelAsync<MalfunctionInfo>(
                    MalfunctionInfo,
                    "MalfunctionInfo",
                    i => i.BeginTime, i => i.FoundedTime, i => i.Type, i => i.Part,
                    i => i.Phenomenon, i => i.Reason, i => i.Remark))
            {
                // 上传附件
                if (FileUpload.FormFile != null && FileUpload.FormFile.Length > 0)
                {
                    var formFileContent =
                        await FileHelpers.ProcessFormFile<Upload>(
                            FileUpload.FormFile, ModelState, _fileSizeLimit);

                    if (!ModelState.IsValid)
                    {
                        return Page();
                    }

                    MalfunctionInfo.Attachment = formFileContent;
                    MalfunctionInfo.FileName = FileUpload.FormFile.FileName;
                    MalfunctionInfo.UploadTime = DateTime.Now;
                }

                await _context.SaveChangesAsync();

                return RedirectToPage("../WorkOrders/Details", new { id = MalfunctionInfo.MalfunctionWorkOrderID });
            }
            return Page();
        }

        // 确认故障基础信息
        public async Task<IActionResult> OnPostComfirmAsync(int id)
        {
            MalfunctionInfo = await _context.MalfunctionInfo
                                .Include(m => m.MalfunctionWorkOrder)
                                .ThenInclude(m => m.Instrument)
                                .FirstOrDefaultAsync(m => m.ID == id);

            if (MalfunctionInfo == null)
            {
                return new JsonResult("未找到该记录");
            }

            var isAuthorized = await _authorizationService.AuthorizeAsync(User, MalfunctionInfo.MalfunctionWorkOrder, Operations.Comfirm);

            if (!isAuthorized.Succeeded)
            {
                return new JsonResult("权限不足");
            }

            if (MalfunctionInfo.Part == null ||
                MalfunctionInfo.Reason == null)
            {
                return new JsonResult("请补充故障部位或可能原因");
            }

            MalfunctionInfo.IsConfirm = true;

            try
            {
                await _context.SaveChangesAsync();
                return new JsonResult("故障信息已确认！");
            }
            catch (Exception ex)
            {
                return new JsonResult($"故障信息未确认，错误信息：{ex}");
            }
        }

        public class Upload
        {
            [Display(Name = "File")]
            public IFormFile FormFile { get; set; }
        }
    }
}
