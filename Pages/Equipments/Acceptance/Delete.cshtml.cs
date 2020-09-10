using System;
using System.Threading.Tasks;
using EquipmentManagementSystem.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace EquipmentManagementSystem.Pages.Equipments.Acceptance
{
    public class DeleteModel : PageModel
    {
        private readonly EquipmentManagementSystem.Data.EquipmentContext _context;

        public DeleteModel(EquipmentManagementSystem.Data.EquipmentContext context)
        {
            _context = context;
        }

        [BindProperty]
        public InstrumentAcceptance InstrumentAcceptance { get; set; }

        public IActionResult OnGet(int? id)
        {
            return RedirectToPage("Index");
            //if (id == null)
            //{
            //    return NotFound();
            //}

            //InstrumentAcceptance = await _context.InstrumentAcceptances
            //    .FirstOrDefaultAsync(m => m.Id == id);

            //if (InstrumentAcceptance == null)
            //{
            //    return NotFound();
            //}
            //return Page();
        }

        public IActionResult OnPost(int? id)
        {
            return RedirectToPage("Index");

            //if (id == null)
            //{
            //    return NotFound();
            //}

            //InstrumentAcceptance = await _context.InstrumentAcceptances.FindAsync(id);

            //if (InstrumentAcceptance != null)
            //{
            //    _context.InstrumentAcceptances.Remove(InstrumentAcceptance);
            //    await _context.SaveChangesAsync();
            //}

            //return RedirectToPage("./Index");
        }

        public async Task<IActionResult> OnPostDeleteAsync(int id)
        {
            var instrumentAcceptanceToDelete = await _context.InstrumentAcceptances
                                        .FirstOrDefaultAsync(m => m.Id == id);

            if (instrumentAcceptanceToDelete == null)
            {
                return new JsonResult("未找到该记录");
            }

            //var isAuthorized = await _authorizationService.AuthorizeAsync(User, InstrumentAcceptance, Operations.Delete);

            //if (!isAuthorized.Succeeded)
            //{
            //    return new JsonResult("权限不足");
            //}

            try
            {
                _context.InstrumentAcceptances.Remove(instrumentAcceptanceToDelete);
                await _context.SaveChangesAsync();
                return new JsonResult("删除成功！");
            }
            catch (Exception ex)
            {
                return new JsonResult($"删除失败，请重试。错误信息：{ex}");
            }

        }

    }
}
