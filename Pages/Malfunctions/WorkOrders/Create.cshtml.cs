using EquipmentManagementSystem.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Linq;
using System.Threading.Tasks;

namespace EquipmentManagementSystem.Pages.Malfunctions.WorkOrders
{
    public class CreateModel : PageModel
    {
        private readonly EquipmentManagementSystem.Data.MalfunctionContext _context;

        public CreateModel(EquipmentManagementSystem.Data.MalfunctionContext context)
        {
            _context = context;
        }

        //public IActionResult OnGet()
        //{
        //    ViewData["InstrumentID"] = new SelectList(_context.Set<Instrument>().OrderBy(m => m.ID), "ID", "ID");
        //    return Page();
        //}

        public IActionResult OnGet(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                ViewData["InstrumentID"] = new SelectList(_context.Set<Instrument>().OrderBy(m => m.ID), "ID", "ID");
            }
            else
            {
                ViewData["InstrumentID"] = id;
            }
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

            // 新建工单内容
            MalfunctionWorkOrder.Investigation = new Investigation { };
            MalfunctionWorkOrder.RepairRequest = new RepairRequest { };
            MalfunctionWorkOrder.AccessoriesOrder = new AccessoriesOrder { };
            MalfunctionWorkOrder.Maintenance = new Maintenance { };
            MalfunctionWorkOrder.Validation = new Validation { };

            _context.MalfunctionWorkOrder.Add(MalfunctionWorkOrder);

            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
