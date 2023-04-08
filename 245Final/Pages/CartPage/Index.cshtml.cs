using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using _245Final.Data;
using _245Final.Models;
using Newtonsoft.Json;
using _245Final.GamesPage;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authorization;

namespace _245Final.Pages.CartPage
{
    [Authorize(Roles = "User")]
    public class IndexModel : PageModel
    {
        private readonly _245Final.Data.ApplicationDbContext _context;

        public IndexModel(_245Final.Data.ApplicationDbContext context)
        {
            _context = context;
        }
        public IList<Games> Games { get; set; } /*= default!;*/
        public CartViewModel Cart { get; set; }
        public IList<CartItem> CartItems { get; set; }

        public async Task OnGetAsync()
        {
            int gameId = 0;
            int quantity = 0;

            if (TempData["gameId"] != null && TempData["quantity"] != null)
            {
                gameId = (int)TempData["gameId"];
                quantity = (int)TempData["quantity"];
            }
            var game = await _context.Games.FindAsync(gameId);

            Cart = HttpContext.Session.GetObjectFromJson<CartViewModel>("Cart");
            if (Cart == null)
                Cart = new();

            CartItem cartItem = null;

            if (quantity == 0)
            {
                var existingItem = Cart.GameList.FirstOrDefault(item => item.GameID == gameId);
                if (existingItem != null)
                {
                    Cart.GameList.Remove(existingItem);
                }
            }
            else
            {

                var existingItem = Cart.GameList.FirstOrDefault(item => item.GameID == gameId);
                if (existingItem != null)
                    existingItem.Quantity += quantity;
                else
                    cartItem = new CartItem
                    {
                        GameID = game.ID,
                        Quantity = quantity,
                        Game = game
                    };

                if (cartItem != null)
                    Cart.GameList.Add(cartItem);
            }

            if (_context.Games != null)
            {
                Games = await _context.Games.OrderBy(g => g.Title).ThenBy(g => g.Platform).ToListAsync();

                foreach (var specificGame in Games)
                {
                    var genre = await _context.Genres.FirstOrDefaultAsync(g => g.ID.ToString() == specificGame.Genre);
                    specificGame.Genre = genre?.Genre;
                    var plat = await _context.Platform.FirstOrDefaultAsync(p => p.ID.ToString() == specificGame.Platform);
                    specificGame.Platform = plat?.SystemName;
                }

            }

            HttpContext.Session.SetObjectAsJson("Cart", Cart);
        }

        public IActionResult OnPostRemoveFromCart(int id)
        {
            Cart = HttpContext.Session.GetObjectFromJson<CartViewModel>("Cart");
            if (Cart != null)
            {
                var cartItemToRemove = Cart.GameList.FirstOrDefault(item => item.GameID == id);
                if (cartItemToRemove != null)                
                    Cart.GameList.Remove(cartItemToRemove);
                
                HttpContext.Session.SetObjectAsJson("Cart", Cart);
            }
            return Page();
        }

        public IActionResult OnPostUpdateCart(int id, int quantity)
        {
            Cart = HttpContext.Session.GetObjectFromJson<CartViewModel>("Cart");
            if (Cart != null)
            {
                var cartItemToChange = Cart.GameList.FirstOrDefault(item => item.GameID == id);
                if (cartItemToChange != null)                
                    if (quantity != 0)
                    {
                        cartItemToChange.Quantity = quantity;
                        HttpContext.Session.SetObjectAsJson("Cart", Cart);
                    }
                    else
                        OnPostRemoveFromCart(id);                
            }
            return Page();
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
