using EquipmentManagementSystem.Models;
using System.Collections.Generic;

namespace EquipmentManagementSystem.Interfaces
{
    public interface IInstrumentRepository : IGenericRepository<Instrument>
    {
        public List<string> GetTestProjectsById(string instrumentId);
        public List<string> GetAllInstrumentId();
        public List<string> GetAllInstrumentIdByGroup(string group);
        public List<string> GetAllInstrumentIdsByProject(string project);
    }
}
