using EquipmentManagementSystem.Data;
using EquipmentManagementSystem.Models;
using EquipmentManagementSystem.Utilities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace EquipmentManagementSystem.Pages.Malfunctions.Maintain
{
    public class EditModel : PageModel
    {
        private readonly MalfunctionContext _context;
        private readonly long _fileSizeLimit;

        public EditModel(MalfunctionContext context, IConfiguration config)
        {
            _context = context;
            _fileSizeLimit = config.GetValue<long>("FileSizeLimit");
        }

        [BindProperty]
        public Maintenance Maintenance { get; set; }

        public class Upload
        {
            [Display(Name = "File")]
            public IFormFile FormFile { get; set; }
        }

        [BindProperty]
        public Upload FileUpload { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Maintenance = await _context.Maintenance.FirstOrDefaultAsync(m => m.ID == id);

            if (Maintenance == null)
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

            Maintenance = await _context.Maintenance
                                .Include(m => m.MalfunctionWorkOrder)
                                .FirstAsync(m => m.ID == id);

            if (Maintenance == null)
            {
                return NotFound();
            }


            if (await TryUpdateModelAsync<Maintenance>(
                    Maintenance,
                    "Maintenance",
                    i => i.Repairer, i => i.Solution, i => i.IsCritical, i => i.Remark))
            {
                // 如果进度在维修中之前则更新为维修中
                if (Maintenance.MalfunctionWorkOrder.Progress < WorkOrderProgress.Repairing)
                {
                    Maintenance.MalfunctionWorkOrder.Progress = WorkOrderProgress.Repairing;
                }

                if (FileUpload.FormFile != null && FileUpload.FormFile.Length > 0)
                {
                    var formFileContent =
                        await FileHelpers.ProcessFormFile<Upload>(
                            FileUpload.FormFile, ModelState, _fileSizeLimit);

                    if (!ModelState.IsValid)
                    {
                        return Page();
                    }
                    Maintenance.Attachment = formFileContent;
                    Maintenance.FileName = FileUpload.FormFile.FileName;
                    Maintenance.UploadTime = DateTime.Now;
                }

                await _context.SaveChangesAsync();
                return RedirectToPage("../WorkOrders/Details", new { id = Maintenance.MalfunctionWorkOrderID });
            }
            return Page();
        }
    }
}
