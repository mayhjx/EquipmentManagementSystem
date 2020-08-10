using System.Linq;
using System.Threading.Tasks;
using EquipmentManagementSystem.Authorization;
using EquipmentManagementSystem.Data;
using EquipmentManagementSystem.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EquipmentManagementSystem.Pages.Records.UsageRecords
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
            PopulateProjectDropDownList(_context);
            return Page();
        }

        public JsonResult OnGetInstrumentFilter(string projectName)
        {
            var result = new JsonResult(from m in _context.Instruments
                                        where m.Projects.IndexOf(projectName) >= 0
                                        select m.ID);
            return result;
        }

        [BindProperty]
        public UsageRecord UsageRecord { get; set; }

        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                PopulateProjectDropDownList(_context);
                return Page();
            }

            UsageRecord.ProjectId = _context.Projects.FirstOrDefaultAsync(p => p.Name == UsageRecord.ProjectName).Result.Id;

            var isAuthorized = await _authorizationService.AuthorizeAsync(User, UsageRecord, Operations.Create);

            if (!isAuthorized.Succeeded)
            {
                return Forbid();
            }

            _context.UsageRecords.Add(UsageRecord);
            await _context.SaveChangesAsync();

            return RedirectToPage("../Index");
        }
    }
}
