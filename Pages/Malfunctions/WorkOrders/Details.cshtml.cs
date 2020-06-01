using EquipmentManagementSystem.Data;
using EquipmentManagementSystem.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using System.Net.Mime;
using System.Threading.Tasks;

namespace EquipmentManagementSystem.Pages.Malfunctions.WorkOrders
{
    public class DetailsModel : PageModel
    {
        private readonly MalfunctionContext _context;
        private readonly IFileProvider _fileProvider;

        public DetailsModel(MalfunctionContext context, IFileProvider fileProvider)
        {
            _context = context;
            _fileProvider = fileProvider;
        }

        public MalfunctionWorkOrder MalfunctionWorkOrder { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            MalfunctionWorkOrder = await _context.MalfunctionWorkOrder
                                    .Include(m => m.MalfunctionInfo)
                                    .Include(m => m.Investigation)
                                    .Include(m => m.RepairRequest)
                                    .Include(m => m.AccessoriesOrder)
                                    .Include(m => m.Maintenance)
                                    .Include(m => m.Validation)
                                    .AsNoTracking()
                                    .FirstOrDefaultAsync(m => m.ID == id);

            if (MalfunctionWorkOrder == null)
            {
                return NotFound();
            }
            return Page();
        }

        public IActionResult OnGetDownload(string fileName)
        {
            var downloadFile = _fileProvider.GetFileInfo(fileName);
            return PhysicalFile(downloadFile.PhysicalPath, MediaTypeNames.Application.Octet, fileName);
        }
    }
}
