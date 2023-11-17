﻿using System.Data.Common;
using System.Data.SqlClient;
using Dapper;
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;
using System.Data;



namespace InsuranceManagement
{
    /// <summary>
    /// The database representational model for the application
    /// This class will handle database connections
    /// </summary>
    public class DapperDbContext
    {
        private readonly IConfiguration _configuration;

        /// <summary>
        /// Default constructor, expecting inject IConfiguration for accessing configuration settings
        /// </summary>
        /// <param name="configuration">configuration settings</param>
        public DapperDbContext(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        /// <summary>
        /// Private method to create a new IDbConnection using the configured connection string
        /// </summary>
        /// <returns></returns>
        private DbConnection CreateConnection()
        {
            // Using SqlConnection for SQL Server,
            return new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));

        }

        /// <summary>
        /// Generic method for executing a single-row query asynchronously
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sql"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public async Task<T?> QuerySingleAsync<T>(string sql, object? parameters = null)
        {
            using var connection = CreateConnection(); // Creating a new database connection
            await connection.OpenAsync(); // Opening the connection asynchronously

            // Using Dapper's QuerySingleOrDefaultAsync to execute the query asynchronously and return a single result
            return await connection.QuerySingleOrDefaultAsync<T>(sql, parameters);
            
        }

        /// <summary>
        /// Generic method for executing a Multiple-row query asynchronously
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sql"></param>
        /// <param name="parameters"></param>
        /// <returns>returns multiple results</returns>
        public async Task<IEnumerable<T>> QueryMultipleAsync<T>(string sql, object? parameters = null)
        {
            using var connection = CreateConnection();
            await connection.OpenAsync();

            return await connection.QueryAsync<T>(sql, parameters);
        }

        /// <summary>
        /// Method for executing queries with transactions
        /// </summary>
        /// <param name="action"></param>
        /// <returns></returns>
        public async Task ExecuteTransactionAsync(Func<IDbConnection, IDbTransaction, Task> action)
        {
            using var connection = CreateConnection();
            await connection.OpenAsync();

            using var transaction = connection.BeginTransaction();
            try
            {
                await action(connection, transaction);
                transaction.Commit();
            }
            catch
            {
                transaction.Rollback();
                throw; // Re-throw the exception after rolling back the transaction
            }
        }
    }
}
