using Dapper;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using System;

namespace DataLayer
{
    public class ProductRepository : IProductRepository
    {
        private readonly string _connectionString;

        public ProductRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task<List<T>> LoadData<T, U>(string sql, U parameters)
        {
            using (IDbConnection connection = new MySqlConnection(_connectionString))
            {
                var rows = await connection.QueryAsync<T>(sql, parameters);
                return rows.ToList();
            }
        }

        public Task SaveData<T>(string sql, T parameters)
        {
            using (IDbConnection connection = new MySqlConnection(_connectionString))
            {
                // Log SQL command and parameters for debugging
                Console.WriteLine($"Executing SQL: {sql}");
                Console.WriteLine($"Parameters: {parameters}");

                return connection.ExecuteAsync(sql, parameters);
            }
        }

    }
}
