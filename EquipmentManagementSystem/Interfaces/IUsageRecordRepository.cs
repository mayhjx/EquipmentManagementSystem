using EquipmentManagementSystem.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EquipmentManagementSystem.Interfaces
{
    public interface IUsageRecordRepository : IGenericRepository<UsageRecord>
    {
        Task CreateRecords(List<UsageRecord> usageRecords);
        List<UsageRecord> GetAllByInstrumentIdAndBeginTime(string instrumentId, DateTime? date);
        Task UpdateEndTime(UsageRecord usageRecord, DateTime endTime);
    }
}
