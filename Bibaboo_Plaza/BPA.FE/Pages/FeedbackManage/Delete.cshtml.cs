﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Test.Models;

namespace BPA.FE.Pages.FeedbackManage
{
    public class DeleteModel : PageModel
    {
        private readonly Test.Models.BPADatabaseContext _context;

        public DeleteModel(Test.Models.BPADatabaseContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Feedback Feedback { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var feedback = await _context.Feedbacks.FirstOrDefaultAsync(m => m.id == id);

            if (feedback == null)
            {
                return NotFound();
            }
            else
            {
                Feedback = feedback;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var feedback = await _context.Feedbacks.FindAsync(id);
            if (feedback != null)
            {
                Feedback = feedback;
                _context.Feedbacks.Remove(Feedback);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}