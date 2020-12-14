using EquipmentManagementSystem.Data;
using EquipmentManagementSystem.Interfaces;
using EquipmentManagementSystem.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EquipmentManagementSystem.Repositories
{
    public class AuditTrailRepository : IAuditTrailRepository
    {
        private readonly EquipmentContext _context;
        public AuditTrailRepository(EquipmentContext context)
        {
            _context = context;
        }

        // TODO 限制时间范围或记录数量
        public async Task<List<AuditTrailLog>> GetAuditTrailLogs(string entityName, int? id = null)
        {
            return await _context.AuditTrailLogs
                .AsNoTracking()
                .Where(l => l.EntityName == entityName)
                .Where(l => id != null && l.PrimaryKeyValue == id.ToString())
                .OrderByDescending(l => l.DateChanged)
                .ToListAsync();
        }
    }
}
