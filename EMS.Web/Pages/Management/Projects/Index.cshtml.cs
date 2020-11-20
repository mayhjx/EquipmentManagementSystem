using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EquipmentManagementSystem.Data;
using EquipmentManagementSystem.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace EquipmentManagementSystem.Pages.Management.Projects
{
    public class IndexModel : PageModel
    {
        private readonly EquipmentContext _context;

        public IndexModel(EquipmentContext context)
        {
            _context = context;
        }

        public IList<Project> Project { get; set; }

        public async Task OnGetAsync()
        {
            Project = await _context.Projects
                .AsNoTracking()
                .Include(m => m.Group)
                .OrderBy(m => m.Group.Name)
                .ToListAsync();
        }
    }
}
