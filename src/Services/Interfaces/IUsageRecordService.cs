using EquipmentManagementSystem.Models.Record;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EquipmentManagementSystem.Services.Interfaces
{
    public interface IUsageRecordService
    {
        Task<UsageRecord> GetByIdAsync(int id);
        Task AddAsync(UsageRecord usageRecord);
        Task UpdateAsync(UsageRecord usageRecord);
        Task DeleteAsync(UsageRecord usageRecord);
        Task<List<UsageRecord>> ListAllAsync();

        Task<List<UsageRecord>> ListAllByGroupAsync(string groupName);
        Task<List<UsageRecord>> ListAllByProjectAsync(string projectName);
        Task<List<UsageRecord>> ListAllByInstrumentAsync(string instrumentId);

        Dictionary<string, List<float>> GetLastNColumnPressure(string projectName, string instrumentId, int n);
        Dictionary<string, List<float>> GetLastNVacuumDegree(string projectName, string instrumentId, int n);
        Dictionary<string, List<float>> GetLastNTest(string projectName, string instrumentId, int n);

        Task<UsageRecord> GetLastestRecordByProjectAndInstrumentAsync(string projectName, string instrumentId);
    }
}
