using EquipmentManagementSystem.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EquipmentManagementSystem.Services.Interfaces
{
    public interface IAuditTrailService
    {
        Task<List<AuditTrailLog>> GetAuditTrailLogsAsync(string entityName);
    }
}
