using EquipmentManagementSystem.Data;
using EquipmentManagementSystem.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EquipmentManagementSystem.Pages
{
    [AllowAnonymous]
    public class IndexModel : PageModel
    {
        private readonly EquipmentContext _context;
        private readonly MalfunctionContext _malfunctionContext;

        public IndexModel(EquipmentContext context, MalfunctionContext malfunctionContext)
        {
            _context = context;
            _malfunctionContext = malfunctionContext;
        }

        public int InstrumentNumber { get; set; }

        // 快到期设备
        public IList<Instrument> InstrumentOfExpire { get; set; }

        public int MalfunctionWorkOrderOfFollowNumber { get; set; }

        public void OnGet()
        {
            InstrumentNumber = _context.Instruments.Count();

            InstrumentOfExpire = (from m in _context.Instruments
                                .AsNoTracking()
                                .Include(m => m.Calibrations)
                                .AsEnumerable()
                                  where (m.Status == InstrumentStatus.Using)
                                  where (m.Calibrations.Count > 0 && m.Calibrations.Last().Date != DateTime.MinValue)
                                  let remainDay = m.Calibrations.Last().Date.AddYears(m.CalibrationCycle) - DateTime.Today
                                  where remainDay.Days < 30 // 到期前30天内
                                  select m)
                                .ToList();

            MalfunctionWorkOrderOfFollowNumber = (from m in _malfunctionContext.MalfunctionWorkOrder
                                                  where m.Progress != WorkOrderProgress.Completed
                                                  select m)
                                                  .Count();
        }
    }
}
