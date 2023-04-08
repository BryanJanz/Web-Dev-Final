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

namespace _245Final.GenresPage
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
      public Genres Genres { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Genres == null)
            {
                return NotFound();
            }

            var genres = await _context.Genres.FirstOrDefaultAsync(m => m.ID == id);

            if (genres == null)
            {
                return NotFound();
            }
            else 
            {
                Genres = genres;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null || _context.Genres == null)
            {
                return NotFound();
            }
            var genres = await _context.Genres.FindAsync(id);

            if (genres != null)
            {
                Genres = genres;
                _context.Genres.Remove(Genres);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
