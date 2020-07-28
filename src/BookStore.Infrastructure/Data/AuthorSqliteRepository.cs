using BookStore.Core.Entities;
using BookStore.Core.Interfaces;
using BookStore.Infrastructure.DataAccess;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Infrastructure.Data
{
    public class AuthorSqliteRepository : IAuthorRepository
    {
        private readonly SqliteDataAccess _sqliteData;
        private readonly ILogger<AuthorSqliteRepository> _logger;
        private readonly string connectionString = "sqlite";

        public AuthorSqliteRepository(SqliteDataAccess sqliteData, ILogger<AuthorSqliteRepository> logger)
        {
            _sqliteData = sqliteData;
            _logger = logger;
        }

        public async Task<bool> Create(Author entity)
        {
            try
            {
                var author = new
                {
                    entity.FirstName,
                    entity.LastName,
                    entity.Bio
                };

                await _sqliteData.SaveData(
                    "INSERT INTO Authors (FirstName, LastName, Bio) VALUES (@FirstName, @LastName, @Bio);",
                    author, connectionString);

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
            try
            {
                await _sqliteData.SaveData("DELETE FROM Authors WHERE Id = @Id", new { entity.Id }, connectionString);

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
            try
            {
                var authors = await _sqliteData.LoadData<Author, dynamic>("SELECT * FROM Authors", new { }, connectionString);

                return authors.ToList();
            }
            catch (Exception ex)
            {
                _logger.LogError($"{ex.Message} - {ex.InnerException}");

                return null;
            }
            
        }

        public async Task<Author> FindById(int id)
        {
            try
            {
                var author = await _sqliteData.LoadData<Author, dynamic>("SELECT * FROM People Where Id = @Id", new { Id = id }, connectionString);

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
            try
            {
                await _sqliteData.SaveData("UPDATE Authors SET FirstName = @FirstName," +
                " LastName = @LastName, Bio = @Bio" +
                " WHERE Id = @Id", entity, connectionString);

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError($"{ex.Message} - {ex.InnerException}");

                return false;
            }    
        }

        public Task<bool> SaveAll()
        {
            throw new NotImplementedException();
        }

        public Task<bool> IsExists(int id)
        {
            throw new NotImplementedException();
        }
    }
}
