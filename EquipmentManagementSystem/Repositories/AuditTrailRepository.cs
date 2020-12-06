using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EquipmentManagementSystem.Data;
using EquipmentManagementSystem.Models;
using EquipmentManagementSystem.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace EquipmentManagementSystem.Repositories
{
    public class AuditTrailRepository : IAuditTrailRepository
    {
        private readonly EquipmentContext _context;
        public AuditTrailRepository(EquipmentContext context)
        {
            _context = context;
        }

        public async Task<List<AuditTrailLog>> GetAuditTrailLogs(string entityName)
        {
            return await _context.AuditTrailLogs
                .AsNoTracking()
                .Where(l => l.EntityName == entityName)
                .OrderByDescending(l => l.DateChanged)
                .ToListAsync();
        }
    }
}
