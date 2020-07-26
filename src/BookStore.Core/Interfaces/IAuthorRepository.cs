using BookStore.Core.Entities;
using System.Threading.Tasks;

namespace BookStore.Core.Interfaces
{
    public interface IAuthorRepository : IBaseRepository<Author>
    {
        Task<bool> IsExists(int id);
    }
}
