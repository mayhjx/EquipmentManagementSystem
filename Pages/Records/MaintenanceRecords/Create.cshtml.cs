using System.Linq;
using System.Threading.Tasks;
using EquipmentManagementSystem.Data;
using EquipmentManagementSystem.Models;
using EquipmentManagementSystem.Pages.Records;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EquipmentManagementSystem.Pages.MaintenanceRecords
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

        /// <summary>
        /// 返回包含某项目的设备编号
        /// </summary>
        /// <param name="projectName"></param>
        /// <returns></returns>
        public JsonResult OnGetInstrumentFilter(string projectName)
        {
            var result = new JsonResult(from m in _context.Instruments
                                        where m.Projects.IndexOf(projectName) >= 0
                                        select m.ID);
            return result;
        }

        /// <summary>
        /// 返回对应设备平台和对应维护类型的维护内容
        /// </summary>
        public JsonResult OnGetMaintenanceContents(string instrument, string maintenanceType)
        {
            var platform = _context.Instruments.FirstOrDefault(m => m.ID == instrument).Platform;
            if (platform == null)
            {
                return new JsonResult("");
            }

            var contents = _context.MaintenanceContents.Where(m => m.InstrumentPlatform == platform)
                                                       .Where(m => m.Type == maintenanceType);
            if (contents.Any() == false)
            {
                return new JsonResult("");
            }

            var result = new JsonResult(from c in contents
                                        select c.Text);

            return result;
        }

        [BindProperty]
        public MaintenanceRecord MaintenanceRecord { get; set; }

        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync(string maintenanceType, string[] maintenanceContent)
        {
            if (!ModelState.IsValid)
            {
                PopulateProjectDropDownList(_context);
                return Page();
            }

            MaintenanceRecord.ProjectId = _context.Projects.FirstOrDefaultAsync(p => p.Name == MaintenanceRecord.ProjectName).Result.Id;
            MaintenanceRecord.Type = maintenanceType;
            if (maintenanceType != "临时维护")
            {
                MaintenanceRecord.Content = string.Join(", ", maintenanceContent);
            }

            _context.MaintenanceRecords.Add(MaintenanceRecord);
            await _context.SaveChangesAsync();

            return RedirectToPage("../Index");
        }
    }
}
