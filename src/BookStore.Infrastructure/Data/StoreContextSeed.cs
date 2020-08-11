using BookStore.Core.Entities;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Infrastructure.Data
{
    public class StoreContextSeed
    {
        public static async Task SeedAsync(StoreContext dbContext, ILoggerFactory logger)
        {
            if(!dbContext.Authors.Any())
            {
                var authors = new List<Author>
                {
                    new Author
                    {
                        FirstName = "Jane",
                        LastName ="Street",
                        Bio =   "A pretty nice writer"
                    },
                    new Author
                    {
                        FirstName = "Bill",
                        LastName ="Hardley",
                        Bio =   "Fun is the name of the game"
                    }
                };

                await dbContext.Authors.AddRangeAsync(authors);
            }

            if (!dbContext.Books.Any())
            {
                var replies = new List<Book>
                {
                    new Book
                    {
                        AuthorId = 1,
                        Title = "Super Thriller",
                        Year = 2009,
                        Summary = "A heart reacing thriller story",
                        Isbn = "TEST-1111-22BN",
                        Price = (decimal?)19.99
                    },
                    new Book
                    {
                        AuthorId = 1,
                        Title = "Super Thriller 2",
                        Year = 2010,
                        Summary = "A sequel to the best heart reacing thriller",
                        Isbn = "TEST-2222-23BN",
                        Price = (decimal?)29.99
                    },
                    new Book
                    {
                        AuthorId = 1,
                        Title = "Super Thriller 3",
                        Year = 2011,
                        Summary = "The third installment of heart racing thriller series",
                        Isbn = "TEST-3333-32BN",
                        Price = (decimal?)39.99
                    },
                    new Book
                    {
                        AuthorId = 2,
                        Title = "A Trip Outside",
                        Year = 2015,
                        Summary = "Light hearted fun in the sun",
                        Isbn = "TEST-4444-45BN",
                        Price = (decimal?)39.99
                    }
                };

                await dbContext.Books.AddRangeAsync(replies);
            }

            await dbContext.SaveChangesAsync();
        }
    }
}
