using EquipmentManagementSystem.Data;
using EquipmentManagementSystem.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace EquipmentManagementSystem.Pages.Malfunctions.WorkOrders
{
    public class CreateModel : PageModel
    {
        private readonly MalfunctionContext _context;
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;

        public CreateModel(MalfunctionContext context,
            SignInManager<User> signInManager,
            UserManager<User> userManager)
        {
            _context = context; 
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public IActionResult OnGet(string id)
        {
            ViewData["InstrumentID"] = new SelectList(_context.Set<Instrument>().OrderBy(m => m.ID), "ID", "ID", id);
            return Page();
        }

        [BindProperty]
        public MalfunctionWorkOrder MalfunctionWorkOrder { get; set; }

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

            MalfunctionWorkOrder.CreatedTime = DateTime.Now;
            if (_signInManager.IsSignedIn(User))
            {
                var user = await _userManager.GetUserAsync(User);
                MalfunctionWorkOrder.Creator = user.Name;
            }
            
            _context.MalfunctionWorkOrder.Add(MalfunctionWorkOrder);

            await _context.SaveChangesAsync();

            return RedirectToPage("./Details", new { MalfunctionWorkOrder.ID });
        }
    }
}
