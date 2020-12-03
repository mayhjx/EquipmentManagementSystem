using EquipmentManagementSystem.Data;
using EquipmentManagementSystem.Interfaces;
using EquipmentManagementSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EquipmentManagementSystem.Repositories
{
    public class UsageRecordRepository : GenericRepository<UsageRecord>, IUsageRecordRepository
    {
        public UsageRecordRepository(EquipmentContext context) : base(context)
        {
        }

        public async Task CreateRecords(List<UsageRecord> usageRecords)
        {
            await _context.Set<UsageRecord>().AddRangeAsync(usageRecords);
            await _context.SaveChangesAsync();
        }

        public List<UsageRecord> GetAllByInstrumentIdAndBeginTime(string instrumentId, DateTime? date)
        {
            if (date.GetValueOrDefault() == null)
            {
                date = DateTime.Now;
            }

            return _context.Set<UsageRecord>()
                .AsEnumerable()
                .Where(i => i.IsDelete == false)
                .Where(i => i.InstrumentId == instrumentId)
                .Where(i => i.BeginTime.GetValueOrDefault().Year == date.GetValueOrDefault().Year)
                .Where(i => i.BeginTime.GetValueOrDefault().Month == date.GetValueOrDefault().Month)
                .ToList();
        }

        public async Task UpdateEndTime(UsageRecord usageRecord, DateTime endTime)
        {
            usageRecord.EndTime = endTime;
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// fake delete
        /// </summary>
        /// <param name="usageRecord"></param>
        /// <returns></returns>
        public new async Task Delete(UsageRecord usageRecord)
        {
            usageRecord.IsDelete = true;
            await _context.SaveChangesAsync();
        }
    }
}
