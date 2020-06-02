using EquipmentManagementSystem.Data;
using EquipmentManagementSystem.Models;
using EquipmentManagementSystem.Utilities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace EquipmentManagementSystem.Pages.Malfunctions.Validate
{
    public class EditModel : PageModel
    {
        private readonly MalfunctionContext _context;
        private readonly long _fileSizeLimit;

        public EditModel(MalfunctionContext context, IConfiguration config)
        {
            _fileSizeLimit = config.GetValue<long>("FileSizeLimit");
            _context = context;
        }

        [BindProperty]
        public Validation Validation { get; set; }

        public class Upload
        {
            [Display(Name = "故障修复后性能验证报告")]
            public IFormFile PerformanceReportFile { get; set; }

            [Display(Name = "故障前病人结果评估报告")]
            public IFormFile EffectReportFile { get; set; }

            [Display(Name = "附件")]
            public IFormFile Attachment { get; set; }
        }

        [BindProperty]
        public Upload FileUpload { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Validation = await _context.Validation.FirstOrDefaultAsync(m => m.ID == id);

            if (Validation == null)
            {
                return NotFound();
            }
            return Page();
        }


        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Validation = await _context.Validation
                                .Include(m => m.MalfunctionWorkOrder)
                                .FirstAsync(m => m.ID == id);

            if (Validation == null)
            {
                return NotFound();
            }

            if (await TryUpdateModelAsync<Validation>(
                    Validation,
                    "Validation",
                    i => i.EndTime, i => i.IsConfirm, i => i.Summary))
            {
                // 如果进度在已保修之前则更新已报修，设备状态更新为正常
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

                // 上传故障前病人结果评估报告
                if (FileUpload.EffectReportFile != null && FileUpload.EffectReportFile.Length > 0)
                {
                    var formFileContent =
                        await FileHelpers.ProcessFormFile<Upload>(
                            FileUpload.EffectReportFile, ModelState, _fileSizeLimit);

                    if (!ModelState.IsValid)
                    {
                        return Page();
                    }

                    Validation.EffectReportFile = formFileContent;
                    Validation.EffectReportFileName = FileUpload.EffectReportFile.FileName;
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
    }
}
