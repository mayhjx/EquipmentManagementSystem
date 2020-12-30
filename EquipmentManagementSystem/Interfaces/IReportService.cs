using System.Collections.Generic;

namespace EquipmentManagementSystem.Interfaces
{
    public interface IReportService
    {
        /// <summary>
        /// 某仪器某年每月使用时长
        /// </summary>
        /// <param name="InstrumentId"></param>
        /// <param name="year"></param>
        /// <returns></returns>
        List<double> GetMonthlyUsageHoursOfInstrument(string InstrumentId, int year);

        /// <summary>
        /// 某仪器某年每月故障时长
        /// </summary>
        /// <param name="InstrumentId"></param>
        /// <param name="year"></param>
        /// <returns></returns>
        List<double> GetMonthlyMalfunctionHoursOfInstrument(string InstrumentId, int year);

        /// <summary>
        /// 某仪器平台某年每月使用时长
        /// </summary>
        /// <param name="InstrumentId"></param>
        /// <param name="year"></param>
        /// <returns></returns>
        List<double> GetMonthlyUsageHoursOfInstrumentPlatform(string platform, int year);

    }
}
