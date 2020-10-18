using BookStore.Core.Interfaces;
using Microsoft.Extensions.Caching.Memory;
using BookStore.Core.Entities;

namespace BookStore.Infrastructure.Services
{
    public class BookCache: IBookCache
    {
        private MemoryCache _cache { get; set; }

        private string GetCacheKey(int id) => $"Book-{id}";

        public BookCache()
        {
            _cache = new MemoryCache(new MemoryCacheOptions
            {
                SizeLimit = 100
            });
        }

        public Book Get(int id)
        {
            _cache.TryGetValue(GetCacheKey(id), out Book book);
            return book;
        }

        public void Remove(int id)
        {
            _cache.Remove(GetCacheKey(id));
        }

        public void Set(Book book)
        {
            var cacheEntryOptions = new MemoryCacheEntryOptions().SetSize(1);
            _cache.Set(GetCacheKey(book.Id), book, cacheEntryOptions);
        }
    }
}
