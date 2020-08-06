using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.UI.Wasm.Static
{
    public static class Endpoints
    {
        public static string BaseUrl = "https://localhost:5001";
        public static string AuthorsEndpont = $"{BaseUrl}/api/authors/";
        public static string BooksEndpont = $"{BaseUrl}/api/books/";
    }
}
