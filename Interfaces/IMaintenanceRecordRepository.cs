using EquipmentManagementSystem.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EquipmentManagementSystem.Interfaces
{
    public interface IMaintenanceRecordRepository : IGenericRepository<MaintenanceRecord>
    {
        Task CreateRecords(List<MaintenanceRecord> usageRecords);
        List<MaintenanceRecord> GetAllByInstrumentIdAndYearAndMonth(string instrumentId, DateTime? date);
    }
}
