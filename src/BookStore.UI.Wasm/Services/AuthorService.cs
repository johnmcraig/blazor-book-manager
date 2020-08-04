using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using BookStore.UI.Wasm.Contracts;
using BookStore.UI.Wasm.Models;

namespace BookStore.UI.Wasm.Services
{
    public class AuthorService : RepositoryService<Author>, IAuthorRepository
    {
        private readonly HttpClient _client;

        public AuthorService(HttpClient client) : base(client)
        {
            _client = client;
        }

        public async Task<IList<Author>> GetAll(string url)
        {
            throw new System.NotImplementedException();
        }

        public async Task<Author> GetSingle(string url, int id)
        {
            throw new System.NotImplementedException();
        }

        public async Task<Author> Create(string url, Author entity)
        {
            throw new System.NotImplementedException();
        }

        public async Task<Author> Update(string url, Author entity, int id)
        {
            throw new System.NotImplementedException();
        }

        public async Task<bool> Delete(string url, int id)
        {
            throw new System.NotImplementedException();
        }
    }
}