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
    }
}
