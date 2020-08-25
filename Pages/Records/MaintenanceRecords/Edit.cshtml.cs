using System.Linq;
using System.Threading.Tasks;
using EquipmentManagementSystem.Data;
using EquipmentManagementSystem.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace EquipmentManagementSystem.Pages.MaintenanceRecords
{
    public class EditModel : PageModel
    {
        private readonly EquipmentContext _context;

        public EditModel(EquipmentContext context)
        {
            _context = context;
        }

        [BindProperty]
        public MaintenanceRecord MaintenanceRecord { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            MaintenanceRecord = await _context.MaintenanceRecords
                .Include(m => m.Instrument)
                .Include(m => m.Project).FirstOrDefaultAsync(m => m.Id == id);

            if (MaintenanceRecord == null)
            {
                return NotFound();
            }

            return Page();
        }

        /// <summary>
        /// 返回对应设备平台的维护内容
        /// </summary>
        public JsonResult OnGetMaintenanceContents(string instrument)
        {
            var platform = _context.Instruments.FirstOrDefault(m => m.ID == instrument).Platform;
            if (platform == null)
            {
                return new JsonResult("");
            }

            var contents = _context.MaintenanceContents.Where(m => m.InstrumentPlatform == platform);

            if (contents.Any() == false)
            {
                return new JsonResult("");
            }

            var result = new JsonResult(contents);

            return result;
        }

        public async Task<IActionResult> OnPostAsync(int id, string maintenanceType, string[] maintenanceContent, string otherMaintenanceContent)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            if (maintenanceType == null)
            {
                ModelState.AddModelError("", "请选择一个维护类型");
                return Page();
            }

            var maintenanceRecordToUpdate = await _context.MaintenanceRecords
                                                    .FirstOrDefaultAsync(m => m.Id == id);

            if (maintenanceRecordToUpdate == null)
            {
                return NotFound();
            }

            if (await TryUpdateModelAsync<MaintenanceRecord>(
                maintenanceRecordToUpdate,
                "MaintenanceRecord",
                i => i.BeginTime, i => i.EndTime, i => i.Operator, i => i.Type, i => i.Content))
            {
                maintenanceRecordToUpdate.Type = maintenanceType;
                maintenanceRecordToUpdate.Content = string.Empty;

                if (maintenanceContent.Length > 0)
                {
                    maintenanceRecordToUpdate.Content = string.Join(", ", maintenanceContent);
                }

                if (maintenanceType == "临时维护" && otherMaintenanceContent != null)
                {
                    maintenanceRecordToUpdate.Content += (maintenanceContent.Length > 0 ? ", " : "") + $"其他：{otherMaintenanceContent}";
                }

                await _context.SaveChangesAsync();
                return RedirectToPage("../Index");
            }

            return Page();
        }
    }
}
