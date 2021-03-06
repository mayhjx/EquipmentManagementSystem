using EquipmentManagementSystem.Data;
using EquipmentManagementSystem.Interfaces;
using EquipmentManagementSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EquipmentManagementSystem.Repositories
{
    public class UsageRecordRepository : GenericRepository<UsageRecord>, IUsageRecordRepository
    {
        public UsageRecordRepository(EquipmentContext context) : base(context)
        {
        }

        public List<UsageRecord> GetAllByInstrumentIdAndMonthOfBeginTime(string instrumentId, DateTime date)
        {
            return _context.Set<UsageRecord>()
                .AsEnumerable()
                .Where(i => i.InstrumentID == instrumentId)
                .Where(i => i.BeginTime.GetValueOrDefault().Year == date.Year)
                .Where(i => i.BeginTime.GetValueOrDefault().Month == date.Month)
                .OrderBy(i=> i.BeginTime)
                .ToList();
        }

        public async Task UpdateEndTime(UsageRecord usageRecord, DateTime endTime)
        {
            usageRecord.EndTime = endTime;
            await _context.SaveChangesAsync();
        }

        public new async Task Delete(UsageRecord usageRecord)
        {
            //usageRecord.IsDelete = true;
            //await _context.SaveChangesAsync();
            _context.Set<UsageRecord>().Remove(usageRecord);
            await _context.SaveChangesAsync();
        }

        public UsageRecord GetLatestRecordOfProject(string projectName, string instrumentId)
        {
            return _context.UsageRecords
                .Where(i => i.ProjectName == projectName)
                .Where(i => i.InstrumentID == instrumentId)
                .ToList()
                .LastOrDefault();
        }

        public Dictionary<char, string> GetMobilePhaseOrCarrierGasOfRecord(string instrumentId, DateTime month)
        {
            /*
             * 初始化字典，key为自增的大写字母
             * 循环某个月份某台仪器的UsageRecords
             * 判断其流动相/载气
             * 如果字典的值不包含该流动相/载气，则添加到字典中
             */
            var mobilePhaseOrCarrierGas = new Dictionary<char, string>();
            var records = GetAllByInstrumentIdAndMonthOfBeginTime(instrumentId, month);

            foreach (var r in records)
            {
                var mobilePhase = r.GetMobilePhase();
                foreach (var mp in mobilePhase)
                {
                    if (!mobilePhaseOrCarrierGas.ContainsValue(mp) && !string.IsNullOrEmpty(mp))
                    {
                        char key = (char)(mobilePhaseOrCarrierGas.Keys.Count + 65);
                        mobilePhaseOrCarrierGas.Add(key, mp);
                    }
                }
            }
            return mobilePhaseOrCarrierGas;
        }

        public Dictionary<char, string> GetColumnTypeOfRecord(string instrumentId, DateTime month)
        {
            var columnType = new Dictionary<char, string>();
            var records = GetAllByInstrumentIdAndMonthOfBeginTime(instrumentId, month);

            foreach (var r in records)
            {
                if (!columnType.ContainsValue(r.ColumnType) && !string.IsNullOrEmpty(r.ColumnType))
                {
                    char key = (char)(columnType.Keys.Count + 65);
                    columnType.Add(key, r.ColumnType);
                }
            }
            return columnType;
        }

        //public Dictionary<char, string> GetColumnNumberOfRecord(string instrumentId, DateTime month)
        //{
        //    var columnNumber = new Dictionary<char, string>();
        //    var records = GetAllByInstrumentIdAndMonthOfBeginTime(instrumentId, month);

        //    foreach (var r in records)
        //    {
        //        if (!columnNumber.ContainsValue(r.SystemOneColumnNumber) && !string.IsNullOrEmpty(r.SystemOneColumnNumber))
        //        {
        //            char key = (char)(columnNumber.Keys.Count + 65);
        //            columnNumber.Add(key, r.SystemOneColumnNumber);
        //        }
        //        if (!columnNumber.ContainsValue(r.SystemTwoColumnNumber) && !string.IsNullOrEmpty(r.SystemTwoColumnNumber))
        //        {
        //            char key = (char)(columnNumber.Keys.Count + 65);
        //            columnNumber.Add(key, r.SystemTwoColumnNumber);
        //        }

        //    }
        //    return columnNumber;
        //}

        public Dictionary<char, string> GetIonSourceOfRecord(string instrumentId, DateTime month)
        {
            var ionSource = new Dictionary<char, string>();
            var records = GetAllByInstrumentIdAndMonthOfBeginTime(instrumentId, month);

            foreach (var r in records)
            {
                if (!ionSource.ContainsValue(r.IonSource) && !string.IsNullOrEmpty(r.IonSource))
                {
                    char key = (char)(ionSource.Keys.Count + 65);
                    ionSource.Add(key, r.IonSource);
                }
            }
            return ionSource;
        }

        public Dictionary<char, string> GetDetectorOfRecord(string instrumentId, DateTime month)
        {
            var detector = new Dictionary<char, string>();
            var records = GetAllByInstrumentIdAndMonthOfBeginTime(instrumentId, month);

            foreach (var r in records)
            {
                if (!detector.ContainsValue(r.Detector) && !string.IsNullOrEmpty(r.Detector))
                {
                    char key = (char)(detector.Keys.Count + 65);
                    detector.Add(key, r.Detector);
                }
            }
            return detector;
        }

        public double GetTotalUsageHoursOfRecords(List<UsageRecord> usageRecords)
        {
            double total = 0;
            foreach (var record in usageRecords)
            {
                var beginTime = record.BeginTime;
                var endTime = record.EndTime;

                if (beginTime != null && endTime != null)
                {
                    total += (endTime - beginTime).GetValueOrDefault().TotalHours;
                }
            }
            return Math.Round(total, 1);
        }

        public int GetTotalSampleNumberOfRecords(List<UsageRecord> usageRecords)
        {
            int total = 0;
            foreach (var record in usageRecords)
            {
                var number = record.SampleNumber.GetValueOrDefault();
                total += number;
            }
            return total;
        }

        public int GetTotalBatchNumberOfRecords(List<UsageRecord> usageRecords)
        {
            int total = 0;
            foreach (var record in usageRecords)
            {
                var s1Number = record.SystemOneBatchNumber.GetValueOrDefault();
                var s2Number = record.SystemTwoBatchNumber.GetValueOrDefault();
                total += s1Number + s2Number;
            }
            return total;
        }

        public int GetTotalS1BatchNumberOfRecords(List<UsageRecord> usageRecords)
        {
            int total = 0;
            foreach (var record in usageRecords)
            {
                var number = record.SystemOneBatchNumber.GetValueOrDefault();
                total += number;
            }
            return total;
        }

        public int GetTotalS2BatchNumberOfRecords(List<UsageRecord> usageRecords)
        {
            int total = 0;
            foreach (var record in usageRecords)
            {
                var number = record.SystemTwoBatchNumber.GetValueOrDefault();
                total += number;
            }
            return total;
        }
    }
}
