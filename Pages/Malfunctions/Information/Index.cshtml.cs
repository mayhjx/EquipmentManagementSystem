using EquipmentManagementSystem.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
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
    }
}
