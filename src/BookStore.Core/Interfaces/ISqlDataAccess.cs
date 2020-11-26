using System.Collections.Generic;
using System.Threading.Tasks;

namespace BookStore.Core.Interfaces
{
    public interface ISqlDataAccess
    {
        Task<List<T>> LoadData<T, U>(string sql, U parameters, string connectionStringName);

        Task SaveData<T>(string sql, T parameters, string connectionStringName);
    }
}
