using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace BookStore.UI.Wasm.Models
{
    public class Book
    {
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }
        [DisplayName("Year of Publication")]
        public int Year { get; set; }
        public string Summary { get; set; }
        [Required]
        [MaxLength(14)]
        public string Isbn { get; set; }
        public string Image { get; set; } = "https://via/placeholder.com/300x300";
        public decimal? Price { get; set; }

        public int AuthorId { get; set; }
        public virtual Author Author { get; set; }
    }
}