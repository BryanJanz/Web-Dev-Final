using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace _245Final.Models
{
    public class CartViewModel
    {
        public List<CartItem> GameList { get; set; }

        public CartViewModel()
        {
            GameList = new List<CartItem>();
        }
    }
}
