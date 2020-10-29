using BookStore.Core.Entities;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.Infrastructure.Data
{
    public class ApiDbContextSeed
    {
        public static async Task SeedAsync(ApiDbContext dbContext, ILoggerFactory logger)
        {
            if(!dbContext.Authors.Any())
            {
                var authors = new List<Author>
                {
                    new Author
                    {
                        FirstName = "Andrew",
                        LastName = "Lock",
                        Bio = "Author and blogger of ASP.Net Core under Manning, physical and digital."
                    },
                    new Author
                    {
                        FirstName = "Jon",
                        LastName = "Smith",
                        Bio = "Technical writer for Entity Framework Core documentation."
                    },
                    new Author
                    {
                        FirstName = "Adam",
                        LastName = "Freeman",
                        Bio = "UK based author of many software development framework books."
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
                        Title = "ASP.Net Core In Action",
                        Year = 2018,
                        Summary = "A particle guide on the ASP.Net Core framework.",
                        Isbn = "978-1617294617",
                        Price = 44.99m
                    },
                    new Book
                    {
                        AuthorId = 2,
                        Title = "Entity Framework Core In Action",
                        Year = 2018,
                        Summary = "This teaches you how to access and update relational data from .NET applications. Following the crystal-clear explanations, real-world examples, and around 100 diagrams, you’ll discover time-saving patterns and best practices for security, performance tuning, and unit testing.",
                        Isbn = "978-1617294563",
                        Price = 39.70m
                    },
                    new Book
                    {
                        AuthorId = 3,
                        Title = "Pro ASP.Net Core 3",
                        Year = 2020,
                        Summary = "Now in its 8th edition, the comprehensive book you need to learn ASP.NET Core development!",
                        Isbn = "978-1484254394",
                        Price = 34.99m
                    },
                    new Book
                    {
                        AuthorId = 3,
                        Title = "Pro React 16",
                        Year = 2019,
                        Summary = "Use this book to build dynamic JavaScript applications using the popular React library.",
                        Isbn = "978-1484244500",
                        Price = 35.97m
                    }
                };

                await dbContext.Books.AddRangeAsync(books);
            }

            await dbContext.SaveChangesAsync();
        }
    }
}
