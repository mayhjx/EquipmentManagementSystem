using EquipmentManagementSystem.Data;
using EquipmentManagementSystem.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Linq;
using System.Threading.Tasks;

namespace EquipmentManagementSystem.Pages.Malfunctions
{
    public class CreateModel : PageModel
    {
        private readonly EquipmentContext _context;

        public CreateModel(EquipmentContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Malfunction Malfunction { get; set; }

        public Instrument Instrument { get; set; }

        [BindProperty]
        public string instrumentID { get; set; }
        public SelectList InstrumentOptions { get; set; }
        public SelectList ComponentOptions { get; set; }

        //public Instrument instrument { get; set; }

        public IActionResult OnGet(string? instrumentID)
        {
            if (instrumentID == null)
            {
                InstrumentOptions = new SelectList(_context.Instruments, "ID", "ID");
                ComponentOptions = new SelectList(_context.Components, "ID", "Name");
            }
            else
            {
                //Instrument = _context.Instruments.Find(instrumentID);
                this.instrumentID = instrumentID;
                var components = from m in _context.Components
                             .Where(m => m.InstrumentID == instrumentID)
                                 select m;
                ComponentOptions = new SelectList(components, "ID", "Name");
            }

            return Page();
        }



        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            //if (!ModelState.IsValid)
            //{
            //    return Page();
            //}

            _context.Malfunctions.Add(Malfunction);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
