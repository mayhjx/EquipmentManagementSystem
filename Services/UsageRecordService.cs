using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using EquipmentManagementSystem.Data;
using EquipmentManagementSystem.Models;
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

        public async Task<bool> IsValidAsync(int id)
        {
            return await _context.UsageRecords.AnyAsync(r => r.Id == id);
        }

        public bool IsAuthorized()
        {
            throw new NotImplementedException();
        }

        public async Task<UsageRecord> GetRecordAsync(int id)
        {
            var isValid = await IsValidAsync(id);
            if (!isValid)
            {
                throw new ArgumentNullException("Not Found");
            }
            return await _context.UsageRecords.FirstOrDefaultAsync(r => r.Id == id);
        }

        public async Task<List<UsageRecord>> GetRecordListAsync()
        {
            return await _context.UsageRecords.AsNoTracking().ToListAsync();
        }

        public async Task AddRecordAsync(UsageRecord usageRecord)
        {
            await _context.UsageRecords.AddAsync(usageRecord);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateRecordAsync(UsageRecord usageRecord)
        {
            _context.UsageRecords.Attach(usageRecord).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await IsValidAsync(usageRecord.Id))
                {
                    throw new ArgumentNullException("Not Found");
                }
                else
                {
                    throw;
                }
            }
        }

        public async Task DeleteRecordAsync(int id)
        {
            var isValid = await IsValidAsync(id);
            if (!isValid)
            {
                throw new ArgumentNullException("Not Found");
            }
            var usageRecord = await GetRecordAsync(id);
            _context.UsageRecords.Remove(usageRecord);
            await _context.SaveChangesAsync();
        }
    }
}
