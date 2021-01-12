using EquipmentManagementSystem.Authorization;
using EquipmentManagementSystem.Data;
using EquipmentManagementSystem.Models;
using EquipmentManagementSystem.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EquipmentManagementSystem.Pages.Malfunctions.WorkOrders
{
    public class CreateModel : BasePageModel
    {
        private readonly IInstrumentService _instrumentService;
        private readonly IInstrumentRepository _instrumentRepository;
        public CreateModel(EquipmentContext context,
            IAuthorizationService authorizationService,
            UserManager<User> userManager,
            IInstrumentService instrumentService,
            IInstrumentRepository instrumentRepository)
            : base(context, authorizationService, userManager)
        {
            _instrumentService = instrumentService;
            _instrumentRepository = instrumentRepository;
        }

        public IActionResult OnGet()
        {

            var isAdmin = User.IsInRole(Constants.ManagerRole) ||
                                User.IsInRole(Constants.DirectorRole);

            if (isAdmin)
            {
                // 获取所有设备编号
                InstrumentSelectList = _instrumentRepository.GetAllInstrumentId();
            }
            else
            {
                // 获取技术员或设备负责人所属项目组的设备编号
                var userGroup = _userManager.GetUserAsync(User).Result.Group;
                InstrumentSelectList = _instrumentService.GetInstrumentIdRelateToProjectsOfGroup(userGroup);
            }

            MalfunctionPhenomenonSelectList = new SelectList(_context.MalfunctionPhenomenon, "Phenomenon", "Phenomenon");

            return Page();
        }

        [BindProperty]
        public MalfunctionWorkOrder MalfunctionWorkOrder { get; set; }

        public List<string> InstrumentSelectList { get; set; }

        public SelectList MalfunctionPhenomenonSelectList { get; set; }


        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            // 改变故障设备的状态
            MalfunctionWorkOrder.Instrument = await _context.Set<Instrument>().FindAsync(MalfunctionWorkOrder.InstrumentID);
            MalfunctionWorkOrder.Instrument.Status = "故障";

            // 新建工单关联的内容
            MalfunctionWorkOrder.Investigation = new Investigation { };
            MalfunctionWorkOrder.RepairRequest = new RepairRequest { };
            MalfunctionWorkOrder.AccessoriesOrder = new AccessoriesOrder { };
            MalfunctionWorkOrder.Repair = new Repair { };
            MalfunctionWorkOrder.Validation = new Validation { };

            var isAuthorized = await _authorizationService.AuthorizeAsync(User, MalfunctionWorkOrder, Operations.Create);

            if (!isAuthorized.Succeeded)
            {
                return Forbid();
            }

            _context.MalfunctionWorkOrder.Add(MalfunctionWorkOrder);

            await _context.SaveChangesAsync();

            return RedirectToPage("./Details", new { MalfunctionWorkOrder.ID });
        }
    }
}
