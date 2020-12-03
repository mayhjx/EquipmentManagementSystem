using System.Threading.Tasks;
using EquipmentManagementSystem.Authorization;
using EquipmentManagementSystem.Data;
using EquipmentManagementSystem.Models;
using EquipmentManagementSystem.Pages.Instruments;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace EquipmentManagementSystem.Pages.Calibrations
{
    public class CreateModel : BasePageModel
    {
        public CreateModel(EquipmentContext context,
            UserManager<User> userManager,
            IAuthorizationService authorizationService)
            : base(context, userManager, authorizationService)
        {
        }

        public IActionResult OnGet(string id)
        {
            instrumentId = id;

            var isAuthorized = User.IsInRole(Constants.DirectorRole) || User.IsInRole(Constants.ManagerRole) ||
                            _userManager.GetUserAsync(User).Result.Group == _context.Instruments.FindAsync(id).Result.Group;

            if (!isAuthorized)
            {
                return Forbid();
            }

            return Page();
        }

        public string instrumentId { get; set; }

        [BindProperty]
        public Calibration Calibration { get; set; }

        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }


            var isAuthorized = User.IsInRole(Constants.DirectorRole) || User.IsInRole(Constants.ManagerRole) ||
                            _userManager.GetUserAsync(User).Result.Group == _context.Instruments.FindAsync(Calibration.InstrumentID).Result.Group;

            if (!isAuthorized)
            {
                return Forbid();
            }

            _context.Calibrations.Add(Calibration);
            await _context.SaveChangesAsync();

            return RedirectToPage("../Instruments/Details", new { id = Calibration.InstrumentID });
        }
    }
}
