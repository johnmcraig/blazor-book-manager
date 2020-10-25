using BookStore.Core.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BookStore.Core.Interfaces
{
    public interface IBaseRepository<T> where T : BaseEntity
    {
        Task<IList<T>> FindAll();
        Task<IList<T>> FindBySearch(string search);
        Task<T> FindById(int id);
        Task<bool> Create(T entity);
        Task<bool> Update(T entity);
        Task<bool> Delete(T entity);
        Task<bool> IsExists(int id);
    }
}
