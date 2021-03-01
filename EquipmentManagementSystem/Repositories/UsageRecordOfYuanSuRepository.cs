using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EquipmentManagementSystem.Interfaces;
using EquipmentManagementSystem.Models;
using EquipmentManagementSystem.Data;

namespace EquipmentManagementSystem.Repositories
{
    public class UsageRecordOfYuanSuRepository : GenericRepository<UsageRecordOfYuanSu>, IUsageRecordOfYuanSuRepository
    {
        public UsageRecordOfYuanSuRepository(EquipmentContext context):base(context)
        {
        }

        public List<UsageRecordOfYuanSu> GetAllByInstrumentIdAndMonthOfBeginTime(string instrumentId, DateTime date)
        {
            return _context.Set<UsageRecordOfYuanSu>()
                .AsEnumerable()
                .Where(i => i.InstrumentID == instrumentId)
                .Where(i => i.BeginTime.GetValueOrDefault().Year == date.Year)
                .Where(i => i.BeginTime.GetValueOrDefault().Month == date.Month)
                .OrderBy(i => i.BeginTime)
                .ToList();
        }

        public double GetTotalUsageHoursOfRecords(List<UsageRecordOfYuanSu> usageRecords)
        {
            double total = 0;
            foreach (var record in usageRecords)
            {
                var beginTime = record.BeginTime;
                var endTime = record.EndTime;

                if (beginTime != null && endTime != null)
                {
                    total += (endTime - beginTime).GetValueOrDefault().TotalHours;
                }
            }
            return Math.Round(total, 1);
        }
    }
}
