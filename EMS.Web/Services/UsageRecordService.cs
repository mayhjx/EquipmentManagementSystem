using EquipmentManagementSystem.Data;
using EquipmentManagementSystem.Models.Record;
using EquipmentManagementSystem.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EquipmentManagementSystem.Services
{
    public class UsageRecordService : IUsageRecordService
    {
        private readonly EquipmentContext _context;
        public UsageRecordService(EquipmentContext context)
        {
            _context = context;
        }

        public async Task AddAsync(UsageRecord usageRecord)
        {
            await _context.AddAsync(usageRecord);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(UsageRecord usageRecord)
        {
            _context.Attach(usageRecord).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(UsageRecord usageRecord)
        {
            _context.Remove(usageRecord);
            await _context.SaveChangesAsync();
        }

        public async Task<UsageRecord> GetByIdAsync(int id)
        {
            return await _context.Set<UsageRecord>().FindAsync(id);
        }

        /// <summary>
        /// 获取某项目某仪器最新的使用记录
        /// </summary>
        /// <param name="projectName">项目名</param>
        /// <param name="instrumentId">仪器编号</param>
        /// <returns>Usage Record</returns>
        public async Task<UsageRecord> GetLastestRecordByProjectAndInstrumentAsync(string projectName, string instrumentId)
        {
            return await _context.UsageRecords.LastOrDefaultAsync(r => r.ProjectName == projectName && r.InstrumentId == instrumentId);
        }

        /// <summary>
        /// 获取近N天（默认10天）的色谱柱压力值
        /// </summary>
        /// <param name="projectName">项目名</param>
        /// <param name="instrumentId">仪器编号</param>
        /// <param name="n">个数</param>
        /// <returns>字典：{"系统1":"值列表", "系统2":"值列表"}</returns>
        public Dictionary<string, List<float>> GetLastNColumnPressure(string projectName, string instrumentId, int n = 10)
        {
            var pressureDic = new Dictionary<string, List<float>>();
            var records = _context.UsageRecords.Where(r => r.ProjectName == projectName && r.InstrumentId == instrumentId).ToList().TakeLast(n);

            foreach (var record in records)
            {
                var columns = record.GetColumnInfo();
                if (columns == null) continue;
                foreach (var column in columns)
                {
                    try
                    {
                        pressureDic[column.System].Add(column.Value);
                    }
                    catch (KeyNotFoundException)
                    {
                        pressureDic.Add(column.System, new List<float> { column.Value });
                    }
                }
            }
            return pressureDic;
        }

        /// <summary>
        /// 获取近N天（默认10天）的Test信号值
        /// </summary>
        /// <param name="projectName">项目名</param>
        /// <param name="instrumentId">仪器编号</param>
        /// <param name="n">个数</param>
        /// <returns>字典：{"系统1":"值列表", "系统2":"值列表"}</returns>
        public Dictionary<string, List<float>> GetLastNTest(string projectName, string instrumentId, int n = 10)
        {
            var testDic = new Dictionary<string, List<float>>();
            var records = _context.UsageRecords.Where(r => r.ProjectName == projectName && r.InstrumentId == instrumentId).ToList().TakeLast(n);

            foreach (var record in records)
            {
                var tests = record.GetTestInfo();
                if (tests == null) continue;
                foreach (var test in tests)
                {
                    try
                    {
                        testDic[test.System].Add(test.Value);
                    }
                    catch (KeyNotFoundException)
                    {
                        testDic.Add(test.System, new List<float> { test.Value });
                    }
                }
            }
            return testDic;
        }

        /// <summary>
        /// 获取近N天（默认10天）的真空度压力值
        /// </summary>
        /// <param name="projectName">项目名</param>
        /// <param name="instrumentId">仪器编号</param>
        /// <param name="n">个数</param>
        /// <returns>字典：{"系统1":"值列表", "系统2":"值列表"}</returns>
        public Dictionary<string, List<float>> GetLastNVacuumDegree(string projectName, string instrumentId, int n = 10)
        {
            var vacuumDic = new Dictionary<string, List<float>>();
            var records = _context.UsageRecords.Where(r => r.ProjectName == projectName && r.InstrumentId == instrumentId).ToList().TakeLast(n);

            foreach (var record in records)
            {
                var vacuums = record.GetVacuumDegreeInfo();
                if (vacuums == null) continue;
                foreach (var vacuum in vacuums)
                {
                    try
                    {
                        vacuumDic[vacuum.System].Add(vacuum.Value);
                    }
                    catch (KeyNotFoundException)
                    {
                        vacuumDic.Add(vacuum.System, new List<float> { vacuum.Value });
                    }
                }
            }
            return vacuumDic;
        }

        public async Task<List<UsageRecord>> ListAllAsync()
        {
            return await _context.UsageRecords.AsNoTracking().ToListAsync();
        }

        public async Task<List<UsageRecord>> ListAllByGroupAsync(string groupName)
        {
            return await _context.UsageRecords.Where(r => r.GroupName == groupName).AsNoTracking().ToListAsync();
        }

        public async Task<List<UsageRecord>> ListAllByInstrumentAsync(string instrumentId)
        {
            return await _context.UsageRecords.Where(r => r.InstrumentId == instrumentId).AsNoTracking().ToListAsync();
        }

        public async Task<List<UsageRecord>> ListAllByProjectAsync(string projectName)
        {
            return await _context.UsageRecords.Where(r => r.ProjectName == projectName).AsNoTracking().ToListAsync();
        }
    }
}
