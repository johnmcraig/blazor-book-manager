using System.Net.Http;
using BookStore.UI.Wasm.Models;

namespace BookStore.UI.Wasm.Contracts
{
    public interface IAuthorRepository : IRepositoryService<Author>
    {
    }
}