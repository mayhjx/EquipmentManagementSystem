﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using EquipmentManagementSystem.Data;
using EquipmentManagementSystem.Models;

namespace EquipmentManagementSystem.Pages.MaintenanceRecords
{
    public class IndexModel : PageModel
    {
        private readonly EquipmentManagementSystem.Data.EquipmentContext _context;

        public IndexModel(EquipmentManagementSystem.Data.EquipmentContext context)
        {
            _context = context;
        }

        public IList<MaintenanceRecord> MaintenanceRecord { get;set; }

        public async Task OnGetAsync()
        {
            MaintenanceRecord = await _context.MaintenanceRecord
                .Include(m => m.Instrument)
                .Include(m => m.Project).ToListAsync();
        }
    }
}