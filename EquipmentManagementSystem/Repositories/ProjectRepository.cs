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

        public async Task<List<string>> GetColumnTypesByName(string projectName)
        {
            return (await _context.Projects.FirstOrDefaultAsync(p => p.Name == projectName)).GetColumnType();
        }

        public async Task<List<string>> GetMobilePhasesByName(string projectName)
        {
            return (await _context.Projects.FirstOrDefaultAsync(p => p.Name == projectName)).GetMobilePhase();
        }
    }
}
