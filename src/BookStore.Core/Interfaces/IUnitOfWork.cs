using BookStore.Core.Entities;
using System;
using System.Threading.Tasks;

namespace BookStore.Core.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IBaseRepository<TEntity> Repository<TEntity>() where TEntity : BaseEntity;
        Task<int> Complete();
    }
}
