using BookStore.Core.Entities;
using BookStore.Core.Interfaces;
using System;
using System.Collections;
using System.Threading.Tasks;

namespace BookStore.Infrastructure.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        public UnitOfWork(IBookRepository bookRepo, IAuthorRepository authRepo)
        {
            BookRepository = bookRepo;
            AuthorRepository = authRepo;
        }

        public IBookRepository BookRepository { get; private set; }
        public IAuthorRepository AuthorRepository { get; private set; }
    }
}
