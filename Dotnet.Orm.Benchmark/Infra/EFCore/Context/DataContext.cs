using Dotnet.Orm.Benchmark.Model.Entity;
using Dotnet.Orm.Benchmark.Infra.EFCore.Mapping;
using Microsoft.EntityFrameworkCore;

namespace Dotnet.Orm.Benchmark.Infra.EFCore.Context;

public class DataContext : DbContext
{
    public DbSet<CarEntity>? Car { get; set; }

    public DataContext() { }
    public DataContext(DbContextOptions<DataContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<CarEntity>(new CarMapping().Configure);
    }
}