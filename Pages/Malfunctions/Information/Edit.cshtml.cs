using EquipmentManagementSystem.Data;
using EquipmentManagementSystem.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace EquipmentManagementSystem.Pages.Malfunctions.Information
{
    public class EditModel : PageModel
    {
        private readonly MalfunctionContext _context;
        private readonly string _targetFilePath;

        public EditModel(MalfunctionContext context, IConfiguration config)
        {
            _context = context;
            _targetFilePath = config.GetValue<string>("StoredFilesPath");
        }

        [BindProperty]
        public MalfunctionInfo MalfunctionInfo { get; set; }

        public class Upload
        {
            [Required]
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
                                .Include(m => m.MalfunctionWorkOrder)
                                .FirstOrDefaultAsync(m => m.ID == id);

            if (MalfunctionInfo == null)
            {
                return NotFound();
            }
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {

            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(MalfunctionInfo).State = EntityState.Modified;
            var filename = Path.GetFileName(FileUpload.FormFile.FileName);
            var filepath = Path.Combine(_targetFilePath, filename);

            MalfunctionInfo.Attachment = filename;
            MalfunctionInfo.FilePath = filepath;
            MalfunctionInfo.UploadTime = DateTime.Now;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MalfunctionInfoExists(MalfunctionInfo.ID))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            using (var fileStream = System.IO.File.Create(filepath))
            {
                await FileUpload.FormFile.CopyToAsync(fileStream);
            }

            return RedirectToPage("../WorkOrders/Details", new { id = MalfunctionInfo.MalfunctionWorkOrderID });
        }

        private bool MalfunctionInfoExists(int id)
        {
            return _context.MalfunctionInfo.Any(e => e.ID == id);
        }
    }
}
