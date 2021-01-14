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

        /// <summary>
        /// 获取某仪器最新一条季度维护记录，用于提醒
        /// </summary>
        /// <param name="instrumentId"></param>
        /// <returns></returns>
        MaintenanceRecord GetLatestQuarterlyRecordOfInstrumentId(string instrumentId);

        /// <summary>
        /// 获取某仪器最新一条年度维护记录，用于提醒
        /// </summary>
        /// <param name="instrumentId"></param>
        /// <returns></returns>
        MaintenanceRecord GetLatestYearlyRecordOfInstrumentId(string instrumentId);
    }
}
