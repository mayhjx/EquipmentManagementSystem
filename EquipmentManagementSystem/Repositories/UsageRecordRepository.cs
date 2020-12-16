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

        public List<UsageRecord> GetAllByInstrumentIdAndBeginTime(string instrumentId, DateTime? date)
        {
            if (date.GetValueOrDefault() == null)
            {
                date = DateTime.Now;
            }

            return _context.Set<UsageRecord>()
                .AsEnumerable()
                .Where(i => i.IsDelete == false)
                .Where(i => i.InstrumentId == instrumentId)
                .Where(i => i.BeginTime.GetValueOrDefault().Year == date.GetValueOrDefault().Year)
                .Where(i => i.BeginTime.GetValueOrDefault().Month == date.GetValueOrDefault().Month)
                .ToList();
        }

        public async Task UpdateEndTime(UsageRecord usageRecord, DateTime endTime)
        {
            usageRecord.EndTime = endTime;
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// fake delete
        /// </summary>
        /// <param name="usageRecord"></param>
        /// <returns></returns>
        public new async Task Delete(UsageRecord usageRecord)
        {
            usageRecord.IsDelete = true;
            await _context.SaveChangesAsync();
        }

        public UsageRecord GetLatestRecordOfProject(string projectName)
        {
            return _context.UsageRecords
                .Where(i => i.IsDelete == false)
                .Where(i => i.ProjectName == projectName)
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
            var records = GetAllByInstrumentIdAndBeginTime(instrumentId, month);

            foreach (var r in records)
            {
                var mobilePhase = r.GetMobilePhase();
                foreach (var mp in mobilePhase)
                {
                    if (!mobilePhaseOrCarrierGas.ContainsValue(mp))
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
            var records = GetAllByInstrumentIdAndBeginTime(instrumentId, month);

            foreach (var r in records)
            {
                if (!columnType.ContainsValue(r.ColumnType))
                {
                    char key = (char)(columnType.Keys.Count + 65);
                    columnType.Add(key, r.ColumnType);
                }
            }
            return columnType;
        }

        public Dictionary<char, string> GetIonSourceOfRecord(string instrumentId, DateTime month)
        {
            var ionSource = new Dictionary<char, string>();
            var records = GetAllByInstrumentIdAndBeginTime(instrumentId, month);

            foreach (var r in records)
            {
                if (!ionSource.ContainsValue(r.IonSource))
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
            var records = GetAllByInstrumentIdAndBeginTime(instrumentId, month);

            foreach (var r in records)
            {
                if (!detector.ContainsValue(r.Detector))
                {
                    char key = (char)(detector.Keys.Count + 65);
                    detector.Add(key, r.Detector);
                }
            }
            return detector;
        }
    }
}
