using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace _245Final.Models
{
    public class CartItem
    {
        [Key]
        public int ID { get; set; }
        public int Quantity { get; set; }
        [ForeignKey("Game")]
        public int GameID { get; set; }
        public Games Game { get; set; }
    }
}
