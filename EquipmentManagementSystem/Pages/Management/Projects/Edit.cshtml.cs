using EquipmentManagementSystem.Interfaces;
using EquipmentManagementSystem.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EquipmentManagementSystem.Pages.Management.Projects
{
    public class EditModel : BasePageModel
    {
        private readonly IGroupRepository _groupRepo;
        public EditModel(IProjectRepository projectRepository, IGroupRepository groupRepository) : base(projectRepository)
        {
            _groupRepo = groupRepository;
        }

        [BindProperty]
        public Project Project { get; set; }

        public SelectList GroupSelectList { get; set; }

        [BindProperty]
        public List<string> MobilePhases { get; set; } = new List<string> { "", "", "", "" };

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Project = await _projectRepository.GetById(id);

            if (Project == null)
            {
                return NotFound();
            }

            var mobilePhaseOfProject = Project.GetMobilePhase();
            for (int i = 0; i < mobilePhaseOfProject.Count; i++)
            {
                MobilePhases[i] = mobilePhaseOfProject[i];
            }

            GroupSelectList = new SelectList(await _groupRepo.GetAll(), "Name", "Name");

            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            try
            {
                Project.SetMobilePhase(MobilePhases);
                await _projectRepository.Update(Project);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await ProjectExists(Project.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private async Task<bool> ProjectExists(int id)
        {
            return await _projectRepository.GetById(id) != null;
        }
    }
}
