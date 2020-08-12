using BookStore.UI.Wasm.Contracts;
using BookStore.UI.Wasm.Models;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace BookStore.UI.Wasm.Services
{
    public class BookService : RepositoryService<Book>, IBookRepository
    {
        private readonly HttpClient _client;
        private readonly ILogger<BookService> _logger;
        //private readonly ILocalStorageService _localStorage;

        public BookService(HttpClient client, ILogger<BookService> logger) : base(client, logger)
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
