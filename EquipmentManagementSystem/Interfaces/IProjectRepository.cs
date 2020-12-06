using EquipmentManagementSystem.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EquipmentManagementSystem.Interfaces
{
    public interface IProjectRepository : IGenericRepository<Project>
    {
        public Task<List<string>> GetMobilePhasesByName(string projectName);
        public Task<List<string>> GetColumnTypesByName(string projectName);
        public Task<List<string>> GetIonSourceTypesByName(string projectName);
        public Task<List<string>> GetDetectorTypesByName(string projectName);
    }
}
