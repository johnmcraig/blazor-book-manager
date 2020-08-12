using BookStore.UI.Wasm.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.UI.Wasm.Contracts
{
    public interface IBookRepository : IRepositoryService<Book>
    {
    }
}
