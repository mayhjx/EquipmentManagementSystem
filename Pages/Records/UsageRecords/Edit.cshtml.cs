﻿using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EquipmentManagementSystem.Authorization;
using EquipmentManagementSystem.Data;
using EquipmentManagementSystem.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EquipmentManagementSystem.Pages.Records.UsageRecords
{
    public class EditModel : BasePageModel
    {
        public EditModel(EquipmentContext context,
            UserManager<User> userManager,
            IAuthorizationService authorizationService)
            : base(context, userManager, authorizationService)
        {
        }

        [BindProperty]
        public UsageRecord UsageRecord { get; set; }
        public IList<AuditTrailLog> AuditTrailLogs { get; set; }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            UsageRecord = await _context.UsageRecords
                                .AsNoTracking()
                                .Include(m => m.Project)
                                    .ThenInclude(p => p.Group)
                                .FirstOrDefaultAsync(m => m.Id == id);

            if (UsageRecord == null)
            {
                return NotFound();
            }

            var isAuthorized = await _authorizationService.AuthorizeAsync(User, UsageRecord, Operations.Update);

            if (!isAuthorized.Succeeded)
            {
                return Forbid();
            }

            AuditTrailLogs = await _context.AuditTrailLogs
                .AsNoTracking()
                .Where(l => l.EntityName == UsageRecord.GetType().Name && l.PrimaryKeyValue == id.ToString())
                .OrderByDescending(l => l.DateChanged)
                .ToListAsync();

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int id)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var UsageRecordToUpdate = await _context.UsageRecords
                                            .Include(m => m.Project)
                                                .ThenInclude(p => p.Group)
                                            .FirstOrDefaultAsync(m => m.Id == id);

            if (UsageRecordToUpdate == null)
            {
                return NotFound();
            }

            var isAuthorized = await _authorizationService.AuthorizeAsync(User, UsageRecordToUpdate, Operations.Update);

            if (!isAuthorized.Succeeded)
            {
                return Forbid();
            }

            if (await TryUpdateModelAsync<UsageRecord>(
                UsageRecordToUpdate,
                "UsageRecord",
                // i => i.BeginTimeOfMaintain, 
                i => i.BeginTimeOfTest,
                i => i.ColumnNumber, i => i.ColumnPressure, i => i.PressureUnit,
                i => i.ColumnTwoNumber, i => i.ColumnTwoPressure,
                i => i.SampleNumber, i => i.TestNumber, i => i.VacuumDegree,
                i => i.VacuumDegreeUnit, i => i.BlankSignal, i => i.TestSignal,
                i => i.EndTime, i => i.Remark))
            {
                await _context.SaveChangesAsync();
                return RedirectToPage("../Index");
            }

            return Page();
        }
    }
}