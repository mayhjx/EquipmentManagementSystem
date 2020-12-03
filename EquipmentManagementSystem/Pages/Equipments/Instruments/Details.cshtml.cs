﻿using System.Threading.Tasks;
using EquipmentManagementSystem.Data;
using EquipmentManagementSystem.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace EquipmentManagementSystem.Pages.Instruments
{
    public class DetailsModel : PageModel
    {
        private readonly EquipmentContext _context;

        public DetailsModel(EquipmentContext context)
        {
            _context = context;
        }

        public Instrument Instrument { get; set; }

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

            InstrumentAcceptance = await _context.InstrumentAcceptances
                                .AsNoTracking()
                                .FirstOrDefaultAsync(m => m.InstrumentID == id);

            if (Instrument == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}