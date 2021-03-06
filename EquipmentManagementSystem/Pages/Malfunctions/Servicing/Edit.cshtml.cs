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
using System.IO;
using System.Net;
using System.Threading.Tasks;

namespace EquipmentManagementSystem.Pages.Malfunctions.Servicing
{
    public class EditModel : BasePageModel
    {
        private readonly long _fileSizeLimit;
        private readonly string _uploadFilePath;

        public EditModel(EquipmentContext context,
            IAuthorizationService authorizationService,
            UserManager<User> userManager,
            IConfiguration config)
            : base(context, authorizationService, userManager)
        {
            _fileSizeLimit = config.GetValue<long>("FileSizeLimit");
            _uploadFilePath = Path.Combine(config.GetValue<string>("StoredFilesPath"), "Malfunction");
            Directory.CreateDirectory(_uploadFilePath);
        }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            Repair = await _context.Repair
                                .Include(m => m.MalfunctionWorkOrder)
                                .ThenInclude(m => m.Instrument)
                                .FirstOrDefaultAsync(m => m.ID == id);

            if (Repair == null)
            {
                return NotFound();
            }

            // 如果工单已完成则跳转到工单详情页
            if (Repair.MalfunctionWorkOrder.Progress == WorkOrderProgress.Completed)
            {
                return RedirectToPage("../WorkOrders/Details", new { id = Repair.MalfunctionWorkOrderID });
            }

            var isAuthorized = await _authorizationService.AuthorizeAsync(User, Repair, Operations.Update);

            if (!isAuthorized.Succeeded)
            {
                return Forbid();
            }

            SolutionSelectList = new SelectList(_context.MalfunctionSolution, "Solution", "Solution");

            return Page();
        }

        public async Task<IActionResult> OnGetDownloadAsync(int id)
        {
            // 下载附件
            var requestFile = await _context.Repair.FindAsync(id);

            if (requestFile == null || !System.IO.File.Exists(requestFile.FilePath))
            {
                return NotFound();
            }

            var memoryStream = new MemoryStream();
            using (var stream = new FileStream(requestFile.FilePath, FileMode.Open))
            {
                await stream.CopyToAsync(memoryStream);
            }
            memoryStream.Position = 0;

            // Don't display the untrusted file name in the UI. HTML-encode the value.
            return File(memoryStream, FileHelpers.GetContentType(requestFile.FilePath), WebUtility.HtmlEncode(requestFile.FileName));
        }

        [BindProperty]
        public Repair Repair { get; set; }

        [BindProperty]
        public Upload FileUpload { get; set; }

        public SelectList SolutionSelectList { get; set; }

        public async Task<IActionResult> OnPostAsync(int id)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            Repair = await _context.Repair
                                .Include(m => m.MalfunctionWorkOrder)
                                .ThenInclude(m => m.Instrument)
                                .FirstOrDefaultAsync(m => m.ID == id);

            if (Repair == null)
            {
                return NotFound();
            }

            // 如果工单已完成则跳转到工单详情页
            if (Repair.MalfunctionWorkOrder.Progress == WorkOrderProgress.Completed)
            {
                return RedirectToPage("../WorkOrders/Details", new { id = Repair.MalfunctionWorkOrderID });
            }

            var isAuthorized = await _authorizationService.AuthorizeAsync(User, Repair, Operations.Update);

            if (!isAuthorized.Succeeded)
            {
                return Forbid();
            }

            if (await TryUpdateModelAsync<Repair>(
                    Repair,
                    "Repair",
                    i => i.Repairer, i => i.Solution, i => i.BeginTime, i => i.EndTime, i => i.IsCritical, i => i.Remark))
            {
                // 更新进度
                if (Repair.MalfunctionWorkOrder.Progress < WorkOrderProgress.Repaired)
                {
                    Repair.MalfunctionWorkOrder.Progress = WorkOrderProgress.Repaired;
                }

                // 上传附件
                if (FileUpload.FormFile != null)
                {
                    var formFileContent =
                        await FileHelpers.ProcessFormFile<Upload>(
                            FileUpload.FormFile, ModelState, _fileSizeLimit);

                    if (!ModelState.IsValid)
                    {
                        return Page();
                    }

                    var filename = Path.GetFileName(FileUpload.FormFile.FileName);
                    var filepath = FileHelpers.CreateFilePath(_uploadFilePath, filename);

                    FileHelpers.SaveFile(formFileContent, filepath);
                    FileHelpers.DeleteOlderFile(Repair.FilePath);

                    Repair.FileName = filename;
                    Repair.FilePath = filepath;
                    Repair.UploadTime = DateTime.Now;
                }

                await _context.SaveChangesAsync();

                return RedirectToPage("../WorkOrders/Details", new { id = Repair.MalfunctionWorkOrderID });
            }
            return Page();
        }

        public class Upload
        {
            [Display(Name = "File")]
            public IFormFile FormFile { get; set; }
        }
    }
}
