using EquipmentManagementSystem.Data;
using EquipmentManagementSystem.Interfaces;
using EquipmentManagementSystem.Models;
using System.Collections.Generic;
using System.Linq;

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

        public List<string> GetTestProjectsById(string instrumentId)
        {
            return _context.Set<Instrument>().Find(instrumentId)?.GetProjects() ?? new List<string>();
        }
    }
}
