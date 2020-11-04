using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EquipmentManagementSystem.Data;
using EquipmentManagementSystem.Models.Record;
using EquipmentManagementSystem.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

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
            _context.Add(usageRecord);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(UsageRecord usageRecord)
        {
            _context.Remove(usageRecord);
            await _context.SaveChangesAsync();
        }

        public async Task<UsageRecord> GetByIdAsync(int id)
        {
            return await _context.UsageRecords.FindAsync(id);
        }

        public async Task<UsageRecord> GetLastestRecordByProjectAndInstrumentAsync(string projectName, string instrumentId)
        {
            return await _context.UsageRecords.LastOrDefaultAsync(r => r.ProjectName == projectName && r.InstrumentId == instrumentId);
        }

        public Task<List<float>> GetLastNColumnPressureAsync(string projectName, string instrument, int n)
        {
            throw new System.NotImplementedException();
        }

        public Task<List<float>> GetLastNTestAsync(string projectName, string instrument, int n)
        {
            throw new System.NotImplementedException();
        }

        public Task<List<float>> GetLastNVacuumDegreeAsync(string projectName, string instrument, int n)
        {
            throw new System.NotImplementedException();
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

        public async Task UpdateAsync(UsageRecord usageRecord)
        {
            _context.Attach(usageRecord).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task UpdateEndTimeAsync(int id, DateTime endTime)
        {
            var usageRecord = await _context.UsageRecords.FindAsync(id);
            usageRecord.EndTime = endTime;
            _context.Attach(usageRecord).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }
    }
}
