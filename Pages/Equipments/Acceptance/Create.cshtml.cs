using System;
using System.Threading.Tasks;
using EquipmentManagementSystem.Data;
using EquipmentManagementSystem.Models;
using EquipmentManagementSystem.Pages.Instruments;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace EquipmentManagementSystem.Pages.Equipments.Acceptance
{
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
            return RedirectToPage("./Index");
        }

        [BindProperty]
        public InstrumentAcceptance InstrumentAcceptance { get; set; }

        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            InstrumentAcceptance.CreatedTime = DateTime.Now;
            InstrumentAcceptance.Creator = _userManager.GetUserAsync(User).Result.Name;

            _context.InstrumentAcceptances.Add(InstrumentAcceptance);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Edit", new { id = InstrumentAcceptance.Id });
            //return RedirectToPage("./Index");
        }
    }
}
