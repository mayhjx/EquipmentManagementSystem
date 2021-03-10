using EquipmentManagementSystem.Interfaces;
using EquipmentManagementSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static EquipmentManagementSystem.Pages.IndexModel;

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

            List<string> contents = _contentRepository.GetMaintenanceContentByInstrumentPlatform(platform, MaintenanceType.Daily);
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
            //var recordsHasDaily = records.Where(i => !string.IsNullOrEmpty(i.Daily)).ToList();

            foreach (var record in records)
            {
                var day = record.BeginTime.GetValueOrDefault().Day;
                var Operator = !string.IsNullOrEmpty(record.Daily) ? record.Operator : "/";
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

            List<string> contents = _contentRepository.GetMaintenanceContentByInstrumentPlatform(platform, MaintenanceType.Weekly);
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

        public List<string> GetWeeklyMaintenanceOperatorOfMonth(string instrumentId, DateTime month)
        {
            var weeklyMaintenanceOperator = InitialList(4);

            List<MaintenanceRecord> records = _recordRepository.GetAllByInstrumentIdAndYearAndMonth(instrumentId, month);
            var recordsHasWeekly = records.Where(i => !string.IsNullOrEmpty(i.Weekly)).ToList();

            var i = 0;
            foreach (var record in recordsHasWeekly)
            {
                if (i == 4) break;
                var op = record.Operator;
                var date = record.BeginTime.GetValueOrDefault().ToString("yyyy-MM-dd");
                weeklyMaintenanceOperator[i++] = $"{op}/{date}";
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

        // 返回某月每天对应的记录Id，用于Ajax请求
        public List<string> GetRecordIdOfMonth(List<MaintenanceRecord> maintenanceRecords)
        {
            // 一个月固定31天
            var recordId = InitialList(31);

            foreach (var record in maintenanceRecords)
            {
                var day = record.BeginTime.GetValueOrDefault().Day;
                var id = record.Id.ToString();
                recordId[day - 1] = id;
            }

            return recordId;
        }

        // 获取不同设备最新一条的季度维护记录
        // 根据设备编号获取所属平台
        // 计算该维护记录已过多少天，设为day
        // 获取该平台该维护类型的维护周期cycle和提醒时间remindTime
        // 如果day > cycle or cycle - day <= remindTime， 提醒
        public async Task<List<MaintenanceInfo>> GetToBeMaintenanceInfoOfQuarterly()
        {
            const string maintenanceType = "季度维护";
            var info = new List<MaintenanceInfo>();
            var instrumentIds = _instrumentRepository.GetAllInstrumentId();

            foreach (var id in instrumentIds)
            {
                var platform = (await _instrumentRepository.GetById(id)).Platform;
                var group = (await _instrumentRepository.GetById(id)).Group;

                var record = _recordRepository.GetLatestQuarterlyRecordOfInstrumentId(id);

                if (record != null)
                {
                    var quarterlyContent = record.GetQuarterly();
                    foreach (var content in quarterlyContent)
                    {
                        var remindTime = _contentRepository.GetRemindTimeOfPlatform(platform, maintenanceType, content);
                        // 为0时不提醒
                        if (remindTime == 0) continue;

                        var cycle = _contentRepository.GetMaintenanceCycleOfPlatform(platform, maintenanceType, content);
                        // 计划维护时间
                        var planMaintainDay = record.BeginTime.GetValueOrDefault().AddDays(cycle);

                        if (planMaintainDay <= DateTime.Now || (planMaintainDay - DateTime.Now ).Days <= remindTime)
                        {
                            info.Add(new MaintenanceInfo
                            {
                                Group = group,
                                InstrumentId = id,
                                MaintenanceType = maintenanceType,
                                MaintenanceContent = content,
                                MaintenanceTime = record.BeginTime.GetValueOrDefault(),
                                NextMaintenanceTime = record.BeginTime.GetValueOrDefault().AddDays(cycle)
                            });
                        }
                    }
                }
            }

            return info;
        }

        public async Task<List<MaintenanceInfo>> GetToBeMaintenanceInfoOfYearly()
        {
            const string maintenanceType = "年度维护";
            var info = new List<MaintenanceInfo>();
            var instrumentIds = _instrumentRepository.GetAllInstrumentId();

            foreach (var id in instrumentIds)
            {
                var platform = (await _instrumentRepository.GetById(id)).Platform;
                var group = (await _instrumentRepository.GetById(id)).Group;

                var record = _recordRepository.GetLatestYearlyRecordOfInstrumentId(id);

                if (record != null)
                {
                    var quarterlyContent = record.GetYearly();
                    foreach (var content in quarterlyContent)
                    {
                        var cycle = _contentRepository.GetMaintenanceCycleOfPlatform(platform, maintenanceType, content);
                        var remindTime = _contentRepository.GetRemindTimeOfPlatform(platform, maintenanceType, content);

                        // 为0时不提醒
                        if (remindTime == 0) continue;

                        // 距离上次维护已过多少天
                        var day = (DateTime.Now - record.BeginTime.GetValueOrDefault()).Days;

                        if (day >= cycle || (cycle - day) <= remindTime)
                        {
                            info.Add(new MaintenanceInfo
                            {
                                Group = group,
                                InstrumentId = id,
                                MaintenanceType = maintenanceType,
                                MaintenanceContent = content,
                                MaintenanceTime = record.BeginTime.GetValueOrDefault(),
                                NextMaintenanceTime = record.BeginTime.GetValueOrDefault().AddDays(cycle)
                            });
                        }
                    }
                }
            }

            return info;
        }
    }
}
