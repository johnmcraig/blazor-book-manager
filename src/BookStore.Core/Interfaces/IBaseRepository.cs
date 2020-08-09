using System.Collections.Generic;
using System.Threading.Tasks;

namespace BookStore.Core.Interfaces
{
    public interface IBaseRepository<T> where T : class
    {
        Task<IList<T>> FindAll();
        Task<T> FindById(int id);
        Task<bool> Create(T entity);
        Task<bool> Update(T entity);
        Task<bool> Delete(T entity);
    }
}
