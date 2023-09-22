using Dapper;
using Data.Abstraction;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Data
{
    public class DapperHelper : IDapperHelper
    {
        private readonly string _configuration;
        private readonly IDbConnection _dbConnection;

        public DapperHelper(IConfiguration configuration)
        {
            _configuration = configuration.GetConnectionString("DbInit");
            _dbConnection = new SqlConnection(_configuration);
        }
        public async Task ExecuteNotReturnAsync(string query, DynamicParameters parameters = null)
        {
            // mở kết nối DB
            using (var dbConnection = new SqlConnection(_configuration))
            {
                await dbConnection.ExecuteAsync(query, parameters, commandType: CommandType.Text);
            }
        }

       /* public async Task<T> ExecuteScalarAsync<T>(string query, DynamicParameters parameters = null)
        {
            using (var dbConnection = new SqlConnection(_configuration))
            {
                await dbConnection.OpenAsync();
                return await dbConnection.ExecuteScalarAsync<T>(query, parameters);
            }
        }*/
        public async Task<T> ExecuteScalarAsync<T>(string query, DynamicParameters parameters = null)
        {
            using (var dbConnection = new SqlConnection(_configuration))
            {
                await dbConnection.OpenAsync();
                return await dbConnection.ExecuteScalarAsync<T>(query, parameters);
            }
        }
        // For get by Id.
        public async Task<T> ExecuteSqlReturnSingle<T>(string query, DynamicParameters parameters = null)
        {
            using (var dbConnection = new SqlConnection(_configuration))
            {
                await dbConnection.OpenAsync();
                return await dbConnection.QuerySingleOrDefaultAsync<T>(query, parameters);
            }
        }

        public async Task<IEnumerable<T>> ExecuteSqlReturnList<T>(string query, DynamicParameters parameters = null)
        {
            using (var dbConnection = new SqlConnection(_configuration))
            {
                return await dbConnection.QueryAsync<T>(query, parameters, commandType: CommandType.Text);
            }
        }

        public async Task<IEnumerable<T>> ExecuteStoreProcedureReturnList<T>(string query, DynamicParameters parameters = null)
        {
            using (var dbConnection = new SqlConnection(_configuration))
            {
                return await dbConnection.QueryAsync<T>(query, parameters, commandType: CommandType.StoredProcedure);
            }
        }
    }
}
