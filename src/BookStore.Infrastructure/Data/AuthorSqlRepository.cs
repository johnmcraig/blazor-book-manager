using BookStore.Core.Entities;
using BookStore.Core.Interfaces;
using BookStore.Infrastructure.DataAccess;
using Dapper;
using Microsoft.Data.Sqlite;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Infrastructure.Data
{
    public class AuthorSqlRepository : IAuthorEfRepository
    {
        private readonly ISqlDataAccess _sqliteData;
        private readonly ILogger<AuthorSqlRepository> _logger;
        private readonly string connectionString = "sqlite";
        private readonly IDbConnection cnn;

        public AuthorSqlRepository(ISqlDataAccess sqliteData, ILogger<AuthorSqlRepository> logger, IConfiguration configuration)
        {
            _sqliteData = sqliteData;
            _logger = logger;
            this.cnn = new SqliteConnection(configuration.GetConnectionString(connectionString));
        }

        public async Task<bool> Create(Author entity)
        {
            string sql = "INSERT INTO Authors (FirstName, LastName, Bio) VALUES (@FirstName, @LastName, @Bio);";

            try
            {
                var author = new
                {
                    entity.FirstName,
                    entity.LastName,
                    entity.Bio
                };

                await _sqliteData.SaveData(sql, author, connectionString);

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError($"{ex.Message} - {ex.InnerException}");

                return false;
            }
        }

        public async Task<bool> Delete(Author entity)
        {
            string sql = "DELETE FROM Authors WHERE Id = @Id";

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

        public async Task<IList<Author>> FindAll()
        {
            string sql = @"SELECT a.*, b.* FROM Authors AS a LEFT JOIN Books AS b ON a.Id = b.AuthorId;";
            
            try
            {
                var authors = await _sqliteData.LoadData<Author, dynamic>(sql, new { }, connectionString);
                return authors.ToList();

                //using (var connection = new SqliteConnection(connectionString))
                //{
                //    connection.Open();

                //var authorDic = new Dictionary<int, Author>();

                //var list = await cnn.QueryAsync<Author, Book, Author>(sql, (a, b) =>
                //{
                //    if (!authorDic.TryGetValue(a.Id, out var currentAuthor))
                //    {
                //        currentAuthor = a;
                //        authorDic.Add(currentAuthor.Id, currentAuthor);
                //    }
                //    currentAuthor.Books.Add(b);
                //    return currentAuthor;
                //}, splitOn: "AuthorId");

                //return list.Distinct().ToList();
                //}
            }
            catch (Exception ex)
            {
                _logger.LogError($"{ex.Message} - {ex.InnerException}");

                return null;
            }
            
        }

        public async Task<IList<Author>> FindBySearch(string search)
        {
            string sql = "SELECT * FROM Authors WHERE FirstName LIKE @Search " +
                         "UNION SELECT * FROM Authors WHERE LastName LIKE @Search";

            try
            {
                var results = await _sqliteData.LoadData<Author, dynamic>(sql, new {Search = "%" + search + "%"}, connectionString);

                return results.ToList();
            }
            catch (Exception ex)
            {
                _logger.LogError($"{ex.Message} - {ex.InnerException}");

                return null;
            }
        }

        public async Task<Author> FindById(int id)
        {
            string sql = "SELECT * FROM Authors Where Id = @Id";

            try
            {
                var author = await _sqliteData.LoadData<Author, dynamic>(sql, new { Id = id }, connectionString);

                return author.FirstOrDefault();
            }
            catch (Exception ex)
            {
                _logger.LogError($"{ex.Message} - {ex.InnerException}");

                return null;
            }
        }

        public async Task<bool> Update(Author entity)
        {
            string sql = "UPDATE Authors SET FirstName = @FirstName, LastName = @LastName, Bio = @Bio" +
                " WHERE Id = @Id";

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
            string sql = @"SELECT CASE WHEN EXISTS (SELECT Id FROM Authors " +
                "WHERE Id = @Id)" + 
                "THEN CAST(1 AS BIT) ELSE CAST(0 AS BIT) END AS Result";

            try
            {
                using var connection = new SqliteConnection(connectionString);

                connection.Open();

                var isExists = await connection.QueryFirstAsync<bool>(sql, new { Id = id });

                return isExists;
            }
            catch (Exception ex)
            {
                _logger.LogError($"{ex.Message} - {ex.InnerException}");

                return false;
            }
        }
    }
}
