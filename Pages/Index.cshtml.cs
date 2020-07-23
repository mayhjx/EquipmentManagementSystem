using System;
using System.Collections.Generic;
using System.Linq;
using EquipmentManagementSystem.Data;
using EquipmentManagementSystem.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace EquipmentManagementSystem.Pages
{
    [AllowAnonymous]
    public class IndexModel : PageModel
    {
        private readonly EquipmentContext _equipmentContext;
        private readonly MalfunctionContext _malfunctionContext;

        public IndexModel(EquipmentContext context, MalfunctionContext malfunctionContext)
        {
            _equipmentContext = context;
            _malfunctionContext = malfunctionContext;
        }

        // 主检设备数量
        public int InstrumentNumber { get; set; }

        // 待校准的主检设备
        public IList<Instrument> InstrumentOfExpire { get; set; }

        // 待跟进的故障工单
        public int MalfunctionWorkOrderOfFollowNumber { get; set; }

        // 故障
        public void OnGet()
        {
            InstrumentNumber = _equipmentContext.Instruments.Count();

            InstrumentOfExpire = (from m in _equipmentContext.Instruments
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
