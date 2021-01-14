using EquipmentManagementSystem.Data;
using EquipmentManagementSystem.Interfaces;
using EquipmentManagementSystem.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EquipmentManagementSystem.Repositories
{
    public class ProjectRepository : GenericRepository<Project>, IProjectRepository
    {
        public ProjectRepository(EquipmentContext context) : base(context)
        {
        }

        public async Task<string> GetColumnTypesByShortName(string projectShortName)
        {
            return (await _context.Projects.FirstOrDefaultAsync(p => p.ShortName == projectShortName))?.ColumnType ?? string.Empty;
        }

        public async Task<string> GetDetectorsByShortName(string projectShortName)
        {
            return (await _context.Projects.FirstOrDefaultAsync(p => p.ShortName == projectShortName))?.Detector ?? string.Empty;
        }

        public async Task<string> GetIonSourcesByShortName(string projectShortName)
        {
            return (await _context.Projects.FirstOrDefaultAsync(p => p.ShortName == projectShortName))?.IonSource ?? string.Empty;
        }

        public async Task<string> GetMobilePhasesByShortName(string projectShortName)
        {
            // 如果流动相为空的话返回载气
            var mobilePhase = (await _context.Projects.FirstOrDefaultAsync(p => p.ShortName == projectShortName))?.MobilePhase;
            var carrierGas = (await _context.Projects.FirstOrDefaultAsync(p => p.ShortName == projectShortName))?.CarrierGas;

            return string.IsNullOrEmpty(mobilePhase) ? 
                string.IsNullOrEmpty(carrierGas) ? "": carrierGas 
                : mobilePhase;
        }

        public async Task<string> GetGroupNameByShortName(string projectShortName)
        {
            return (await _context.Projects.FirstOrDefaultAsync(p => p.ShortName == projectShortName))?.GroupName ?? string.Empty;
        }

        public async Task<List<string>> GetShortNamesByNames(List<string> projectName)
        {
            var result = new List<string>();
            foreach (var name in projectName)
            {
                var project = await _context.Projects.FirstOrDefaultAsync(i => i.Name == name);
                if (project != null)
                {
                    result.Add(project.ShortName);
                }
            }
            return result;
        }

        /// <summary>
        /// 返回某项目组的所有检测项目
        /// </summary>
        /// <param name="group"></param>
        /// <returns></returns>
        public List<string> GetByGroup(string group)
        {
            return _context.Set<Project>().Where(i => i.GroupName == group).Select(i => i.Name).ToList();
        }
    }
}
