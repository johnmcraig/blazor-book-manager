using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Blazored.LocalStorage;
using BookStore.UI.Wasm.Contracts;
using BookStore.UI.Wasm.Models;

namespace BookStore.UI.Wasm.Services
{
    public class AuthorService : RepositoryService<Author>, IAuthorRepository
    {
        private readonly HttpClient _client;
        private readonly ILocalStorageService _localStorage;

        public AuthorService(HttpClient client, ILocalStorageService localStorage) : base(client, localStorage)
        {
            _client = client;
            _localStorage = localStorage;
        }

        private async Task<string> GetBearerToken()
        {
            return await _localStorage.GetItemAsync<string>("authToken");
        }
    }
}