using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Core.Interfaces
{
    public  interface IEfCoreExtensions
    {
        Task<bool> SaveAll();
    }
}
