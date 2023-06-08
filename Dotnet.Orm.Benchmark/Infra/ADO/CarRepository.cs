using Dotnet.Orm.Benchmark.Model.Entity;
using Dotnet.Orm.Benchmark.Interface.Repository;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;

namespace Dotnet.Orm.Benchmark.Infra.ADO;

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
        CarEntity? car = null;

        using (var connection = new MySqlConnection(_connectionString))
        {
            await connection.OpenAsync();
            var command = new MySqlCommand(query, connection);
            command.Parameters.AddWithValue("@Id", id);

            var reader = await command.ExecuteReaderAsync();

            while (reader.Read())
            {
                car = new CarEntity
                {
                    Id = int.Parse(reader[0].ToString() ?? "0"),
                    Model = reader[1].ToString() ?? "",
                    Brand = reader[2].ToString() ?? "",
                    Fuel = reader[3].ToString() ?? "",
                    Year = int.Parse(reader[4].ToString() ?? ""),
                    Value = reader[5].ToString() ?? "",
                };
            }
        }

        return car;
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
        var cars = new List<CarEntity>();

        using (var connection = new MySqlConnection(_connectionString))
        {
            await connection.OpenAsync();
            var command = new MySqlCommand(query, connection);

            var reader = await command.ExecuteReaderAsync();

            while (reader.Read())
            {
                var car = new CarEntity
                {
                    Id = int.Parse(reader[0].ToString() ?? "0"),
                    Model = reader[1].ToString() ?? "",
                    Brand = reader[2].ToString() ?? "",
                    Fuel = reader[3].ToString() ?? "",
                    Year = int.Parse(reader[4].ToString() ?? ""),
                    Value = reader[5].ToString() ?? "",
                };

                cars.Add(car);
            }
        }

        return cars;
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
        var cars = new List<CarEntity>();

        using (var connection = new MySqlConnection(_connectionString))
        {
            await connection.OpenAsync();
            var command = new MySqlCommand(query, connection);
            command.Parameters.AddWithValue("@Brand", brand);

            var reader = await command.ExecuteReaderAsync();

            while (reader.Read())
            {
                var car = new CarEntity
                {
                    Id = int.Parse(reader[0].ToString() ?? "0"),
                    Model = reader[1].ToString() ?? "",
                    Brand = reader[2].ToString() ?? "",
                    Fuel = reader[3].ToString() ?? "",
                    Year = int.Parse(reader[4].ToString() ?? ""),
                    Value = reader[5].ToString() ?? "",
                };

                cars.Add(car);
            }
        }

        return cars;
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

        using (var connection = new MySqlConnection(_connectionString))
        {
            await connection.OpenAsync();
            var command = new MySqlCommand(query, connection);
            command.Parameters.AddWithValue("@Model", entity.Model);
            command.Parameters.AddWithValue("@Brand", entity.Brand);
            command.Parameters.AddWithValue("@Fuel", entity.Fuel);
            command.Parameters.AddWithValue("@Year", entity.Year);
            command.Parameters.AddWithValue("@Value", entity.Value);


            var result = await command.ExecuteNonQueryAsync();

            if (result < 1)
                throw new Exception("Erro on execute query");
        }
    }

    public async Task RemoveAllAsync()
    {
        var query = @"TRUNCATE TABLE TB_CAR";

        using (var connection = new MySqlConnection(_connectionString))
        {
            await connection.OpenAsync();
            var command = new MySqlCommand(query, connection);

            await command.ExecuteNonQueryAsync();
        }
    }
}
