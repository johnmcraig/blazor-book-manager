using System;
using System.Collections.Generic;
using System.Text;

namespace BooStore.Core.Entities
{
    public class Book : BaseEntity
    {
        public string Title { get; set; }
        public DateTime PublishDate { get; set; }
        public DateTime DateRevised { get; set; }
        public string Description { get; set; }
        public string ISBN { get; set; }
        public List<Author> Authors { get; set; }
    }
}
