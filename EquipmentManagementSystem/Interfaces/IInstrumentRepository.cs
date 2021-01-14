using EquipmentManagementSystem.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;

namespace EquipmentManagementSystem.Interfaces
{
    public interface IInstrumentRepository : IGenericRepository<Instrument>
    {
        public List<string> GetTestProjectsById(string instrumentId);
        public List<string> GetAllInstrumentId();
        public List<string> GetAllInstrumentIdByGroup(string group);
        public List<string> GetAllInstrumentIdByProject(string project);
        public Task<string> GetModelById(string instrumentId);
        public Task<DateTime?> GetLatestCalibratedDateOfInstrument(string instrumentId);
    }
}
