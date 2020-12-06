using EquipmentManagementSystem.Data;
using EquipmentManagementSystem.Models;
using EquipmentManagementSystem.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace EquipmentManagementSystem.Pages.Management.Projects
{
    public class EditModel : BasePageModel
    {
        private readonly IGroupRepository _groupRepo;
        public EditModel(IProjectRepository projectRepository, IGroupRepository groupRepository):base(projectRepository)
        {
            _groupRepo = groupRepository;
        }

        [BindProperty]
        public Project Project { get; set; }

        public SelectList GroupSelectList { get; set; }

        [BindProperty]
        public List<string> ColumnTypes { get; set; }

        [BindProperty]
        public List<string> MobilePhases { get; set; }

        [BindProperty]
        public List<string> IonSources { get; set; }

        [BindProperty]
        public List<string> Detectors { get; set; }

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

            ColumnTypes = Project.GetColumnType();
            MobilePhases = Project.GetMobilePhase();
            IonSources = Project.GetIonSource();
            Detectors = Project.GetDetector();
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
                Project.SetColumnType(ColumnTypes);
                Project.SetMobilePhase(MobilePhases);
                Project.SetIonSource(IonSources);
                Project.SetDetector(Detectors);
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
