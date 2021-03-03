using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EquipmentManagementSystem.Interfaces;
using EquipmentManagementSystem.Models;
using EquipmentManagementSystem.Data;

namespace EquipmentManagementSystem.Repositories
{
    public class MaintenanceRecordOfYuanSuRepository : GenericRepository<MaintenanceRecordOfYuanSu>, IMaintenanceRecordOfYuanSuRepository
    {
        public MaintenanceRecordOfYuanSuRepository(EquipmentContext context) : base(context)
        {
        }

        public async Task CreateRecords(List<MaintenanceRecordOfYuanSu> maintenanceRecords)
        {
            await _context.Set<MaintenanceRecordOfYuanSu>().AddRangeAsync(maintenanceRecords);
            await _context.SaveChangesAsync();
        }

        public List<MaintenanceRecordOfYuanSu> GetAllByInstrumentIdAndYearAndMonth(string instrumentId, DateTime? date)
        {
            if (date.GetValueOrDefault() == null)
            {
                date = DateTime.Now;
            }

            return _context.Set<MaintenanceRecordOfYuanSu>()
                .AsEnumerable()
                .Where(i => i.InstrumentID == instrumentId)
                .Where(i => i.BeginTime.GetValueOrDefault().Year == date.GetValueOrDefault().Year)
                .Where(i => i.BeginTime.GetValueOrDefault().Month == date.GetValueOrDefault().Month)
                .ToList();
        }
    }
}
