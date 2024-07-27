﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Test.Models;

namespace BPA.FE.Pages.ReportManage
{
    public class DetailsModel : PageModel
    {
        private readonly Test.Models.BPADatabaseContext _context;

        public DetailsModel(Test.Models.BPADatabaseContext context)
        {
            _context = context;
        }

        public Report Report { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var report = await _context.Reports.FirstOrDefaultAsync(m => m.id == id);
            if (report == null)
            {
                return NotFound();
            }
            else
            {
                Report = report;
            }
            return Page();
        }
    }
}
