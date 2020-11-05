using EquipmentManagementSystem.Data;
using EquipmentManagementSystem.Models;
using EquipmentManagementSystem.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EquipmentManagementSystem.Services
{
    public class AuditTrailService : IAuditTrailService
    {
        private readonly EquipmentContext _context;

        public AuditTrailService(EquipmentContext context)
        {
            _context = context;
        }

        /// <summary>
        /// 获取某模块的操作日志
        /// </summary>
        /// <param name="entityName">模块名</param>
        /// <returns>List<AuditTrailLog></returns>
        public async Task<List<AuditTrailLog>> GetAuditTrailLogsAsync(string entityName)
        {
            return await _context.AuditTrailLogs
                .AsNoTracking()
                .Where(l => l.EntityName == entityName)
                .OrderByDescending(l => l.DateChanged)
                .ToListAsync();
        }
    }
}
