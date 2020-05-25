using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using EquipmentManagementSystem.Data;
using EquipmentManagementSystem.Models;

namespace EquipmentManagementSystem.Pages.Malfunctions.AccessoriesOrders
{
    public class IndexModel : PageModel
    {
        private readonly EquipmentManagementSystem.Data.MalfunctionContext _context;

        public IndexModel(EquipmentManagementSystem.Data.MalfunctionContext context)
        {
            _context = context;
        }

        public IList<AccessoriesOrder> AccessoriesOrder { get;set; }

        public async Task OnGetAsync()
        {
            AccessoriesOrder = await _context.AccessoriesOrder.ToListAsync();
        }
    }
}
