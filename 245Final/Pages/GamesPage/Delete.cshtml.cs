using System;
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

namespace _245Final.GamesPage
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
        public Games Games { get; set; }
        public string GenreName { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Games == null)
            {
                return NotFound();
            }

            var games = await _context.Games.FirstOrDefaultAsync(m => m.ID == id);
            var genre = await _context.Genres.FirstOrDefaultAsync(g => games.Genre == g.ID.ToString());

            if (games == null)
            {
                return NotFound();
            }
            else
            {
                Games = games;
                GenreName = genre.Genre;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null || _context.Games == null)
            {
                return NotFound();
            }
            var games = await _context.Games.FindAsync(id);

            if (games != null)
            {
                Games = games;
                _context.Games.Remove(Games);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
