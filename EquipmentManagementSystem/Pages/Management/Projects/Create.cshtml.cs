using EquipmentManagementSystem.Interfaces;
using EquipmentManagementSystem.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EquipmentManagementSystem.Pages.Management.Projects
{
    public class CreateModel : BasePageModel
    {
        private readonly IGroupRepository _groupRepo;
        public CreateModel(IProjectRepository projectRepository, IGroupRepository groupRepository) : base(projectRepository)
        {
            _groupRepo = groupRepository;
        }

        public async Task<IActionResult> OnGet()
        {
            GroupSelectList = new SelectList(await _groupRepo.GetAll(), "Name", "Name");
            return Page();
        }

        [BindProperty]
        public Project Project { get; set; }

        public SelectList GroupSelectList { get; set; }

        [BindProperty]
        public List<string> MobilePhases { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                GroupSelectList = new SelectList(await _groupRepo.GetAll(), "Name", "Name");
                return Page();
            }

            Project.SetMobilePhase(MobilePhases);

            await _projectRepository.Create(Project);

            return RedirectToPage("./Index");
        }
    }
}
