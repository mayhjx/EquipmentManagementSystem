using System.Linq;
using System.Threading.Tasks;
using EquipmentManagementSystem.Authorization;
using EquipmentManagementSystem.Data;
using EquipmentManagementSystem.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace EquipmentManagementSystem.Pages.Malfunctions.WorkOrders
{
    public class CreateModel : BasePageModel
    {
        public CreateModel(MalfunctionContext context,
            IAuthorizationService authorizationService,
            UserManager<User> userManager)
            : base(context, authorizationService, userManager)
        {
        }

        public IActionResult OnGet(string id)
        {

            var isAdmin = User.IsInRole(Constants.ManagerRole) ||
                                User.IsInRole(Constants.DirectorRole);

            if (isAdmin)
            {
                // 获取所有设备编号
                InstrumentSelectList = new SelectList(_context.Set<Instrument>().OrderBy(m => m.ID), "ID", "ID", id);
            }
            else
            {
                // 获取技术员或设备负责人所属项目组的设备编号
                var userGroup = _userManager.GetUserAsync(User).Result.Group;
                InstrumentSelectList = new SelectList(_context.Set<Instrument>()
                                                                .Where(m => m.Group == userGroup)
                                                                .OrderBy(m => m.ID), "ID", "ID", id);
            }

            MalfunctionPhenomenonSelectList = new SelectList(_context.MalfunctionPhenomenon, "Phenomenon", "Phenomenon");

            return Page();
        }

        [BindProperty]
        public MalfunctionWorkOrder MalfunctionWorkOrder { get; set; }

        public SelectList InstrumentSelectList { get; set; }

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
            MalfunctionWorkOrder.Instrument.Status = InstrumentStatus.Malfunction;

            // 新建工单关联的内容
            MalfunctionWorkOrder.Investigation = new Investigation { };
            MalfunctionWorkOrder.RepairRequest = new RepairRequest { };
            MalfunctionWorkOrder.AccessoriesOrder = new AccessoriesOrder { };
            MalfunctionWorkOrder.Maintenance = new Maintenance { };
            MalfunctionWorkOrder.Validation = new Validation { };

            MalfunctionWorkOrder.Creator = _userManager.GetUserAsync(User).Result.Name;

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
