using EquipmentManagementSystem.Data;
using EquipmentManagementSystem.Interfaces;
using EquipmentManagementSystem.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EquipmentManagementSystem.Repositories
{
    public class ProjectRepository : GenericRepository<Project>, IProjectRepository
    {
        public ProjectRepository(EquipmentContext context) : base(context)
        {
        }

        public async Task<string> GetColumnTypesByName(string projectName)
        {
            return (await _context.Projects.FirstOrDefaultAsync(p => p.Name == projectName))?.ColumnType ?? string.Empty;
        }

        public async Task<string> GetDetectorsByName(string projectName)
        {
            return (await _context.Projects.FirstOrDefaultAsync(p => p.Name == projectName))?.Detector ?? string.Empty;
        }

        public async Task<string> GetIonSourcesByName(string projectName)
        {
            return (await _context.Projects.FirstOrDefaultAsync(p => p.Name == projectName))?.IonSource ?? string.Empty;
        }

        public async Task<List<string>> GetMobilePhasesByName(string projectName)
        {
            return (await _context.Projects.FirstOrDefaultAsync(p => p.Name == projectName))?.GetMobilePhase() ?? new List<string>();
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
    }
}
