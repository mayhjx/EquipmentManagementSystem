using EquipmentManagementSystem.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EquipmentManagementSystem.Interfaces
{
    public interface IUsageRecordOfYuanSuRepository : IGenericRepository<UsageRecordOfYuanSu>
    {
        List<UsageRecordOfYuanSu> GetAllByInstrumentIdAndMonthOfBeginTime(string instrumentId, DateTime date);
        double GetTotalUsageHoursOfRecords(List<UsageRecordOfYuanSu> usageRecords);
    }
}
