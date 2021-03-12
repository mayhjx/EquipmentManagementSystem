using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EquipmentManagementSystem.Data;
using EquipmentManagementSystem.Models;
using EquipmentManagementSystem.Interfaces;

namespace EquipmentManagementSystem.Repositories
{
    public class RecordTableSettingRepository : GenericRepository<RecordTableSetting>, IRecordTableSettingRepository
    {
        public RecordTableSettingRepository(EquipmentContext context):base(context)
        {
        }

        public string GetMaintenanceRecordChineseTitle(string instrumentId)
        {
            return Find(i => i.InstrumentID == instrumentId).FirstOrDefault()?.MaintenanceRecordTableChineseTitle;
        }

        public string GetMaintenanceRecordEnglishTitle(string instrumentId)
        {
            return Find(i => i.InstrumentID == instrumentId).FirstOrDefault()?.MaintenanceRecordTableEnglishTitle;
        }

        public string GetMaintenanceRecordTableNumber(string instrumentId)
        {
            return Find(i => i.InstrumentID == instrumentId).FirstOrDefault()?.MaintenanceRecordTableNumber;
        }

        public string GetUsageRecordChineseTitle(string instrumentId)
        {
            return Find(i => i.InstrumentID == instrumentId).FirstOrDefault()?.UsageRecordTableChineseTitle;
        }

        public string GetUsageRecordEnglishTitle(string instrumentId)
        {
            return Find(i => i.InstrumentID == instrumentId).FirstOrDefault()?.UsageRecordTableEnglishTitle;
        }

        public string GetUsageRecordTableNumber(string instrumentId)
        {
            return Find(i => i.InstrumentID == instrumentId).FirstOrDefault()?.UsageRecordTableNumber;
        }
    }
}
