using BookStore.Core.Entities;
using BookStore.Core.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BookStore.Infrastructure.Data
{
    public class BookRepository : IBookRepository
    {
        private readonly StoreContext _context;

        public BookRepository(StoreContext context)
        {
            _context = context;
        }

        public async Task<IList<Book>> FindAll()
        {
            var books = await _context.Books.ToListAsync();
            return books;
        }

        public async Task<Book> FindById(int id)
        {
            var book = await _context.Books.FindAsync(id);
            return book;
        }

        public async Task<bool> Create(Book entity)
        {
            await _context.Books.AddAsync(entity);
            return await SaveAll();
        }

        public async Task<bool> Delete(Book entity)
        {
            _context.Books.Remove(entity);
            return await SaveAll();
        }

        public async Task<bool> Update(Book entity)
        {
            _context.Books.Update(entity);
            return await SaveAll();
        }

        public async Task<bool> SaveAll()
        {
            return (await _context.SaveChangesAsync() > 0);
        }

        public async Task<bool> IsExists(int id)
        {
            var isExists = await _context.Books.AnyAsync(q => q.Id == id);
            return isExists;
        }
    }
}
