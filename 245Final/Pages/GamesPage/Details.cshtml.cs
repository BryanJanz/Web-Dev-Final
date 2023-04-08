using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using _245Final.Data;
using _245Final.Models;

namespace _245Final.GamesPage
{
    public class DetailsModel : PageModel
    {
        private readonly _245Final.Data.ApplicationDbContext _context;

        public DetailsModel(_245Final.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public Games Games { get; set; }        
        public string GenreName { get; set; }
        public string PlatformName { get; set; }
        public string Manufacturer { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Games == null)
            {
                return NotFound();
            }

            //We find the game in the list based on the id sent and compare that with ID in the database
            var games = await _context.Games.FirstOrDefaultAsync(m => m.ID == id);
            //Similarly, we find the genre based on the game selected Genre string and compare that with the ID
            var genre = await _context.Genres.FirstOrDefaultAsync(g => games.Genre == g.ID.ToString());

            var plat = await _context.Platform.FirstOrDefaultAsync(p => games.Platform == p.ID.ToString());
            
            if (games == null)
            {
                return NotFound();
            }
            else
            {
                Games = games;
                //We assign the property GenreName that is used in the cshtml to the Genre property found above
                GenreName = genre.Genre;
                PlatformName = plat.SystemName;
                Manufacturer = plat.Manufacturer;
            }
            return Page();
        }
    }
}
