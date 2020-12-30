using EquipmentManagementSystem.Interfaces;
using System;
using System.Collections.Generic;

namespace EquipmentManagementSystem.Services
{
    public class ReportService : IReportService
    {
        private readonly IUsageRecordRepository _usageRecordRepository;
        private readonly IMalfunctionRepository _malfunctionRepository;
        public ReportService(IUsageRecordRepository usageRecordRepository, IMalfunctionRepository malfunctionRepository)
        {
            _usageRecordRepository = usageRecordRepository;
            _malfunctionRepository = malfunctionRepository;
        }

        public List<double> GetMonthlyMalfunctionHoursOfInstrument(string InstrumentId, int year)
        {
            var monthlyMalfunctionHours = new List<double>();
            // 初始化为0
            for (int i = 0; i <= 11; i++)
            {
                monthlyMalfunctionHours.Add(0);
            }

            // 每月故障时长
            for (int month = 1; month <= 12; month++)
            {
                var recordsOfMonth = _malfunctionRepository.GetAllByInstrumentIdAndMonthOfBeginTime(InstrumentId, new DateTime(year, month, 01));
                var totalHours = _malfunctionRepository.GetTotalMalfunctionTimeOfRecords(recordsOfMonth);
                monthlyMalfunctionHours[month - 1] = totalHours;
            }

            return monthlyMalfunctionHours;
        }

        public List<double> GetMonthlyUsageHoursOfInstrument(string InstrumentId, int year)
        {
            var monthlyUsageHours = new List<double>();
            // 初始化为0
            for (int i = 0; i <= 11; i++)
            {
                monthlyUsageHours.Add(0);
            }

            // 每月使用时长
            for (int month = 1; month <= 12; month++)
            {
                var recordsOfMonth = _usageRecordRepository.GetAllByInstrumentIdAndMonthOfBeginTime(InstrumentId, new DateTime(year, month, 01));
                var totalHours = _usageRecordRepository.GetTotalUsageHoursOfRecords(recordsOfMonth);
                monthlyUsageHours[month - 1] = totalHours;
            }

            return monthlyUsageHours;
        }

        public List<double> GetMonthlyUsageHoursOfInstrumentPlatform(string platform, int year)
        {
            throw new NotImplementedException();
        }
    }
}
