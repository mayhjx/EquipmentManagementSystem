﻿using EquipmentManagementSystem.Data;
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
            return (await _context.Projects.FirstOrDefaultAsync(p => p.ShortName == projectShortName))?.MobilePhase ?? string.Empty;
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
    }
}
