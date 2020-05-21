﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using EquipmentManagementSystem.Data;
using EquipmentManagementSystem.Models;

namespace EquipmentManagementSystem.Pages.MalfunctionInfos
{
    public class IndexModel : PageModel
    {
        private readonly MalfunctionContext _context;

        public IndexModel(MalfunctionContext context)
        {
            _context = context;
        }

        public IList<MalfunctionInfo> MalfunctionInfo { get;set; }

        public async Task OnGetAsync()
        {
            MalfunctionInfo = await _context.MalfunctionInfo.ToListAsync();
        }
    }
}
