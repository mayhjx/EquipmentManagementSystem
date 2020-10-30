using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EquipmentManagementSystem.Models;
using EquipmentManagementSystem.Services.Interfaces;

namespace EquipmentManagementSystem.Services
{
    public class UsageRecordService : IUsageRecordService
    {
        private readonly EfRepository<UsageRecord> _context;

        public UsageRecordService(EfRepository<UsageRecord> context)
        {
            _context = context;
        }

        public async Task<UsageRecord> GetRecordById(int id)
        {
            return await _context.GetByIdAsync(id);
        }

        public async Task<UsageRecord> AddRecord(UsageRecord usageRecord)
        {
            await _context.AddAsync(usageRecord);
            return usageRecord;
        }

        public async Task UpdateRecord(UsageRecord usageRecord)
        {
            await _context.UpdateAsync(usageRecord);
        }

        public async Task DeleteRecord(UsageRecord usageRecord)
        {
            await _context.DeleteAsync(usageRecord);
        }

        public async Task<IList<UsageRecord>> GetNotFinishedRecordsOfGroup(string groupName)
        {
            var records = await _context.ListAllAsync();
            return records.Where(r => r.EndTime == null && r.g).ToList();
        }

        public async Task<IList<UsageRecord>> GetRecordsOfGroup(string groupName)
        {
            var records = await _context.ListAllAsync();
            return records;
        }


    }
}
