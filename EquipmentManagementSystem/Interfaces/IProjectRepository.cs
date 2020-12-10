using EquipmentManagementSystem.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EquipmentManagementSystem.Interfaces
{
    public interface IProjectRepository : IGenericRepository<Project>
    {
        public Task<string> GetMobilePhasesByShortName(string projectName);
        public Task<string> GetColumnTypesByShortName(string projectName);
        public Task<string> GetIonSourcesByShortName(string projectName);
        public Task<string> GetDetectorsByShortName(string projectName);
        public Task<string> GetGroupNameByShortName(string projectName);
        public Task<List<string>> GetShortNamesByNames(List<string> projectName);
    }
}
