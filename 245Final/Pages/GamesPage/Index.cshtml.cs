using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using _245Final.Data;
using _245Final.Models;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Identity;

namespace _245Final.GamesPage
{
    public class IndexModel : PageModel
    {
        private readonly _245Final.Data.ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public IndexModel(_245Final.Data.ApplicationDbContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public IList<Games> Games { get; set; } = default!;
        //public Cart myCart { get; set; }
        public IList<CartItem> CartItems { get; set; }

        public async Task OnGetAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            string s;
            if (user != null)
                s = user.Email;


            if (_context.Games != null)
            {
                Games = await _context.Games.OrderBy(g => g.Title).ThenBy(g => g.Platform).ToListAsync();

                foreach (var game in Games)
                {
                    var genre = await _context.Genres.FirstOrDefaultAsync(g => g.ID.ToString() == game.Genre);
                    game.Genre = genre?.Genre;
                    var plat = await _context.Platform.FirstOrDefaultAsync(p => p.ID.ToString() == game.Platform);
                    game.Platform = plat?.SystemName;
                }
            }
        }

        public IActionResult OnPost(int gameId, int quantity)
        {
            TempData["gameId"] = gameId;
            TempData["quantity"] = quantity;

            return RedirectToPage("/CartPage/Index");
        }
    }
    public static class SessionExtensions
    {
        public static void SetObjectAsJson(this ISession session, string key, object value)
        {
            session.SetString(key, JsonConvert.SerializeObject(value));
        }
        public static T GetObjectFromJson<T>(this ISession session, string key)
        {
            var value = session.GetString(key);
            return value == null ? default(T) : JsonConvert.DeserializeObject<T>(value);
        }
    }
}
