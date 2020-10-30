using System.Collections.Generic;
using System.Threading.Tasks;
using EquipmentManagementSystem.Models;

namespace EquipmentManagementSystem.Services.Interfaces
{
    interface IUsageRecordService
    {
        Task<UsageRecord> GetRecordById(int id);

        Task<UsageRecord> AddRecord(UsageRecord usageRecord);

        Task UpdateRecord(UsageRecord usageRecord);

        Task DeleteRecord(UsageRecord usageRecord);

        Task<IList<UsageRecord>> GetRecordsOfGroup(string groupName);

        Task<IList<UsageRecord>> GetNotFinishedRecordsOfGroup(string groupName);

        //Task<List<AuditTrailLog>> GetAuditTrailLogs(string entityName);
    }
}
