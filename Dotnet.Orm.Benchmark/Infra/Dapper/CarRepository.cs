using Dotnet.Orm.Benchmark.Model.Entity;
using Dotnet.Orm.Benchmark.Interface.Repository;
using Dapper;
using System.Data;
using MySql.Data.MySqlClient;
using Microsoft.Extensions.Configuration;

namespace Dotnet.Orm.Benchmark.Infra.Dapper;

public class CarRepository : ICarRepository
{
    private readonly string _connectionString;

    public CarRepository(IConfiguration configuration)
    {
        _connectionString = configuration["ConnectionString"]!;
    }

    public async Task<CarEntity?> GetByIdAsync(int id)
    {
        var query = @"
            SELECT  Id,
                    Model,
                    Brand,
                    Fuel,
                    Year,
                    Value
            FROM    TB_CAR
            WHERE   Id = @Id
        ";

        var parameters = new DynamicParameters();
        parameters.Add("@Id", id, DbType.Int32, ParameterDirection.Input);

        using (var connection = new MySqlConnection(_connectionString))
        {
            connection.Open();

            return await connection.QueryFirstOrDefaultAsync<CarEntity>(query, parameters);
        }
    }

    public async Task<List<CarEntity>> GetAllAsync()
    {
        var query = @"
            SELECT  Id,
                    Model,
                    Brand,
                    Fuel,
                    Year,
                    Value
            FROM    TB_CAR
        ";

        using (var connection = new MySqlConnection(_connectionString))
        {
            connection.Open();

            var cars = await connection.QueryAsync<CarEntity>(query);

            return cars.ToList();
        }
    }

    public async Task<List<CarEntity>> GetByBrandAsync(string brand)
    {
        var query = @"
            SELECT  Id,
                    Model,
                    Brand,
                    Fuel,
                    Year,
                    Value
            FROM    TB_CAR
            WHERE   Brand = @Brand
        ";

        var parameters = new DynamicParameters();
        parameters.Add("@Brand", brand, DbType.String, ParameterDirection.Input);

        using (var connection = new MySqlConnection(_connectionString))
        {
            connection.Open();

            var cars = await connection.QueryAsync<CarEntity>(query, parameters);

            return cars.ToList();
        }
    }

    public async Task InsertAsync(CarEntity entity)
    {
        var query = @"
            INSERT  INTO TB_CAR
            (
                Model,
                Brand,
                Fuel,
                Year,
                Value
            )
            VALUES
            (
                @Model,
                @Brand,
                @Fuel,
                @Year,
                @Value
            )
        ";

        var parameters = new DynamicParameters();
        parameters.Add("@Model", entity.Model, DbType.String, ParameterDirection.Input);
        parameters.Add("@Brand", entity.Brand, DbType.String, ParameterDirection.Input);
        parameters.Add("@Fuel", entity.Fuel, DbType.String, ParameterDirection.Input);
        parameters.Add("@Year", entity.Year, DbType.Int32, ParameterDirection.Input);
        parameters.Add("@Value", entity.Value, DbType.String, ParameterDirection.Input);

        using (var connection = new MySqlConnection(_connectionString))
        {
            connection.Open();

            var result = await connection.ExecuteAsync(query, parameters);

            if (result < 1)
                throw new Exception("Erro on execute query");
        }
    }

    public async Task RemoveAllAsync()
    {
        var query = @"TRUNCATE TABLE TB_CAR";

        using (var connection = new MySqlConnection(_connectionString))
        {
            connection.Open();

            await connection.ExecuteAsync(query);
        }
    }
}