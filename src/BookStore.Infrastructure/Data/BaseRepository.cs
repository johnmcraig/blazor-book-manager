using BookStore.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace BookStore.Infrastructure.Data
{
    public class BaseRepository<T> : IBaseRepository<T> where T : class
    {
        private readonly StoreContext _context;

        public BaseRepository(StoreContext context)
        {
            _context = context;
        }

        public async Task<IList<T>> FindAll()
        {
            return await _context.Set<T>().ToListAsync();
        }

        public async Task<T> FindById(int id)
        {
            return await _context.Set<T>().FindAsync(id);
        }

        public async Task<bool> Update(T entity)
        {
            _context.Set<T>().Attach(entity);
            _context.Entry(entity).State = EntityState.Modified;
            return await SaveAll();
        }

        public async Task<bool> Delete(T entity)
        {
            _context.Set<T>().Remove(entity);
            return await SaveAll();
        }

        public async Task<bool> Create(T entity)
        {
            await _context.Set<T>().AddAsync(entity);
            return await SaveAll();
        }

        public async Task<bool> SaveAll()
        {
            return (await _context.SaveChangesAsync() > 0);
        }
    }
}
