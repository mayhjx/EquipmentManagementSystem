using EquipmentManagementSystem.Interfaces;
using EquipmentManagementSystem.Models;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace EquipmentManagementSystem.Pages.Management.Projects
{
    public class DeleteModel : BasePageModel
    {
        public DeleteModel(IProjectRepository projectRepository):base(projectRepository)
        {
        }

        [BindProperty]
        public Project Project { get; set; }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            Project = await _projectRepository.GetById(id); 

            if (Project == null)
            {
                return NotFound();
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int id)
        {
            Project = await _projectRepository.GetById(id); 

            if (Project != null)
            {
                await _projectRepository.Delete(Project);
            }

            return RedirectToPage("./Index");
        }
    }
}
