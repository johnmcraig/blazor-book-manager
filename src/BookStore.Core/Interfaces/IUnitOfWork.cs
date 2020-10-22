using BookStore.Core.Entities;
using System;
using System.Threading.Tasks;

namespace BookStore.Core.Interfaces
{
    public interface IUnitOfWork
    {
        IBookRepository BookRepository { get; }
        IAuthorRepository AuthorRepository { get; }
    }
}
