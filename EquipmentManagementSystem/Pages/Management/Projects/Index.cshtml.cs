using EquipmentManagementSystem.Interfaces;
using EquipmentManagementSystem.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

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
