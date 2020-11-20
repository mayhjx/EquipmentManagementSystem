using EquipmentManagementSystem.Data;
using EquipmentManagementSystem.Models;
using EquipmentManagementSystem.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EquipmentManagementSystem.Services
{
    public class MaintenanceRecordService : IMaintenanceRecordService
    {
        private readonly EquipmentContext _context;

        public MaintenanceRecordService(EquipmentContext context)
        {
            _context = context;
        }

        public async Task AddAsync(MaintenanceRecord maintenanceRecord)
        {
            await _context.AddAsync(maintenanceRecord);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(MaintenanceRecord maintenanceRecord)
        {
            _context.Attach(maintenanceRecord).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(MaintenanceRecord maintenanceRecord)
        {
            _context.Remove(maintenanceRecord);
            await _context.SaveChangesAsync();
        }

        public async Task<MaintenanceRecord> GetByIdAsync(int id)
        {
            return await _context.MaintenanceRecords.FindAsync(id);
        }

        public async Task<List<MaintenanceRecord>> ListAllAsync()
        {
            return await _context.MaintenanceRecords.AsNoTracking().ToListAsync();
        }

        public async Task<List<MaintenanceRecord>> ListAllByGroupAsync(string groupName)
        {
            return await _context.MaintenanceRecords.Where(r => r.GroupName == groupName).AsNoTracking().ToListAsync();

        }

        public async Task<List<MaintenanceRecord>> ListAllByInstrumentAsync(string instrumentId)
        {
            return await _context.MaintenanceRecords.Where(r => r.InstrumentId == instrumentId).AsNoTracking().ToListAsync();
        }

        public async Task<List<MaintenanceRecord>> ListAllByProjectAsync(string projectName)
        {
            return await _context.MaintenanceRecords.Where(r => r.ProjectName == projectName).AsNoTracking().ToListAsync();
        }
    }
}
