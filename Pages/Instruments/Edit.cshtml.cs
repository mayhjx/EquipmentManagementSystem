using EquipmentManagementSystem.Authorization;
using EquipmentManagementSystem.Data;
using EquipmentManagementSystem.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace EquipmentManagementSystem.Pages.Instruments
{
    public class EditModel : BasePageModel
    {
        public EditModel(EquipmentContext context,
            UserManager<User> userManager,
            IAuthorizationService authorizationService)
            : base(context, userManager, authorizationService)
        {
        }

        public async Task<IActionResult> OnGetAsync(string id)
        {
            Instrument = await _context.Instruments.FirstOrDefaultAsync(m => m.ID == id);

            if (Instrument == null)
            {
                return NotFound();
            }

            var isAuthorized = await _authorizationService.AuthorizeAsync(User, Instrument, Operations.Update);

            if (!isAuthorized.Succeeded)
            {
                return Forbid();
            }

            ViewData["Group"] = new SelectList(_context.Groups, "Name", "Name", Instrument.Group);

            return Page();
        }

        [BindProperty]
        public Instrument Instrument { get; set; }

        public async Task<IActionResult> OnPostAsync(string id)
        {
            Instrument = await _context.Instruments.FirstOrDefaultAsync(m => m.ID == id);

            if (Instrument == null)
            {
                return NotFound();
            }

            var isAuthorized = await _authorizationService.AuthorizeAsync(User, Instrument, Operations.Update);

            if (!isAuthorized.Succeeded)
            {
                return Forbid();
            }

            if (await TryUpdateModelAsync<Instrument>(
                    Instrument,
                    "Instrument",
                    i => i.Status, i => i.Platform, i => i.Name, i => i.StartUsingDate,
                    i => i.CalibrationCycle, i => i.MetrologicalCharacteristics, i => i.Location,
                    i => i.Principal, i => i.Group, i => i.Projects, i => i.NewSystemCode, i => i.Remark))
            {
                await _context.SaveChangesAsync();
                return RedirectToPage("../Instruments/Details", new { id = Instrument.ID });
            }
            return Page();
        }
    }
}
