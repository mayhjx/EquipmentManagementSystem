using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EquipmentManagementSystem.Interfaces
{
    public interface IMaintenanceRecordService
    {
        /// <summary>
        /// 返回某个月份某台仪器所有维护记录的id，用于在表格中修改和删除
        /// </summary>
        /// <param name="instrumentId"></param>
        /// <param name="month"></param>
        /// <returns></returns>
        List<string> GetRecordIdOfMonth(string instrumentId, DateTime month);
        /// <summary>
        /// 返回某个月份某台仪器的日常维护情况
        /// </summary>
        /// <param name="instrumentId">仪器编号</param>
        /// <param name="month">月份</param>
        /// <returns>字典：key：日常维护内容，value：当月每天的维护情况（Y/N）的列表</returns>
        Task<Dictionary<string, List<string>>> GetDailyMaintenanceSituationOfMonth(string instrumentId, DateTime month);

        /// <summary>
        /// 返回日常维护的操作者
        /// </summary>
        /// <param name="instrumentId">仪器编号</param>
        /// <param name="month">月份</param>
        /// <returns></returns>
        List<string> GetDailyMaintenanceOperatorOfMonth(string instrumentId, DateTime month);

        /// <summary>
        /// 返回某个月份某台仪器的周维护情况
        /// </summary>
        /// <param name="instrumentId">仪器编号</param>
        /// <param name="month">月份</param>
        /// <returns>字典：key：周维护内容，value：当月周维护情况（Y/N）的列表</returns>
        Task<Dictionary<string, List<string>>> GetWeeklyMaintenanceSituationOfMonth(string instrumentId, DateTime month);

        /// <summary>
        /// 返回某个月份某台仪器的周维护操作者
        /// </summary>
        /// <param name="instrumentId">仪器编号</param>
        /// <param name="month">月份</param>
        /// <returns></returns>
        List<string> GetWeeklyMaintenanceOperatorOfMonth(string instrumentId, DateTime month);
    }
}
