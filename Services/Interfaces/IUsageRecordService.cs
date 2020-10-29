using System.Collections.Generic;
using System.Threading.Tasks;
using EquipmentManagementSystem.Models;

namespace EquipmentManagementSystem.Services.Interfaces
{
    interface IUsageRecordService
    {
        Task<bool> IsValidAsync(int id);
        bool IsAuthorized();
        Task AddRecordAsync(UsageRecord usageRecord);
        Task<List<UsageRecord>> GetRecordListAsync();
        Task<UsageRecord> GetRecordAsync(int id);
        Task UpdateRecordAsync(UsageRecord usageRecord);
        Task DeleteRecordAsync(int id);
    }
}
