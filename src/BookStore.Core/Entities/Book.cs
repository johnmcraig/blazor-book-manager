using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookStore.Core.Entities
{
    [Table("Books")]
    public class Book : BaseEntity
    {
        public string Title { get; set; }
        public int Year { get; set; }
        public string Summary { get; set; }
        public string Isbn { get; set; }
        public string Image { get; set; }
        public decimal? Price { get; set; }

        public int AuthorId { get; set; }
        public Author Author { get; set; }
    }
}
