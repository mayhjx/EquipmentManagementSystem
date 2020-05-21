﻿using EquipmentManagementSystem.Data;
using EquipmentManagementSystem.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EquipmentManagementSystem.Pages.MalfunctionWorkOrders
{
    public class IndexModel : PageModel
    {
        private readonly MalfunctionContext _context;

        public IndexModel(MalfunctionContext context)
        {
            _context = context;
        }

        public IList<MalfunctionWorkOrder> MalfunctionWorkOrder { get; set; }

        public async Task OnGetAsync()
        {
            MalfunctionWorkOrder = await _context.MalfunctionWorkOrder
                                    .Include(m => m.Instrument)
                                    .Include(m => m.MalfuntionInfo)
                                    .AsNoTracking()
                                    .ToListAsync();
        }
    }
}
