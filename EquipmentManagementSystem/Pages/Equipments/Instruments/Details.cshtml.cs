using EquipmentManagementSystem.Authorization;
using EquipmentManagementSystem.Data;
using EquipmentManagementSystem.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace EquipmentManagementSystem.Pages.Instruments
{
    public class DetailsModel : BasePageModel
    {
        public DetailsModel(EquipmentContext context,
            UserManager<User> userManager,
            IAuthorizationService authorizationService)
            : base(context, userManager, authorizationService)
        {
        }

        public Instrument Instrument { get; set; }

        public Calibration Calibration { get; set; }
        public MalfunctionWorkOrder MalfunctionWorkOrder { get; set; }

        public InstrumentAcceptance InstrumentAcceptance { get; set; }

        public async Task<IActionResult> OnGetAsync(string id)
        {
            Instrument = await _context.Instruments
                                .AsNoTracking()
                                //.Include(a => a.Assert)
                                .Include(b => b.Calibrations)
                                .Include(c => c.Components)
                                .Include(d => d.MalfunctionWorkOrder)
                                    .ThenInclude(e => e.MalfunctionInfo)
                                .FirstOrDefaultAsync(m => m.ID == id);

            if (Instrument == null)
            {
                return NotFound();
            }

            var isAuthorized = await _authorizationService.AuthorizeAsync(User, Instrument, Operations.Read);

            if (!isAuthorized.Succeeded)
            {
                return Forbid();
            }

            InstrumentAcceptance = await _context.InstrumentAcceptances
                                .AsNoTracking()
                                .FirstOrDefaultAsync(m => m.InstrumentID == id);

            Calibration = Instrument.Calibrations.FirstOrDefault() ?? new Calibration { Instrument = Instrument };

            MalfunctionWorkOrder = Instrument.MalfunctionWorkOrder.FirstOrDefault() ?? new MalfunctionWorkOrder { Instrument = Instrument };
            
            return Page();
        }
    }
}
