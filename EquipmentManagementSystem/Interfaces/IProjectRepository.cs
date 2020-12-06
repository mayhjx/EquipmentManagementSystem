using EquipmentManagementSystem.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EquipmentManagementSystem.Interfaces
{
    public interface IProjectRepository : IGenericRepository<Project>
    {
        public Task<List<string>> GetMobilePhasesByName(string projectName);
        public Task<List<string>> GetColumnTypesByName(string projectName);
        public Task<List<string>> GetIonSourcesByName(string projectName);
        public Task<List<string>> GetDetectorsByName(string projectName);
        public Task<List<string>> GetShortNamesByNames(List<string> projectName);
    }
}
