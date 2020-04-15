﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using EquipmentManagementSystem.Models;
using EquipmentManagementSystem.Data;
using Microsoft.EntityFrameworkCore;


namespace EquipmentManagementSystem.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly EquipmentContext _context;

        public IndexModel(ILogger<IndexModel> logger, EquipmentContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IList<Instrument> Instruments { get; set; }

        public void OnGet()
        {
            var instruments = from m in _context.Instruments
                              .Include(m => m.calibrations)
                              .AsEnumerable()
                              where m.calibrations.Last().Date != DateTime.Parse("0001-01-01")
                              let remainDay = m.calibrations.Last().Date.AddYears(m.CalibrationCycle) - DateTime.Today
                              where remainDay.Days < 30
                              select m;

            Instruments = instruments.ToList();                
                             
        }
    }
}
