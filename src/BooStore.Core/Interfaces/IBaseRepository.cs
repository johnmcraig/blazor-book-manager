using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BooStore.Core.Interfaces
{
    public interface IBaseRepository<T> where T : class
    {
        Task<IList<T>> GetAllToList();
        Task<T> GetByIdAsync(int id);
        Task<T> Update(T entity);
        Task<T> Delete(int id);
    }
}
