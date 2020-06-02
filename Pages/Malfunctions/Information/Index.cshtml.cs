using EquipmentManagementSystem.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Net;
using System.Net.Mime;
using System.Threading.Tasks;

namespace EquipmentManagementSystem.Pages.Malfunctions.Information
{
    public class IndexModel : PageModel
    {
        private readonly EquipmentManagementSystem.Data.MalfunctionContext _context;

        public IndexModel(EquipmentManagementSystem.Data.MalfunctionContext context)
        {
            _context = context;
        }

        public IList<MalfunctionInfo> MalfunctionInfo { get; set; }

        public async Task OnGetAsync()
        {
            MalfunctionInfo = await _context.MalfunctionInfo.ToListAsync();
        }

        public async Task<IActionResult> OnGetDownloadAsync(int? id)
        {
            if (id == null)
            {
                return Page();
            }

            var requestFile = await _context.MalfunctionInfo.SingleOrDefaultAsync(m => m.ID == id);

            if (requestFile == null)
            {
                return Page();
            }

            // Don't display the untrusted file name in the UI. HTML-encode the value.
            return File(requestFile.Attachment, MediaTypeNames.Application.Octet, WebUtility.HtmlEncode(requestFile.FileName));
        }
    }
}
