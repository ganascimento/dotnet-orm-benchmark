using Dotnet.Orm.Benchmark.Model.Entity;
using Dotnet.Orm.Benchmark.Infra.EFCore.Context;
using Dotnet.Orm.Benchmark.Interface.Repository;
using Microsoft.EntityFrameworkCore;

namespace Dotnet.Orm.Benchmark.Infra.EFCore;

public class CarRepository : ICarRepository
{
    private readonly DataContext _context;
    private readonly DbSet<CarEntity> _dataset;

    public CarRepository(DataContext context)
    {
        _context = context;
        _dataset = context.Set<CarEntity>();
    }

    public async Task<CarEntity?> GetByIdAsync(int id)
    {
        return await _dataset
            .FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<List<CarEntity>> GetAllAsync()
    {
        return await _dataset.ToListAsync();
    }

    public async Task<List<CarEntity>> GetByBrandAsync(string brand)
    {
        return await _dataset
            .Where(x => x.Brand == brand)
            .ToListAsync();
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