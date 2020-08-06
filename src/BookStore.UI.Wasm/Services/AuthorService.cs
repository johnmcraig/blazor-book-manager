using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Blazored.LocalStorage;
using BookStore.UI.Wasm.Contracts;
using BookStore.UI.Wasm.Models;
using Microsoft.Extensions.Logging;

namespace BookStore.UI.Wasm.Services
{
    public class AuthorService : RepositoryService<Author>, IAuthorRepository
    {
        private readonly HttpClient _client;
        private readonly ILogger<AuthorService> _logger;
        //private readonly ILocalStorageService _localStorage;

        public AuthorService(HttpClient client, ILogger<AuthorService> logger) : base(client, logger)
        {
            _logger = logger;
            _client = client;
            // , ILocalStorageService localStorage _localStorage = localStorage;
        }

        //private async Task<string> GetBearerToken()
        //{
        //    return await _localStorage.GetItemAsync<string>("authToken");
        //}
    }
}