using EquipmentManagementSystem.Models;
using System.Collections.Generic;
namespace EquipmentManagementSystem.Interfaces
{
    public interface IMaintenanceContentRepository : IGenericRepository<MaintenanceContent>
    {
        public List<MaintenanceContent> GetByInstrumentPlatform(string platform);
    }
}
