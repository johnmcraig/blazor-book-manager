using System;
using System.Net.Http;
using System.Threading.Tasks;
using BookStore.UI.Wasm.Contracts;
using BookStore.UI.Wasm.Services;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Blazored.LocalStorage;
using Blazored.Toast;

namespace BookStore.UI.Wasm
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("app");

            builder.Services.AddTransient(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
            builder.Services.AddBlazoredLocalStorage();
            builder.Services.AddBlazoredToast();
            builder.Services.AddTransient(typeof(IRepositoryService<>), typeof(RepositoryService<>));
            builder.Services.AddTransient<IAuthorRepository, AuthorService>();
            builder.Services.AddTransient<IBookRepository, BookService>();
            builder.Services.AddOptions();
            await builder.Build().RunAsync();
        }
    }
}
