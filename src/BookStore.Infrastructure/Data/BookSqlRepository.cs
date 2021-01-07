﻿using BookStore.Core.Entities;
using BookStore.Core.Interfaces;
using Dapper;
using Microsoft.Data.Sqlite;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace BookStore.Infrastructure.Data
{
    public class BookSqlRepository : IBookRepository
    {
        private readonly ISqlDataAccess _sqliteData;
        private readonly ILogger<BookSqlRepository> _logger;
        private readonly string connectionString = "sqlite";
        private readonly IConfiguration _config;

        public BookSqlRepository(ISqlDataAccess sqliteData,
            ILogger<BookSqlRepository> logger,
            IConfiguration config)
        {
            _sqliteData = sqliteData;
            _logger = logger;
            _config = config;
        }

        public async Task<bool> Create(Book entity)
        {
            string sql = "INSERT INTO Books (Title, Year, Summary, Isbn, Price, Image, AuthorId) VALUES " +
                         "(@Title, @Year, @Summary, @Isbn, @Price, @Image, @AuthorId);";

            try
            {
                var book = new
                {
                    entity.Title,
                    entity.Year,
                    entity.Summary,
                    entity.Isbn,
                    entity.Price,
                    entity.Image,
                    entity.AuthorId,
                };

                await _sqliteData.SaveData(sql, book, connectionString);

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError($"{ex.Message}");

                return false;
            }
        }

        public async Task<bool> Delete(Book entity)
        {
            string sql = "DELETE FROM Books WHERE Id = @Id";

            try
            {
                await _sqliteData.SaveData(sql, new {entity.Id}, connectionString);

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError($"{ex.Message}");

                return false;
            }
        }

        public async Task<IList<Book>> FindAll()
        {
            string sql = "SELECT books.*, author.* FROM Books AS books " +
                         "LEFT JOIN Authors AS author ON books.AuthorId = author.Id";
            try
            {
                using (var connection = new SqliteConnection(_config
                    .GetConnectionString(connectionString)))
                {
                    var bookResults = await connection
                        .QueryAsync<Book, Author, Book>(sql, 
                        (books, author) =>
                        {
                            books.Author = author;
                            return books;
                        }, splitOn: "AuthorId");

                    return bookResults.ToList();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"{ex.Message}");

                return null;
            }
        }

        public async Task<IList<Book>> FindBySearch(string search)
        {
            string sql = "SELECT books.*, author.* FROM Books AS books " +
                         "LEFT JOIN Authors AS author ON books.AuthorId = author.Id " +
                         "WHERE Title LIKE @Search "; 
            
            try
            {
                using (var connection = new SqliteConnection(_config
                    .GetConnectionString(connectionString)))
                {
                    var bookResults = await connection
                        .QueryAsync<Book, Author, Book>(sql,
                        (books, author) =>
                        {
                            books.Author = author;
                            return books;
                        }, 
                        new
                        {
                            Search = "%" + search + "%"
                        }, splitOn: "AuthorId");

                    return bookResults.ToList();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"{ex.Message}");

                return null;
            }
        }

        public async Task<Book> FindById(int id)
        {
            string sql = "SELECT b.Id, b.AuthorId, b.Title, b.Price, b.Year, b.Isbn, b.Summary, " +
                         "a.Id, a.FirstName, a.LastName FROM Books AS b " +
                         "INNER JOIN Authors AS a ON b.AuthorId = a.Id WHERE b.Id = @Id";

            try
            {
                using(var connection = new SqliteConnection(_config
                    .GetConnectionString(connectionString)))
                {
                    var book = await connection.QueryAsync<Book, Author, Book>
                        (sql, (book, author) => 
                        {
                            book.Author = author;
                            return book;
                        },
                        new
                        {
                            @Id = id
                        }, splitOn: "Id");

                    return book.FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"{ex.Message}");

                return null;
            }
        }

        public async Task<bool> Update(Book entity)
        {
            string sql = "UPDATE Books SET Title = @Title, Summary = @Summary, Isbn = @Isbn, Year = @Year," +
                         " Image = @Image, Price = @Price WHERE Id = @Id";

            try
            {
                await _sqliteData.SaveData(sql, entity, connectionString);

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError($"{ex.Message}");

                return false;
            }
        }

        public async Task<bool> IsExists(int id)
        {
            string sql = "SELECT CASE WHEN EXISTS (SELECT Id FROM Books WHERE Id = @Id)" +
                         "THEN CAST(1 AS BIT) ELSE CAST(0 AS BIT) END AS Result";

            try
            {
                using(var connection = new SqliteConnection(_config
                    .GetConnectionString(connectionString)))
                {
                    var isExists = await connection.QueryFirstAsync<bool>(sql,
                        new
                        {
                            @Id = id
                        });

                    return isExists;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"{ex.Message}");

                return false;
            }
        }
    }
}
