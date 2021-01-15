using EquipmentManagementSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EquipmentManagementSystem.Interfaces
{
    public interface IAuditTrailRepository
    {
        Task<List<AuditTrailLog>> GetAuditTrailLogs(string entityName, int? pk = null, DateTime? date = null);
        IEnumerable<IGrouping<string, AuditTrailLog>> GetAuditTrailLogsGroupingByPK(string entityName, DateTime? date = null);
        IEnumerable<IGrouping<string, AuditTrailLog>> GetAuditTrailLogsGroupingByPKOfInstrumentId(string entityName, string instrumentId, DateTime? date = null);
    }
}
