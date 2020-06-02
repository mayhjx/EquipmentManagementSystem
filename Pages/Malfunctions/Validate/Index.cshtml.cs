using EquipmentManagementSystem.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Net;
using System.Net.Mime;
using System.Threading.Tasks;

namespace EquipmentManagementSystem.Pages.Malfunctions.Validate
{
    public class IndexModel : PageModel
    {
        private readonly EquipmentManagementSystem.Data.MalfunctionContext _context;

        public IndexModel(EquipmentManagementSystem.Data.MalfunctionContext context)
        {
            _context = context;
        }

        public IList<Validation> Validation { get; set; }

        public async Task OnGetAsync()
        {
            Validation = await _context.Validation.ToListAsync();
        }

        public async Task<IActionResult> OnGetDownloadAttachmentAsync(int? id)
        {
            if (id == null)
            {
                return Page();
            }

            var requestFile = await _context.Validation.SingleOrDefaultAsync(m => m.ID == id);

            if (requestFile == null)
            {
                return Page();
            }

            // Don't display the untrusted file name in the UI. HTML-encode the value.
            return File(requestFile.Attachment, MediaTypeNames.Application.Octet, WebUtility.HtmlEncode(requestFile.AttachmentName));
        }

        public async Task<IActionResult> OnGetDownloadPerformanceReportFileAsync(int? id)
        {
            if (id == null)
            {
                return Page();
            }

            var requestFile = await _context.Validation.SingleOrDefaultAsync(m => m.ID == id);

            if (requestFile == null)
            {
                return Page();
            }

            // Don't display the untrusted file name in the UI. HTML-encode the value.
            return File(requestFile.PerformanceReportFile, MediaTypeNames.Application.Octet, WebUtility.HtmlEncode(requestFile.PerformanceReportFileName));
        }

        public async Task<IActionResult> OnGetDownloadEffectReportFileAsync(int? id)
        {
            if (id == null)
            {
                return Page();
            }

            var requestFile = await _context.Validation.SingleOrDefaultAsync(m => m.ID == id);

            if (requestFile == null)
            {
                return Page();
            }

            // Don't display the untrusted file name in the UI. HTML-encode the value.
            return File(requestFile.EffectReportFile, MediaTypeNames.Application.Octet, WebUtility.HtmlEncode(requestFile.EffectReportFileName));
        }
    }
}
