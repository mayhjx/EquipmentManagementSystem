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

namespace EquipmentManagementSystem.Pages.Malfunctions.Information
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
        public MalfunctionInfo MalfunctionInfo { get; set; }

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


            MalfunctionInfo = await _context.MalfunctionInfo
                                .FirstOrDefaultAsync(m => m.ID == id);

            if (MalfunctionInfo == null)
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

            MalfunctionInfo = await _context.MalfunctionInfo
                                .Include(m => m.MalfunctionWorkOrder)
                                .FirstAsync(m => m.ID == id);

            if (MalfunctionInfo == null)
            {
                return NotFound();
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

                if (await TryUpdateModelAsync<MalfunctionInfo>(
                    MalfunctionInfo,
                    "MalfunctionInfo",
                    i => i.BeginTime, i => i.FoundedTime, i => i.Type, i => i.Part,
                    i => i.Phenomenon, i => i.Reason, i => i.Remark, i => i.IsConfirm))
                {
                    MalfunctionInfo.Attachment = formFileContent;
                    MalfunctionInfo.FileName = FileUpload.FormFile.FileName;
                    MalfunctionInfo.UploadTime = DateTime.Now;

                    await _context.SaveChangesAsync();
                    return RedirectToPage("../WorkOrders/Details", new { id = MalfunctionInfo.MalfunctionWorkOrderID });
                }
            }
            else
            {
                if (await TryUpdateModelAsync<MalfunctionInfo>(
                    MalfunctionInfo,
                    "MalfunctionInfo",
                    i => i.BeginTime, i => i.FoundedTime, i => i.Type, i => i.Part,
                    i => i.Phenomenon, i => i.Reason, i => i.Remark, i => i.IsConfirm))
                {
                    await _context.SaveChangesAsync();
                    return RedirectToPage("../WorkOrders/Details", new { id = MalfunctionInfo.MalfunctionWorkOrderID });
                }
            }


            return Page();
        }
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://aka.ms/RazorPagesCRUD.
        //public async Task<IActionResult> OnPostAsync()
        //{

        //    if (!ModelState.IsValid)
        //    {
        //        return Page();
        //    }

        //    if (FileUpload.FormFile != null && FileUpload.FormFile.Length > 0)
        //    {
        //        var formFileContent =
        //            await FileHelpers.ProcessFormFile<Upload>(
        //                FileUpload.FormFile, ModelState, _fileSizeLimit);

        //        if (!ModelState.IsValid)
        //        {
        //            return Page();
        //        }

        //        MalfunctionInfo.UploadTime = DateTime.Now;
        //        MalfunctionInfo.Attachment = formFileContent;
        //        MalfunctionInfo.FileName = FileUpload.FormFile.FileName;
        //    }

        //    _context.Attach(MalfunctionInfo).State = EntityState.Modified;

        //    try
        //    {
        //        await _context.SaveChangesAsync();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!MalfunctionInfoExists(MalfunctionInfo.ID))
        //        {
        //            return NotFound();
        //        }
        //        else
        //        {
        //            throw;
        //        }
        //    }

        //    return RedirectToPage("../WorkOrders/Details", new { id = MalfunctionInfo.MalfunctionWorkOrderID });
        //}

        //private bool MalfunctionInfoExists(int id)
        //{
        //    return _context.MalfunctionInfo.Any(e => e.ID == id);
        //}
    }
}
