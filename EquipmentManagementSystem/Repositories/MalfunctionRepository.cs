using EquipmentManagementSystem.Data;
using EquipmentManagementSystem.Interfaces;
using EquipmentManagementSystem.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EquipmentManagementSystem.Repositories
{
    public class MalfunctionRepository : GenericRepository<MalfunctionWorkOrder>, IMalfunctionRepository
    {
        public MalfunctionRepository(EquipmentContext context) : base(context)
        {
        }

        public List<MalfunctionWorkOrder> GetAllByInstrumentIdAndMonthOfBeginTime(string instrumentId, DateTime date)
        {
            return _context.Set<MalfunctionWorkOrder>()
                            .AsNoTracking()
                            .Include(i => i.MalfunctionInfo)
                            .AsEnumerable()
                            .Where(i => i.InstrumentID == instrumentId)
                            .Where(i => i.MalfunctionInfo.BeginTime.Year == date.Year)
                            .Where(i => i.MalfunctionInfo.BeginTime.Month == date.Month)
                            .ToList();
        }

        public double GetTotalMalfunctionTimeOfRecords(List<MalfunctionWorkOrder> workOrders)
        {
            double totalTime = 0;

            foreach (var order in workOrders)
            {
                var workOrderWithInclude = _context.MalfunctionWorkOrder
                .AsNoTracking()
                .Include(w => w.MalfunctionInfo)
                .Include(w => w.Repair)
                .SingleOrDefault(w => w.ID == order.ID);

                var beginTime = workOrderWithInclude.MalfunctionInfo.BeginTime;
                var endTime = workOrderWithInclude.Repair.EndTime;

                if (endTime != null)
                {
                    totalTime = (endTime.GetValueOrDefault() - beginTime).TotalHours;
                }
            }

            return Math.Round(totalTime, 1);
        }
    }
}
