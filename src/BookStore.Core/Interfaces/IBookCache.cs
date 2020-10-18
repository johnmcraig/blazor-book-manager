using BookStore.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Core.Interfaces
{
    public interface IBookCache
    {
        Book Get(int id);
        void Remove(int id);
        void Set(Book book);
    }
}
