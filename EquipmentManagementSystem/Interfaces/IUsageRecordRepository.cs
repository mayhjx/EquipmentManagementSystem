using EquipmentManagementSystem.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EquipmentManagementSystem.Interfaces
{
    public interface IUsageRecordRepository : IGenericRepository<UsageRecord>
    {
        UsageRecord GetLatestRecordOfProject(string projectName);
        List<UsageRecord> GetAllByInstrumentIdAndBeginTime(string instrumentId, DateTime? date);
        Task UpdateEndTime(UsageRecord usageRecord, DateTime endTime);

        /// <summary>
        /// 返回某个月份某台仪器的使用记录的流动相/载气
        /// </summary>
        /// <param name="instrumentId"></param>
        /// <param name="month"></param>
        /// <returns>字典，key是自增的大写字母，value是流动相/载气</returns>
        Dictionary<char, string> GetMobilePhaseOrCarrierGasOfRecord(string instrumentId, DateTime month);
        Dictionary<char, string> GetColumnTypeOfRecord(string instrumentId, DateTime month);
        Dictionary<char, string> GetIonSourceOfRecord(string instrumentId, DateTime month);
        Dictionary<char, string> GetDetectorOfRecord(string instrumentId, DateTime month);
    }
}
