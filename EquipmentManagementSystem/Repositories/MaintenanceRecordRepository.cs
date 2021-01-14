using EquipmentManagementSystem.Data;
using EquipmentManagementSystem.Interfaces;
using EquipmentManagementSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace EquipmentManagementSystem.Repositories
{
    public class MaintenanceRecordRepository : GenericRepository<MaintenanceRecord>, IMaintenanceRecordRepository
    {
        public MaintenanceRecordRepository(EquipmentContext context) : base(context)
        {
        }

        public async Task CreateRecords(List<MaintenanceRecord> usageRecords)
        {
            await _context.Set<MaintenanceRecord>().AddRangeAsync(usageRecords);
            await _context.SaveChangesAsync();
        }

        public List<MaintenanceRecord> GetAllByInstrumentIdAndYearAndMonth(string instrumentId, DateTime? date)
        {
            if (date.GetValueOrDefault() == null)
            {
                date = DateTime.Now;
            }

            return _context.Set<MaintenanceRecord>()
                .AsEnumerable()
                .Where(i => i.InstrumentId == instrumentId)
                .Where(i => i.BeginTime.GetValueOrDefault().Year == date.GetValueOrDefault().Year)
                .Where(i => i.BeginTime.GetValueOrDefault().Month == date.GetValueOrDefault().Month)
                .ToList();
        }

        public MaintenanceRecord GetLatestQuarterlyRecordOfInstrumentId(string instrumentId)
        {
            return _context.Set<MaintenanceRecord>()
                .AsNoTracking()
                .AsEnumerable()
                .OrderBy(i => i.BeginTime)
                .Where(i => i.InstrumentId == instrumentId)
                .Where(i => !string.IsNullOrEmpty(i.Quarterly))
                .LastOrDefault();
        }

        public MaintenanceRecord GetLatestYearlyRecordOfInstrumentId(string instrumentId)
        {
            return _context.Set<MaintenanceRecord>()
                .AsNoTracking()
                .AsEnumerable()
                .OrderBy(i => i.BeginTime)
                .Where(i => i.InstrumentId == instrumentId)
                .Where(i => !string.IsNullOrEmpty(i.Yearly))
                .LastOrDefault();
        }
    }
}
