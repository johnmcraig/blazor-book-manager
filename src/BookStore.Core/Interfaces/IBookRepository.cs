using BookStore.Core.Entities;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BookStore.Core.Interfaces
{
    public interface IBookRepository : IBaseRepository<Book>
    {
        Task<bool> IsExists(int id);
        Task<IList<Book>> FindBookBySearch(string search);
    }
}
