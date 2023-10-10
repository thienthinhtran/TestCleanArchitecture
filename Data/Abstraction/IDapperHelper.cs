using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Abstraction
{
    public interface IDapperHelper
    {
        Task ExecuteNotReturnAsync(string query, DynamicParameters? parameters = null, IDbTransaction? dbTransaction = null);
        Task<T> ExecuteScalarAsync<T>(string query, DynamicParameters? parameters = null);
        Task<IEnumerable<T>> ExecuteSqlReturnList<T>(string query, DynamicParameters? parameters = null);
        Task<T> ExecuteSqlReturnSingle<T>(string query, DynamicParameters? parameters = null);
        Task<IEnumerable<T>> ExecuteStoreProcedureReturnList<T>(string query, DynamicParameters? parameters = null);

    }
}
