using BookStore.Core.Entities;
using BookStore.Core.Interfaces;
using Dapper;
using Microsoft.Data.Sqlite;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Infrastructure.Data
{
    public class BookSqlRepository : IBookRepository
    {
        private readonly ISqlDataAccess _sqliteData;
        private readonly ILogger<BookSqlRepository> _logger;
        private readonly string connectionString = "sqlite";

        public BookSqlRepository(ISqlDataAccess sqliteData, ILogger<BookSqlRepository> logger)
        {
            _sqliteData = sqliteData;
            _logger = logger;
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
                _logger.LogError($"{ex.Message} - {ex.InnerException}");

                return false;
            }
        }

        public async Task<bool> Delete(Book entity)
        {
            string sql = "DELETE FROM Books WHERE Id = @Id";

            try
            {
                await _sqliteData.SaveData(sql, new { entity.Id }, connectionString);

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError($"{ex.Message} - {ex.InnerException}");

                return false;
            }
        }

        public async Task<IList<Book>> FindAll()
        {
            string sql = "SELECT * FROM Books";

            try
            {
                var books = await _sqliteData.LoadData<Book, dynamic>(sql, new { }, connectionString);

                return books.ToList();
            }
            catch (Exception ex)
            {
                _logger.LogError($"{ex.Message} - {ex.InnerException}");

                return null;
            }

        }

        public async Task<IList<Book>> FindBySearch(string search)
        {
            string sql = "SELECT * FROM Books WHERE Title LIKE @Search " +
                         "UNION SELECT * FROM Books WHERE Summary LIKE @Search";

            try
            {
                var results = await _sqliteData.LoadData<Book, dynamic>(sql, new { Search = "%" + search + "%" }, connectionString);

                return results.ToList();
            }
            catch (Exception ex)
            {
                _logger.LogError($"{ex.Message} - {ex.InnerException}");

                return null;
            }
        }

        public async Task<Book> FindById(int id)
        {
            string sql = "SELECT * FROM Books Where Id = @Id";

            try
            {
                var book = await _sqliteData.LoadData<Book, dynamic>(sql, new { Id = id }, connectionString);

                return book.FirstOrDefault();
            }
            catch (Exception ex)
            {
                _logger.LogError($"{ex.Message} - {ex.InnerException}");

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
                _logger.LogError($"{ex.Message} - {ex.InnerException}");

                return false;
            }
        }

        public async Task<bool> IsExists(int id)
        {
            string sql = @"SELECT CASE WHEN EXISTS (SELECT Id FROM Books " +
                "WHERE Id = @Id)" +
                "THEN CAST(1 AS BIT) ELSE CAST(0 AS BIT) END AS Result";

            try
            {
                using (var connection = new SqliteConnection(connectionString))
                {
                    connection.Open();

                    var isExists = await connection.QueryFirstAsync<bool>(sql, new { Id = id });

                    return isExists;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"{ex.Message} - {ex.InnerException}");

                return false;
            }
        }
    }
}
