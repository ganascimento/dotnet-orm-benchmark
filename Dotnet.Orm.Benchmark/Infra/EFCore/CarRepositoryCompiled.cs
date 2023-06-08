using Dotnet.Orm.Benchmark.Model.Entity;
using Dotnet.Orm.Benchmark.Infra.EFCore.Context;
using Dotnet.Orm.Benchmark.Interface.Repository;
using Microsoft.EntityFrameworkCore;

namespace Dotnet.Orm.Benchmark.Infra.EFCore;

public class CarRepositoryCompiled : ICarRepository
{
    private static readonly Func<DataContext, int, Task<CarEntity?>> GetByIdQuery = EF.CompileAsyncQuery(
        (DataContext context, int id) => context.Set<CarEntity>().FirstOrDefault(x => x.Id == id));

    private static readonly Func<DataContext, List<CarEntity>> GetAllQuery = EF.CompileQuery(
        (DataContext context) => context.Set<CarEntity>().ToList());

    private static readonly Func<DataContext, string, IEnumerable<CarEntity>> GetByBrandQuery = EF.CompileQuery(
        (DataContext context, string brand) => context.Set<CarEntity>().Where(x => x.Brand == brand));

    private readonly DataContext _context;
    private readonly DbSet<CarEntity> _dataset;

    public CarRepositoryCompiled(DataContext context)
    {
        _context = context;
        _dataset = context.Set<CarEntity>();
    }

    public async Task<CarEntity?> GetByIdAsync(int id)
    {
        return await GetByIdQuery(_context, id);
    }

    public async Task<List<CarEntity>> GetAllAsync()
    {
        return await Task.FromResult(GetAllQuery(_context));
    }

    public async Task<List<CarEntity>> GetByBrandAsync(string brand)
    {
        return await Task.FromResult(GetByBrandQuery(_context, brand).ToList());
    }

    public async Task InsertAsync(CarEntity entity)
    {
        await _dataset.AddAsync(new CarEntity
        {
            Brand = entity.Brand,
            Fuel = entity.Fuel,
            Model = entity.Model,
            Value = entity.Value,
            Year = entity.Year
        });
        await _context.SaveChangesAsync();
    }

    public async Task RemoveAllAsync()
    {
        await _context.Database.ExecuteSqlRawAsync("TRUNCATE TABLE TB_CAR");
    }
}