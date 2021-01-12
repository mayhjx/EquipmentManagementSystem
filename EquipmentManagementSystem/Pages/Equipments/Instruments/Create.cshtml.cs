using EquipmentManagementSystem.Authorization;
using EquipmentManagementSystem.Data;
using EquipmentManagementSystem.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Threading.Tasks;
using System.Linq;
using System.Collections.Generic;

namespace EquipmentManagementSystem.Pages.Instruments
{
    public class CreateModel : BasePageModel
    {
        public CreateModel(EquipmentContext context,
            UserManager<User> userManager,
            IAuthorizationService authorizationService)
            : base(context, userManager, authorizationService)
        {
        }

        public async Task<IActionResult> OnGet()
        {
            var isAuthorized = await _authorizationService.AuthorizeAsync(User, new Instrument(), Operations.Create);

            if (!isAuthorized.Succeeded)
            {
                return Forbid();
            }

            GroupsSelectList = new SelectList(_context.Groups, "Name", "Name");
            ProjectsSelectList = new SelectList(_context.Projects, "Name", "Name");

            return Page();
        }

        [BindProperty]
        public Instrument Instrument { get; set; }

        [BindProperty]
        public List<string> SelectedProject { get; set; }

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

            var isAuthorized = await _authorizationService.AuthorizeAsync(User, Instrument, Operations.Create);

            if (!isAuthorized.Succeeded)
            {
                return Forbid();
            }

            Instrument.SetProjects(SelectedProject);

            _context.Instruments.Add(Instrument);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
