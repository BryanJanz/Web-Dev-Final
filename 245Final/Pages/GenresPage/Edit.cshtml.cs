using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using _245Final.Data;
using _245Final.Models;
using Microsoft.AspNetCore.Authorization;
using System.Data;

namespace _245Final.GenresPage
{
    [Authorize(Roles = "Admin")]
    public class EditModel : PageModel
    {
        private readonly _245Final.Data.ApplicationDbContext _context;

        public EditModel(_245Final.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Genres Genres { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Genres == null)
            {
                return NotFound();
            }

            var genres =  await _context.Genres.FirstOrDefaultAsync(m => m.ID == id);
            if (genres == null)
            {
                return NotFound();
            }
            Genres = genres;
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(Genres).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!GenresExists(Genres.ID))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool GenresExists(int id)
        {
          return _context.Genres.Any(e => e.ID == id);
        }
    }
}
