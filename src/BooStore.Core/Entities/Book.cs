using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace BooStore.Core.Entities
{
    [Table("Books")]
    public partial class Book : BaseEntity
    {
        public string Title { get; set; }
        public int? Year { get; set; }
        public string MyProperty { get; set; }
        public DateTime PublishDate { get; set; }
        public string Description { get; set; }
        public string Isbn { get; set; }
        public string Image { get; set; }
        public decimal? Price { get; set; }
        public List<Author> Authors { get; set; }
    }
}
