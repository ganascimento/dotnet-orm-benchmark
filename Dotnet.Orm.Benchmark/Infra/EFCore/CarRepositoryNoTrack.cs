using Dotnet.Orm.Benchmark.Model.Entity;
using Dotnet.Orm.Benchmark.Infra.EFCore.Context;
using Dotnet.Orm.Benchmark.Interface.Repository;
using Microsoft.EntityFrameworkCore;

namespace Dotnet.Orm.Benchmark.Infra.EFCore;

public class CarRepositoryNoTrack : ICarRepository
{
    private readonly DataContext _context;
    private readonly DbSet<CarEntity> _dataset;

    public CarRepositoryNoTrack(DataContext context)
    {
        _context = context;
        _dataset = context.Set<CarEntity>();
    }


    public async Task<CarEntity?> GetByIdAsync(int id)
    {
        return await _dataset
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<List<CarEntity>> GetAllAsync()
    {
        return await _dataset
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<List<CarEntity>> GetByBrandAsync(string brand)
    {
        return await _dataset
            .Where(x => x.Brand == brand)
            .AsNoTracking()
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