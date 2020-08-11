using BookStore.Core.Entities;
using BookStore.Core.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Infrastructure.Data
{
    public class AuthorEfCoreRepository : IAuthorEfRepository, IEfCoreExtensions
    {
        private readonly StoreContext _context;

        public AuthorEfCoreRepository(StoreContext context)
        {
            _context = context;
        }

        public async Task<IList<Author>> FindAll()
        {
            var authors = await _context.Authors
                .Include(a => a.Books)
                .ToListAsync();
            return authors;
        }

        public async Task<IList<Author>> FindBySearch(string search)
        {
            var authors = await _context.Authors
                .Include(a => a.Books)
                .ToListAsync();
            return authors;
        }

        public async Task<Author> FindById(int id)
        {
            var author = await _context.Authors
                .Include(q => q.Books)
                .FirstOrDefaultAsync(x => x.Id == id);
            return author;
        }
        
        public async Task<bool> Create(Author entity)
        {
            await _context.Authors.AddAsync(entity);
            return await SaveAll();
        }

        public async Task<bool> Delete(Author entity)
        {
            _context.Authors.Remove(entity);
            return await SaveAll();
        }

        public async Task<bool> Update(Author entity)
        {
            _context.Authors.Update(entity);
            return await SaveAll();
        }

        public async Task<bool> SaveAll()
        {
            return (await _context.SaveChangesAsync() > 0);
        }

        public async Task<bool> IsExists(int id)
        {
            return await _context.Authors.AnyAsync(q => q.Id == id);
        }
    }
}
