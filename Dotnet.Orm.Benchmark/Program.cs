using Dotnet.Orm.Benchmark.Infra.API;
using Dotnet.Orm.Benchmark.Infra.EFCore.Context;
using Dotnet.Orm.Benchmark.Interface.Repository;
using Dotnet.Orm.Benchmark.Service;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

var services = new ServiceCollection();
var builder = new ConfigurationBuilder()
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
    .Build();

services.AddDbContext<DataContext>(
    options => options.UseMySql(
        builder["ConnectionString"],
        new MySqlServerVersion(new Version(5, 7)),
        options => options.EnableRetryOnFailure()
    )
);
services.AddSingleton<IConfiguration>(builder);
services.AddScoped<ICarApiRepository, CarApiRepository>();
//services.AddScoped<ICarRepository, Dotnet.Orm.Benchmark.Infra.ADO.CarRepository>();
//services.AddScoped<ICarRepository, Dotnet.Orm.Benchmark.Infra.Dapper.CarRepository>();
//services.AddScoped<ICarRepository, Dotnet.Orm.Benchmark.Infra.EFCore.CarRepository>();
//services.AddScoped<ICarRepository, Dotnet.Orm.Benchmark.Infra.EFCore.CarRepositoryNoTrack>();
services.AddScoped<ICarRepository, Dotnet.Orm.Benchmark.Infra.EFCore.CarRepositoryCompiled>();
services.AddSingleton<AppService>();

var provider = services.BuildServiceProvider();

var service = provider.GetService<AppService>();

if (service != null)
{
    await service.InsertAsync();
    await service.GetByIdAsync();
    await service.GetByIdAsync();
    await service.GetAllAsync();
    await service.GetByBrandAsync();
    await service.RemoveAllAsync();
}