using EquipmentManagementSystem.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EquipmentManagementSystem.Interfaces
{
    interface IProjectRepository : IGenericRepository<Project>
    {
        public Task<List<string>> GetMobilePhasesByName(string projectName);
        public Task<List<string>> GetColumnTypesByName(string projectName);
    }
}
