using EquipmentManagementSystem.Data;
using EquipmentManagementSystem.Interfaces;
using EquipmentManagementSystem.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System;

namespace EquipmentManagementSystem.Repositories
{
    public class InstrumentRepository : GenericRepository<Instrument>, IInstrumentRepository
    {
        public InstrumentRepository(EquipmentContext context) : base(context)
        {
        }

        public List<string> GetAllInstrumentId()
        {
            return _context.Set<Instrument>().Select(i => i.ID).ToList();
        }

        public List<string> GetAllInstrumentIdByGroup(string group)
        {
            return _context.Set<Instrument>().Where(i => i.Group == group).Select(i => i.ID).ToList();
        }

        public List<string> GetAllInstrumentIdByProject(string project)
        {
            return _context.Set<Instrument>().AsEnumerable().Where(i => i.HasProject(project)).Select(i => i.ID).ToList();
        }

        public async Task<string> GetModelById(string instrumentId)
        {
            var result = (await _context.Set<Instrument>()
                        .AsNoTracking()
                        .Include(m => m.Components)
                        .FirstOrDefaultAsync(m => m.ID == instrumentId))
                        .Components
                        .FirstOrDefault(c => c.Name.Contains("主机"))?.Model;

            return result ?? "";
        }

        public List<string> GetTestProjectsById(string instrumentId)
        {
            return _context.Set<Instrument>().Find(instrumentId)?.GetProjects() ?? new List<string>();
        }

        public async Task<DateTime?> GetLatestCalibratedDateOfInstrument(string instrumentId)
        {
            return (await _context.Set<Calibration>()
                .AsNoTracking()
                .OrderBy(i => i.Date)
                .Where(i => i.InstrumentID == instrumentId)
                .LastOrDefaultAsync())?
                .Date;
        }
    }
}
