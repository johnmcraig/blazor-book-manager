using System.ComponentModel.DataAnnotations;

namespace BookStore.Core.Entities
{
    public class BaseEntity
    {
        [Key]
        public int Id { get; set; }
    }
}
