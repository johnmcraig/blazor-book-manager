using BookStore.Core.Entities;
using BookStore.Core.Interfaces;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Text;

namespace BookStore.Infrastructure.Services
{
    public class AuthorCache : IAuthorCache
    {
        private MemoryCache _cache { get; set; }

        private string GetCacheKey(int id) => $"Author-{id}";

        public AuthorCache()
        {
            _cache = new MemoryCache(new MemoryCacheOptions
            {
                SizeLimit = 100
            });
        }

        public Author Get(int id)
        {
            _cache.TryGetValue(GetCacheKey(id), out Author author);
            return author;
        }

        public void Remove(int id)
        {
            _cache.Remove(GetCacheKey(id));
        }

        public void Set(Author author)
        {
            var cacheEntryOptions = new MemoryCacheEntryOptions().SetSize(1);
            _cache.Set(GetCacheKey(author.Id), author, cacheEntryOptions);
        }

    }
}
