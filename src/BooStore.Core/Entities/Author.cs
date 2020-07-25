using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace BooStore.Core.Entities
{
    [Table("Authors")]
    public partial class Author : BaseEntity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FullName { get { return $"{FirstName} {LastName}"; } }
        public string Bio { get; set; }
        public virtual IList<Book> Books { get; set; }
    }
}
