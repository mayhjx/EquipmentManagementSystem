using EquipmentManagementSystem.Interfaces;
using EquipmentManagementSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EquipmentManagementSystem.Services
{
    public class MaintenanceRecordService : IMaintenanceRecordService
    {
        private readonly IInstrumentRepository _instrumentRepository;
        private readonly IMaintenanceContentRepository _contentRepository;
        private readonly IMaintenanceRecordRepository _recordRepository;

        public MaintenanceRecordService(
            IInstrumentRepository instrumentRepository,
            IMaintenanceContentRepository maintenanceContentRepository,
            IMaintenanceRecordRepository maintenanceRecordRepository)
        {
            _instrumentRepository = instrumentRepository;
            _contentRepository = maintenanceContentRepository;
            _recordRepository = maintenanceRecordRepository;
        }

        public async Task<Dictionary<string, List<string>>> GetDailyMaintenanceSituationOfMonth(string instrumentId, DateTime month)
        {
            var dailyMaintenanceSituation = new Dictionary<string, List<string>>();

            var instrument = await _instrumentRepository.GetById(instrumentId);
            string platform = string.Empty;

            if (instrument != null)
            {
                platform = instrument.Platform;
            }

            List<string> contents = _contentRepository.GetDailyContentByInstrumentPlatform(platform);
            List<MaintenanceRecord> records = _recordRepository.GetAllByInstrumentIdAndYearAndMonth(instrumentId, month);

            foreach (var content in contents)
            {
                // 一个月固定31天
                List<string> situation = InitialList(31);
                foreach (var record in records)
                {
                    var day = record.BeginTime.GetValueOrDefault().Day;
                    situation[day - 1] = record.Daily.Contains(content) ? "Y" : "N";
                }
                dailyMaintenanceSituation.Add(content, situation);
            }

            return dailyMaintenanceSituation;
        }

        public List<string> GetDailyMaintenanceOperatorOfMonth(string instrumentId, DateTime month)
        {
            // 一个月固定31天
            var dailyMaintenanceOperator = InitialList(31);

            List<MaintenanceRecord> records = _recordRepository.GetAllByInstrumentIdAndYearAndMonth(instrumentId, month);

            foreach (var record in records)
            {
                var day = record.BeginTime.GetValueOrDefault().Day;
                var Operator = record.Operator;
                dailyMaintenanceOperator[day - 1] = Operator;
            }

            return dailyMaintenanceOperator;
        }


        public async Task<Dictionary<string, List<string>>> GetWeeklyMaintenanceSituationOfMonth(string instrumentId, DateTime month)
        {
            var weeklyMaintenanceSituation = new Dictionary<string, List<string>>();

            var instrument = await _instrumentRepository.GetById(instrumentId);
            string platform = string.Empty;

            if (instrument != null)
            {
                platform = instrument.Platform;
            }

            List<string> contents = _contentRepository.GetWeeklyContentByInstrumentPlatform(platform);
            List<MaintenanceRecord> records = _recordRepository.GetAllByInstrumentIdAndYearAndMonth(instrumentId, month);

            var recordsHasWeekly = records.Where(i => !string.IsNullOrEmpty(i.Weekly)).ToList();

            foreach (var content in contents)
            {
                // 初始化为长度为4的空列表，记录前4条周维护记录的情况
                List<string> situation = InitialList(4);
                int i = 0;
                foreach (var record in recordsHasWeekly)
                {
                    if (i == 4) break; // 只获取前4条记录
                    if (record.Weekly.Contains(content))
                    {
                        situation[i++] = "Y";
                    }
                    else
                    {
                        situation[i++] = "N";
                    }
                }
                weeklyMaintenanceSituation.Add(content, situation);
            }

            return weeklyMaintenanceSituation;
        }

        public async Task<List<string>> GetWeeklyMaintenanceOperatorOfMonth(string instrumentId, DateTime month)
        {
            var weeklyMaintenanceOperator = InitialList(4);

            var instrument = await _instrumentRepository.GetById(instrumentId);
            string platform = string.Empty;

            if (instrument != null)
            {
                platform = instrument.Platform;
            }

            List<string> contents = _contentRepository.GetWeeklyContentByInstrumentPlatform(platform);
            List<MaintenanceRecord> records = _recordRepository.GetAllByInstrumentIdAndYearAndMonth(instrumentId, month);
            var recordsHasWeekly = records.Where(i => !string.IsNullOrEmpty(i.Weekly)).ToList();

            var i = 0;
            foreach (var record in recordsHasWeekly)
            {
                if (i == 4) break;
                var op = record.Operator;
                var date = record.BeginTime.GetValueOrDefault().ToString("yyyy-MM-dd");
                weeklyMaintenanceOperator[i++] = $"{op} {date}";
            }

            return weeklyMaintenanceOperator;
        }

        private List<string> InitialList(int size)
        {
            // 初始化一个长度为size的空列表
            var list = new List<string>();
            for (int i = 0; i < size; i++)
            {
                list.Add("");
            }
            return list;
        }

        public List<string> GetRecordIdOfMonth(string instrumentId, DateTime month)
        {
            // 一个月固定31天
            var recordId = InitialList(31);

            List<MaintenanceRecord> records = _recordRepository.GetAllByInstrumentIdAndYearAndMonth(instrumentId, month);

            foreach (var record in records)
            {
                var day = record.BeginTime.GetValueOrDefault().Day;
                var id = record.Id.ToString();
                recordId[day - 1] = id;
            }

            return recordId;
        }
    }
}
