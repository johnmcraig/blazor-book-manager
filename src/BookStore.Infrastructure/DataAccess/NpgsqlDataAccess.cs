using BookStore.Core.Interfaces;
using Dapper;
using Microsoft.Extensions.Configuration;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Infrastructure.DataAccess
{
    public class NpgsqlDataAccess : ISqlDataAccess
    {
        private readonly IConfiguration _config;

        public NpgsqlDataAccess(IConfiguration config)
        {
            _config = config;
        }

        public async Task<List<T>> LoadData<T, U>(string sql, U parameters, string connectionStringName)
        {
            string connectionString = _config.GetConnectionString(connectionStringName);

            using (IDbConnection connection = new NpgsqlConnection(connectionString))
            {
                var rows = await connection.QueryAsync<T>(sql, parameters, commandType: CommandType.Text);

                return rows.ToList();
            }
        }

        public async Task SaveData<T>(string sql, T parameters, string connectionStringName)
        {
            string connectionString = _config.GetConnectionString(connectionStringName);

            using (IDbConnection connection = new NpgsqlConnection(connectionString))
            {
                await connection.ExecuteAsync(sql, parameters, commandType: CommandType.Text);
            }
        }
    }
}
