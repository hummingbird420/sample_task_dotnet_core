namespace SampleTaskApp.Repositories
{
    using Dapper;
    using SampleTaskApp.IRepositories;
    using System.Collections.Generic;
    using System.Data;
    using System.Linq;
    using System.Threading.Tasks;

    public class DapperRepository<T> : IDapperRepository<T> where T : class
    {
        private readonly IDbConnection _dbConnection;

        public DapperRepository(IDbConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }

        // DapperGetAllAsync: Get all entities from the table.
        public async Task<IEnumerable<T>> DapperGetAllAsync()
        {
            var tableName = typeof(T).Name + "s"; // Assumes table name is plural
            var sql = $"SELECT * FROM {tableName}";
            return await _dbConnection.QueryAsync<T>(sql);
        }

        // DapperGetByIdAsync: Get an entity by ID.
        public async Task<T> DapperGetByIdAsync(int id)
        {
            var tableName = typeof(T).Name + "s"; // Assumes table name is plural
            var sql = $"SELECT * FROM {tableName} WHERE Id = @Id";
            return await _dbConnection.QuerySingleOrDefaultAsync<T>(sql, new { Id = id });
        }

        // DapperAddAsync: Add a new entity to the table.
        public async Task DapperAddAsync(T entity)
        {
            var tableName = typeof(T).Name + "s"; // Assumes table name is plural
            var properties = typeof(T).GetProperties();
            var columns = string.Join(", ", properties.Select(p => p.Name));
            var parameters = string.Join(", ", properties.Select(p => "@" + p.Name));

            var sql = $"INSERT INTO {tableName} ({columns}) VALUES ({parameters})";

            await _dbConnection.ExecuteAsync(sql, entity);
        }

        // DapperUpdateAsync: Update an existing entity in the table.
        public async Task DapperUpdateAsync(T entity)
        {
            var tableName = typeof(T).Name + "s"; // Assumes table name is plural
            var properties = typeof(T).GetProperties();
            var setClause = string.Join(", ", properties.Select(p => $"{p.Name} = @{p.Name}"));

            var sql = $"UPDATE {tableName} SET {setClause} WHERE Id = @Id";

            await _dbConnection.ExecuteAsync(sql, entity);
        }

        // DapperDeleteAsync: Delete an entity by ID from the table.
        public async Task DapperDeleteAsync(int id)
        {
            var tableName = typeof(T).Name + "s"; // Assumes table name is plural
            var sql = $"DELETE FROM {tableName} WHERE Id = @Id";
            await _dbConnection.ExecuteAsync(sql, new { Id = id });
        }
    }

}
