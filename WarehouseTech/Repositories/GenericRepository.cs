using Dapper;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using WarehouseTech.Repositories.Interfaces;
using WarehouseTech.Repositories;

public class GenericRepository<T> : IRepository<T>
{
    protected readonly DatabaseConnection _databaseConnection;

    public GenericRepository(DatabaseConnection databaseConnection)
    {
        _databaseConnection = databaseConnection;
    }

    // Предполагается, что имя таблицы – имя модели в нижнем регистре с окончанием "s".
    protected virtual string TableName => typeof(T).Name.ToLower() + "s";

    public async Task<IEnumerable<T>> GetAllAsync()
    {
        using (var connection = _databaseConnection.GetConnection())
        {
            string query = $"SELECT * FROM {TableName}";
            return await connection.QueryAsync<T>(query);
        }
    }

    public async Task<T> GetByIdAsync(int id)
    {
        using (var connection = _databaseConnection.GetConnection())
        {
            string query = $"SELECT * FROM {TableName} WHERE id = @Id";
            return await connection.QuerySingleOrDefaultAsync<T>(query, new { Id = id });
        }
    }

    public async Task<int> AddAsync(T entity)
    {
        // Генерация SQL для вставки (игнорируя свойство "Id", если оно есть)
        var properties = typeof(T).GetProperties();
        var columnNames = new List<string>();
        var paramNames = new List<string>();
        var parameters = new DynamicParameters();

        foreach (var prop in properties)
        {
            if (string.Equals(prop.Name, "Id", StringComparison.OrdinalIgnoreCase))
                continue;

            // Предполагаем, что имя столбца соответствует имени свойства в нижнем регистре
            columnNames.Add(prop.Name.ToLower());
            paramNames.Add("@" + prop.Name);
            parameters.Add("@" + prop.Name, prop.GetValue(entity));
        }

        var query = $"INSERT INTO {TableName} ({string.Join(", ", columnNames)}) " +
                    $"VALUES ({string.Join(", ", paramNames)}) RETURNING id";

        using (var connection = _databaseConnection.GetConnection())
        {
            return await connection.QuerySingleAsync<int>(query, parameters);
        }
    }

    public async Task<bool> UpdateAsync(T entity)
    {
        // Предполагается, что сущность имеет свойство "Id"
        var idProp = typeof(T).GetProperty("Id");
        if (idProp == null)
            throw new Exception("Сущность не имеет свойства 'Id'");

        var id = idProp.GetValue(entity);
        if (id == null)
            throw new Exception("Значение Id не может быть null");

        var properties = typeof(T).GetProperties();
        var setClauses = new List<string>();
        var parameters = new DynamicParameters();

        foreach (var prop in properties)
        {
            if (string.Equals(prop.Name, "Id", StringComparison.OrdinalIgnoreCase))
                continue;

            setClauses.Add($"{prop.Name.ToLower()} = @{prop.Name}");
            parameters.Add("@" + prop.Name, prop.GetValue(entity));
        }
        parameters.Add("@Id", id);

        var query = $"UPDATE {TableName} SET {string.Join(", ", setClauses)} WHERE id = @Id";
        using (var connection = _databaseConnection.GetConnection())
        {
            var affected = await connection.ExecuteAsync(query, parameters);
            return affected > 0;
        }
    }

    public async Task<bool> DeleteAsync(int id)
    {
        using (var connection = _databaseConnection.GetConnection())
        {
            var query = $"DELETE FROM {TableName} WHERE id = @Id";
            var affected = await connection.ExecuteAsync(query, new { Id = id });
            return affected > 0;
        }
    }
}