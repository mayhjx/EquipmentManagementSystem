using EquipmentManagementSystem.Data;
using EquipmentManagementSystem.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EquipmentManagementSystem.Pages
{
    public class IndexModel : PageModel
    {
        private readonly EquipmentContext _context;

        public IndexModel(EquipmentContext context)
        {
            _context = context;
        }

        public IList<Instrument> Instruments { get; set; }

        // 快到期设备
        public IList<Instrument> InstrumentOfExpire { get; set; }

        //public IList<MalfunctionWorkOrder> Malfunctions { get; set; }

        public void OnGet()
        {

            InstrumentOfExpire = (from m in _context.Instruments
                                .Include(m => m.Calibrations)
                                .AsEnumerable()
                                  where (m.Calibrations.Count > 0 && m.Calibrations.Last().Date != DateTime.MinValue)
                                  let remainDay = m.Calibrations.Last().Date.AddYears(m.CalibrationCycle) - DateTime.Today
                                  where remainDay.Days < 30 // 到期前30天内
                                  select m).ToList();

            //Malfunctions = (from m in _context.Malfunctions
            //                select m).ToList();

            Instruments = (from m in _context.Instruments
                           select m).ToList();

        }
    }
}
