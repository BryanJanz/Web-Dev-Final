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

namespace _245Final.GamesPage
{
    [Authorize(Roles = "Admin")]
    public class EditModel : PageModel
    {
        private readonly _245Final.Data.ApplicationDbContext _context;

        public EditModel(_245Final.Data.ApplicationDbContext context)
        {
            _context = context;
        }
        public List<Genres> Genres { get; set; }
        public List<Platform> Platforms { get; set; }

        [BindProperty]
        public Games Games { get; set; } = default!;
        
        public string GenreName { get; set; }
        public string PlatName { get; set; }
        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Games == null)
            {
                return NotFound();
            }
            Genres = _context.Genres.ToList();
            Platforms = _context.Platform.ToList();

            var games =  await _context.Games.FirstOrDefaultAsync(m => m.ID == id);
            var genre = await _context.Genres.FirstOrDefaultAsync(g => games.Genre == g.ID.ToString());
            var plat = await _context.Platform.FirstOrDefaultAsync(p => games.Platform == p.ID.ToString());

            if (games == null)
            {
                return NotFound();
            }
            Games = games;
            GenreName = genre.Genre;
            PlatName = plat.SystemName;

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

            _context.Attach(Games).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!GamesExists(Games.ID))
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

        private bool GamesExists(int id)
        {
          return _context.Games.Any(e => e.ID == id);
        }
    }
}
