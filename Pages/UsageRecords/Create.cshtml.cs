using System.Linq;
using System.Threading.Tasks;
using EquipmentManagementSystem.Authorization;
using EquipmentManagementSystem.Data;
using EquipmentManagementSystem.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace EquipmentManagementSystem.Pages.UsageRecords
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
            var isAdmin = User.IsInRole(Constants.ManagerRole) || User.IsInRole(Constants.DirectorRole);
            var userGroup = _userManager.GetUserAsync(User).Result?.Group;

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

        /// <summary>
        /// 根据设备编号返回该设备的检测项目    
        /// </summary>
        /// <param name="instrumentId">设备编号</param>
        /// <returns>JSON</returns>
        public JsonResult OnGetProjectFilter(string instrumentId)
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

            UsageRecord.Creator = _userManager.GetUserAsync(User).Result.Name;

            var isAuthorized = await _authorizationService.AuthorizeAsync(User, UsageRecord, Operations.Create);

            if (!isAuthorized.Succeeded)
            {
                return Forbid();
            }

            _context.UsageRecords.Add(UsageRecord);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
