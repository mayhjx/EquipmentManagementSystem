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

        /// <summary>
        /// 返回某项目组的所有检测项目
        /// </summary>
        /// <param name="group"></param>
        /// <returns></returns>
        List<string> GetByGroup(string group);
    }
}
