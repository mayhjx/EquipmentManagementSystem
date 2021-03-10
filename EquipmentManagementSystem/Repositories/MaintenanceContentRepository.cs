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

        public List<string> GetMaintenanceContentByInstrumentPlatform(string platform, string maintenanceType)
        {
            return _context.MaintenanceContents.Where(i => i.InstrumentPlatform == platform).Where(i => i.Type == maintenanceType).Select(i => i.Text).ToList();
        }

        public int GetMaintenanceCycleOfPlatform(string platform, string maintenanceType, string content)
        {
            // 如果不存在则返回int类型的最大值，表示无限周期
            return _context.MaintenanceContents
                .Where(i => i.InstrumentPlatform == platform)
                .Where(i => i.Type == maintenanceType)
                .FirstOrDefault(i => i.Text == content)?
                .Cycle ?? int.MaxValue;
        }

        public int GetRemindTimeOfPlatform(string platform, string maintenanceType, string content)
        {
            // 如果不存在则返回0，表示不提醒
            return _context.MaintenanceContents
                .Where(i => i.InstrumentPlatform == platform)
                .Where(i => i.Type == maintenanceType)
                .FirstOrDefault(i => i.Text == content)?
                .RemindTime ?? 0;
        }
    }
}
