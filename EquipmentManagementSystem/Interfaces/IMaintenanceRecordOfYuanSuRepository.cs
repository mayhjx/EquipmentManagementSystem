using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EquipmentManagementSystem.Models;

namespace EquipmentManagementSystem.Interfaces
{
    public interface IMaintenanceRecordOfYuanSuRepository : IGenericRepository<MaintenanceRecordOfYuanSu>
    {
        Task CreateRecords(List<MaintenanceRecordOfYuanSu> maintenanceRecords);
        List<MaintenanceRecordOfYuanSu> GetAllByInstrumentIdAndYearAndMonth(string instrumentId, DateTime? date);

        // 待维护提醒 TODO
    }
}
