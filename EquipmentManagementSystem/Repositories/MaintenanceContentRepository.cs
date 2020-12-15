using EquipmentManagementSystem.Data;
using EquipmentManagementSystem.Interfaces;
using EquipmentManagementSystem.Models;
using System.Collections.Generic;
using System.Linq;

namespace EquipmentManagementSystem.Repositories
{
    public class MaintenanceContentRepository : GenericRepository<MaintenanceContent>, IMaintenanceContentRepository
    {
        public MaintenanceContentRepository(EquipmentContext context) : base(context)
        {
        }

        public List<MaintenanceContent> GetByInstrumentPlatform(string platform)
        {
            return _context.MaintenanceContents.Where(i => i.InstrumentPlatform == platform).ToList();
        }

        public List<string> GetDailyContentByInstrumentPlatform(string platform)
        {
            return _context.MaintenanceContents.Where(i => i.InstrumentPlatform == platform).Where(i => i.Type == "日常维护").Select(i => i.Text).ToList();
        }

        public List<string> GetWeeklyContentByInstrumentPlatform(string platform)
        {
            return _context.MaintenanceContents.Where(i => i.InstrumentPlatform == platform).Where(i => i.Type == "周维护").Select(i => i.Text).ToList();
        }
    }
}
