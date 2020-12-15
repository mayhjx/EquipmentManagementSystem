using EquipmentManagementSystem.Data;
using EquipmentManagementSystem.Interfaces;
using EquipmentManagementSystem.Models;
using Microsoft.EntityFrameworkCore;
using System;
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

        public async Task<List<AuditTrailLog>> GetAuditTrailLogs(string entityName, int? pk = null, DateTime? yearOrMonth = null)
        {
            var result = _context.AuditTrailLogs
                .Where(l => l.EntityName == entityName);

            // 限制时间范围
            if (yearOrMonth != null)
            {
                result = result.Where(l => l.DateChanged.Year == yearOrMonth.GetValueOrDefault().Year &&
                                            l.DateChanged.Month == yearOrMonth.GetValueOrDefault().Month);
            }

            if (pk != null)
            {
                result = result.Where(l => l.PrimaryKeyValue == pk.ToString());
            }

            return await result.OrderByDescending(l => l.DateChanged).ToListAsync();
        }
    }
}
