using System.Net.Http;
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
        private readonly ILocalStorageService _localStorage;

        public AuthorService(HttpClient client, ILogger<AuthorService> logger, ILocalStorageService localStorage) : base(client, logger, localStorage)
        {
            _logger = logger;
            _client = client;
            _localStorage = localStorage;
        }
    }
}