using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EquipmentManagementSystem.Models;

namespace EquipmentManagementSystem.Interfaces
{
    public interface IRecordTableSettingRepository:IGenericRepository<RecordTableSetting>
    {
        public string GetUsageRecordChineseTitle(string instrumentId);
        public string GetUsageRecordEnglishTitle(string instrumentId);
        public string GetUsageRecordTableNumber(string instrumentId);

        public string GetMaintenanceRecordChineseTitle(string instrumentId);
        public string GetMaintenanceRecordEnglishTitle(string instrumentId);
        public string GetMaintenanceRecordTableNumber(string instrumentId);
    }
}
