﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using _245Final.Data;
using _245Final.Models;
using Microsoft.AspNetCore.Authorization;
using System.Data;

namespace _245Final.Pages.PlatformsPage
{
    [Authorize(Roles = "Admin")]
    public class DeleteModel : PageModel
    {
        private readonly _245Final.Data.ApplicationDbContext _context;

        public DeleteModel(_245Final.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
      public Platform Platform { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Platform == null)
            {
                return NotFound();
            }

            var platform = await _context.Platform.FirstOrDefaultAsync(m => m.ID == id);

            if (platform == null)
            {
                return NotFound();
            }
            else 
            {
                Platform = platform;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null || _context.Platform == null)
            {
                return NotFound();
            }
            var platform = await _context.Platform.FindAsync(id);

            if (platform != null)
            {
                Platform = platform;
                _context.Platform.Remove(Platform);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
