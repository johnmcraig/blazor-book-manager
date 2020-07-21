using System;
using System.Collections.Generic;
using System.Text;

namespace BooStore.Core.Entities
{
    public class Author : BaseEntity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int BookId { get; set; }
    }
}
