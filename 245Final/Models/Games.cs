using System.ComponentModel.DataAnnotations;

namespace _245Final.Models
{
    public class Games
    {
        public int ID { get; set; }
        public string Title { get; set; }
        [DataType(DataType.Date)]
        public DateTime ReleaseDate { get; set; }
        public string Genre { get; set; }
        public string Platform { get; set; }
        public decimal Price { get; set; }
        public string Picture { get; set; }
        public decimal Rating { get; set; }
        public string Description { get; set; }
    }
}
