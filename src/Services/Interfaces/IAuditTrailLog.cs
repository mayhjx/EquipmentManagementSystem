using System.Collections.Generic;
using System.Threading.Tasks;
using EquipmentManagementSystem.Models;

namespace EquipmentManagementSystem.Services.Interfaces
{
    public interface IAuditTrailLog
    {
        Task<List<AuditTrailLog>> GetAuditTrailLogs(string entityName);
    }
}
