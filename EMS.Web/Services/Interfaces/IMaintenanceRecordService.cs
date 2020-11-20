using EquipmentManagementSystem.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EquipmentManagementSystem.Services.Interfaces
{
    public interface IMaintenanceRecordService
    {
        Task<MaintenanceRecord> GetByIdAsync(int id);
        Task AddAsync(MaintenanceRecord maintenanceRecord);
        Task UpdateAsync(MaintenanceRecord maintenanceRecord);
        Task DeleteAsync(MaintenanceRecord maintenanceRecord);
        Task<List<MaintenanceRecord>> ListAllAsync();

        Task<List<MaintenanceRecord>> ListAllByGroupAsync(string groupName);
        Task<List<MaintenanceRecord>> ListAllByProjectAsync(string projectName);
        Task<List<MaintenanceRecord>> ListAllByInstrumentAsync(string instrumentId);
    }
}
