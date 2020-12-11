using EquipmentManagementSystem.Interfaces;
using EquipmentManagementSystem.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EquipmentManagementSystem.Pages.MaintenanceRecords
{
    public class CreateModel : PageModel
    {
        private readonly IInstrumentRepository _instrumentRepository;
        private readonly IUserResolverService _userResolverService;
        private readonly IMaintenanceContentRepository _maintenanceContentRepository;

        public CreateModel(IInstrumentRepository instrumentRepository,
            IUserResolverService userResolverService,
            IMaintenanceContentRepository maintenanceContentRepository)
        {
            _instrumentRepository = instrumentRepository;
            _userResolverService = userResolverService;
            _maintenanceContentRepository = maintenanceContentRepository;
        }

        [BindProperty]
        public MaintenanceRecord MaintenanceRecord { get; set; }

        public string CurrentUserName { get; set; }

        public List<string> InstrumentSelectList { get; set; }

        [BindProperty]
        public List<string> SelectedInstruments { get; set; }

        public IActionResult OnGet()
        {
            InstrumentSelectList = _instrumentRepository.GetAllInstrumentId();
            CurrentUserName = _userResolverService.GetUserName();
            return Page();
        }

        ///// <summary>
        ///// 返回包含某项目的设备编号
        ///// </summary>
        ///// <param name="projectName"></param>
        ///// <returns></returns>
        //public JsonResult OnGetInstrumentFilter(string projectName)
        //{
        //    var result = new JsonResult(from m in _context.Instruments
        //                                where m.Projects.IndexOf(projectName) >= 0
        //                                select m.ID);
        //    return result;
        //}

        /// <summary>
        /// 返回对应设备平台的维护内容
        /// </summary>
        public async Task<JsonResult> OnGetMaintenanceContents(string instrument)
        {
            var platform = (await _instrumentRepository.GetById(instrument)).Platform;
            if (platform == null)
            {
                return new JsonResult("该仪器未设置平台");
            }

            var contents = _maintenanceContentRepository.GetByInstrumentPlatform(platform);

            if (contents.Count == 0)
            {
                return new JsonResult("该设备平台未设置维护内容");
            }

            var result = new JsonResult(contents);

            return result;
        }


        // TODO 
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://aka.ms/RazorPagesCRUD.
        //public async Task<IActionResult> OnPostAsync(string maintenanceType, string[] maintenanceContent, string otherMaintenanceContent)
        //{
        //if (!ModelState.IsValid)
        //{
        //    return Page();
        //}

        //if (maintenanceType == null)
        //{
        //    ModelState.AddModelError("", "请选择一个维护类型");
        //    return Page();
        //}

        //MaintenanceRecord.ProjectId = _context.Projects.FirstOrDefaultAsync(p => p.Name == MaintenanceRecord.ProjectName).Result.Id;
        //MaintenanceRecord.Type = maintenanceType;

        //if (maintenanceContent.Length > 0)
        //{
        //    MaintenanceRecord.Content = string.Join(", ", maintenanceContent);
        //}

        //if (maintenanceType == "临时维护" && otherMaintenanceContent != null)
        //{
        //    MaintenanceRecord.Content += (maintenanceContent.Length > 0 ? ", " : "") + $"其他：{otherMaintenanceContent}";
        //}

        //_context.MaintenanceRecords.Add(MaintenanceRecord);
        //await _context.SaveChangesAsync();

        //return RedirectToPage("../Index");
        //}
    }
}
