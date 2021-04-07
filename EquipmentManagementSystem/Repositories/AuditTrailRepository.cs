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
        public async Task<List<AuditTrailLog>> GetAuditTrailLogs(string entityName, int? pk = null, DateTime? date = null)
        {
            var result = _context.AuditTrailLogs.Where(l => l.EntityName == entityName);

            if (date != null)
            {
                // 一条记录可能在1月份创建，但在2月份进行了编辑，如果只查找等于date的记录的话，无法看到2月份的编辑操作
                result = result.Where(l => l.DateChanged.Year >= date.GetValueOrDefault().Year &&
                                            l.DateChanged.Month >= date.GetValueOrDefault().Month);
            }

            if (pk != null)
            {
                result = result.Where(l => l.PrimaryKeyValue == pk.ToString());
            }

            return await result.OrderByDescending(l => l.DateChanged).ToListAsync();
        }

        public IEnumerable<IGrouping<string, AuditTrailLog>> GetAuditTrailLogsGroupingByPK(string entityName, DateTime? date = null)
        {
            var result = _context.AuditTrailLogs.Where(l => l.EntityName == entityName);

            if (date != null)
            {
                // 一条记录可能在1月份创建，但在2月份进行了编辑，如果只查找等于date的记录的话，无法看到2月份的编辑操作
                result = result.Where(l => l.DateChanged.Year >= date.GetValueOrDefault().Year &&
                                            l.DateChanged.Month >= date.GetValueOrDefault().Month);
            }

            return result.AsEnumerable().GroupBy(i => i.PrimaryKeyValue.ToString());
        }

        public IEnumerable<IGrouping<string, AuditTrailLog>> GetAuditTrailLogsGroupingByPKOfInstrumentId(string entityName, string instrumentId, DateTime? date = null)
        {
            var result = _context.AuditTrailLogs.Where(l => l.EntityName == entityName);

            if (date != null)
            {
                // 一条记录可能在1月份创建，但在2月份进行了编辑，如果只查找等于date的记录的话，无法看到2月份的编辑操作
                result = result.Where(l => l.DateChanged.Year >= date.GetValueOrDefault().Year &&
                                            l.DateChanged.Month >= date.GetValueOrDefault().Month);
            }

            return result.AsEnumerable()
                .GroupBy(i => i.PrimaryKeyValue.ToString())
                .Where(l => l.First().OriginalValue != null && l.First().OriginalValue.Contains(instrumentId) || 
                             l.First().CurrentValue != null && l.First().CurrentValue.Contains(instrumentId));
        }
    }
}
