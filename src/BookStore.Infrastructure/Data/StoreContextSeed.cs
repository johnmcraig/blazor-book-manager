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
                        LastName = "Street",
                        Bio = "A pretty nice writer."
                    },
                    new Author
                    {
                        FirstName = "Bill",
                        LastName = "Hartley",
                        Bio = "Fun is the name of the game."
                    }
                };

                await dbContext.Authors.AddRangeAsync(authors);
            }

            if (!dbContext.Books.Any())
            {
                var books = new List<Book>
                {
                    new Book
                    {
                        AuthorId = 1,
                        Title = "Super Thriller",
                        Year = 2009,
                        Summary = "A heart racing thriller story",
                        Isbn = "TEST-1111-22BN",
                        Price = 19.99m
                    },
                    new Book
                    {
                        AuthorId = 1,
                        Title = "Super Thriller 2",
                        Year = 2010,
                        Summary = "A sequel to the best heart racing thriller story",
                        Isbn = "TEST-2222-23BN",
                        Price = 29.99m
                    },
                    new Book
                    {
                        AuthorId = 1,
                        Title = "Super Thriller 3",
                        Year = 2011,
                        Summary = "The third installment of heart racing thriller series",
                        Isbn = "TEST-3333-32BN",
                        Price = 39.99m
                    },
                    new Book
                    {
                        AuthorId = 2,
                        Title = "A Trip Outside",
                        Year = 2015,
                        Summary = "Lighthearted fun in the sun",
                        Isbn = "TEST-4444-45BN",
                        Price = 39.99m
                    }
                };

                await dbContext.Books.AddRangeAsync(books);
            }

            await dbContext.SaveChangesAsync();
        }
    }
}
