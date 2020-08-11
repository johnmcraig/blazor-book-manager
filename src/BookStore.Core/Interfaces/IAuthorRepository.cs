using BookStore.Core.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BookStore.Core.Interfaces
{
    public interface IAuthorEfRepository : IBaseRepository<Author>
    {
        Task<bool> IsExists(int id);
        
    }
}
