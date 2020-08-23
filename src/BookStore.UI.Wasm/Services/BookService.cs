using Blazored.LocalStorage;
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
        private readonly ILocalStorageService _localStorage;

        public BookService(HttpClient client, ILogger<BookService> logger, ILocalStorageService localStorage) : base(client, logger, localStorage)
        {
            _logger = logger;
            _client = client;
            _localStorage = localStorage;
        }
    }
}
