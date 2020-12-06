using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EquipmentManagementSystem.Data;
using EquipmentManagementSystem.Interfaces;
using EquipmentManagementSystem.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace EquipmentManagementSystem.Pages.Management.Projects
{
    public class IndexModel : BasePageModel
    {
        public IndexModel(IProjectRepository projectRepository) : base(projectRepository)
        {
        }

        public IList<Project> Project { get; set; }

        public async Task OnGetAsync()
        {
            Project = await _projectRepository.GetAll();
        }
    }
}
