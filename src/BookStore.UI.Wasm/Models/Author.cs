using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace BookStore.UI.Wasm.Models
{
    public class Author
    {
        public int Id { get; set; }

        [Required]
        [DisplayName("First Name")]
        public string FirstName { get; set; }

        [Required]
        [DisplayName("Last Name")]
        public string LastName { get; set; }

        public string FullName { get { return $"{FirstName} {LastName}"; } }

        [Required]
        [DisplayName("Biography")]
        [StringLength(250)]
        public string Bio { get; set; }

        public virtual IList<Book> Books { get; set; }
    }
}