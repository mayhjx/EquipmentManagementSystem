using EquipmentManagementSystem.Data;
using EquipmentManagementSystem.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Threading.Tasks;

namespace EquipmentManagementSystem.Pages.Instruments
{
    [Authorize(Roles = "设备主任, 设备管理员")]
    public class CreateModel : BasePageModel
    {
        public CreateModel(EquipmentContext context,
            UserManager<User> userManager,
            IAuthorizationService authorizationService)
            : base(context, userManager, authorizationService)
        {
        }

        public IActionResult OnGet()
        {
            GroupsSelectList = new SelectList(_context.Groups, "Name", "Name");
            ProjectsSelectList = new SelectList(_context.Projects, "Name", "Name");

            return Page();
        }

        [BindProperty]
        public Instrument Instrument { get; set; }

        [BindProperty]
        public string[] SelectedProject { get; set; }

        public SelectList ProjectsSelectList { get; set; }
        public MultiSelectList GroupsSelectList { get; set; }

        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            Instrument.Projects = string.Join(", ", SelectedProject);

            _context.Instruments.Add(Instrument);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
