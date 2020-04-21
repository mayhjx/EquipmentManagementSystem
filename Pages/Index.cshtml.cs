using EquipmentManagementSystem.Data;
using EquipmentManagementSystem.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;


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

        // 快到期设备
        public IList<Instrument> InstrumentOfExpire { get; set; }

        public void OnGet()
        {

            InstrumentOfExpire = (from m in _context.Instruments
                                .Include(m => m.calibrations)
                                .AsEnumerable()
                                  where (m.calibrations.Count > 0 && m.calibrations.Last().Date != DateTime.MinValue)
                                  let remainDay = m.calibrations.Last().Date.AddYears(m.CalibrationCycle) - DateTime.Today
                                  where remainDay.Days < 30 // 到期前30天内
                                  select m).ToList();

            Instruments = (from m in _context.Instruments
                           select m).ToList();

        }
    }
}
