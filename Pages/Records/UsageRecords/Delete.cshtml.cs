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
    public class DeleteModel : BasePageModel
    {
        public DeleteModel(EquipmentContext context,
            UserManager<User> userManager,
            IAuthorizationService authorizationService)
            : base(context, userManager, authorizationService)
        {
        }

        [BindProperty]
        public UsageRecord UsageRecord { get; set; }
        public string ErrorMessage { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id, bool? saveChangesError = false)
        {
            UsageRecord = await _context.UsageRecords
                                .AsNoTracking()
                                .Include(m => m.Project)
                                    .ThenInclude(p => p.Group)
                                .FirstOrDefaultAsync(m => m.Id == id);

            if (UsageRecord == null)
            {
                return NotFound();
            }

            var isAuthorized = await _authorizationService.AuthorizeAsync(User, UsageRecord, Operations.Delete);

            if (!isAuthorized.Succeeded)
            {
                return Forbid();
            }

            if (saveChangesError.GetValueOrDefault())
            {
                ErrorMessage = "删除失败，请重试！";
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int id)
        {
            UsageRecord = await _context.UsageRecords
                                .AsNoTracking()
                                .Include(m => m.Project)
                                    .ThenInclude(p => p.Group)
                                .FirstOrDefaultAsync(m => m.Id == id);

            if (UsageRecord == null)
            {
                return NotFound();
            }

            var isAuthorized = await _authorizationService.AuthorizeAsync(User, UsageRecord, Operations.Delete);

            if (!isAuthorized.Succeeded)
            {
                return Forbid();
            }

            try
            {
                _context.UsageRecords.Remove(UsageRecord);
                await _context.SaveChangesAsync();
                return RedirectToPage("../Index");
            }
            catch (DbUpdateException)
            {
                return RedirectToAction("./Delete", new { id, saveChangesError = true });
            }
        }
    }
}
