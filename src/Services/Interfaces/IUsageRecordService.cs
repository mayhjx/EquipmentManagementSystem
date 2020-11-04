using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using EquipmentManagementSystem.Models.Record;

namespace EquipmentManagementSystem.Services.Interfaces
{
    interface IUsageRecordService
    {
        Task<UsageRecord> GetByIdAsync(int id);
        Task AddAsync(UsageRecord usageRecord);
        Task UpdateAsync(UsageRecord usageRecord);
        Task UpdateEndTimeAsync(int id, DateTime endTime);
        Task DeleteAsync(UsageRecord usageRecord);
        Task<List<UsageRecord>> ListAllAsync();

        Task<List<UsageRecord>> ListAllByGroupAsync(string groupName);
        Task<List<UsageRecord>> ListAllByProjectAsync(string projectName);
        Task<List<UsageRecord>> ListAllByInstrumentAsync(string instrumentName);

        Task<List<float>> GetLastNColumnPressureAsync(string projectName, string instrument, int n);
        Task<List<float>> GetLastNVacuumDegreeAsync(string projectName, string instrument, int n);
        Task<List<float>> GetLastNTestAsync(string projectName, string instrument, int n);

        Task<UsageRecord> GetLastestRecordByProjectAndInstrumentAsync(string projectName, string instrument);
    }
}
