using BookStore.Core.Entities;
using System.Threading.Tasks;

namespace BookStore.Core.Interfaces
{
    public interface IBookRepository : IBaseRepository<Book>
    {
        Task<bool> IsExists(int id);
    }
}
