using EquipmentManagementSystem.Authorization;
using EquipmentManagementSystem.Data;
using EquipmentManagementSystem.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Linq;
using System.Threading.Tasks;

namespace EquipmentManagementSystem.Pages.UsageRecords
{
    [AllowAnonymous]
    public class CreateModel : PageModel
    {
        private readonly EquipmentContext _context;
        private readonly UserManager<User> _userManager;
        public CreateModel(EquipmentContext context, UserManager<User> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public IActionResult OnGet()
        {
            var isAdmin = User.IsInRole(Constants.ManagerRole) || User.IsInRole(Constants.DirectorRole);
            var userGroup = _userManager.GetUserAsync(User).Result.Group;

            if (isAdmin || userGroup == null)
            {
                InstrumentSelectList = new SelectList(_context.Instruments.OrderBy(m => m.ID), "ID", "ID");
            }
            else
            {
                InstrumentSelectList = new SelectList(_context.Instruments.Where(m => m.Group == userGroup).OrderBy(m => m.ID), "ID", "ID");
            }

            return Page();
        }

        public JsonResult OnGetFilter(string instrumentId)
        {
            return new JsonResult(_context.Instruments.Find(instrumentId).Projects.Split(", "));
        }

        [BindProperty]
        public UsageRecord UsageRecord { get; set; }

        public SelectList InstrumentSelectList { get; set; }

        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.UsageRecords.Add(UsageRecord);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
