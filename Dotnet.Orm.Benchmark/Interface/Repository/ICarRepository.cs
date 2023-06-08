using Dotnet.Orm.Benchmark.Model.Entity;

namespace Dotnet.Orm.Benchmark.Interface.Repository;

public interface ICarRepository
{
    Task<CarEntity?> GetByIdAsync(int id);
    Task<List<CarEntity>> GetAllAsync();
    Task<List<CarEntity>> GetByBrandAsync(string brand);
    Task InsertAsync(CarEntity entity);
    Task RemoveAllAsync();
}