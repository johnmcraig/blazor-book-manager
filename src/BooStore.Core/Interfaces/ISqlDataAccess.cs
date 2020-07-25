using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BooStore.Core.Interfaces
{
    public interface ISqlDataAccess
    {
        Task<List<T>> LoadData<T, U>(string sql, U parameters, string connectionString);

        Task SaveData<T>(string sql, T parameters, string connectionString);
    }
}
