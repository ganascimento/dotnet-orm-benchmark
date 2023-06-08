using System.Diagnostics;
using Dotnet.Orm.Benchmark.Infra.EFCore.Context;
using Dotnet.Orm.Benchmark.Interface.Repository;
using Dotnet.Orm.Benchmark.Model.Dto;
using Dotnet.Orm.Benchmark.Model.Entity;

namespace Dotnet.Orm.Benchmark.Service;

public class AppService
{
    private readonly ICarRepository _carRepository;
    private readonly ICarApiRepository _carApiRepository;
    private readonly Stopwatch _stopwatch = new Stopwatch();
    private readonly List<string> Brands = new List<string> { "Subaru" };
    private readonly DataContext _context;

    public AppService(ICarRepository carRepository, ICarApiRepository carApiRepository, DataContext context)
    {
        _carRepository = carRepository;
        _carApiRepository = carApiRepository;
        _context = context;
    }

    public async Task GetByIdAsync()
    {
        _stopwatch.Reset();
        _stopwatch.Start();

        await _carRepository.GetByIdAsync(1);
        await _carRepository.GetByIdAsync(2);
        await _carRepository.GetByIdAsync(3);
        await _carRepository.GetByIdAsync(4);
        await _carRepository.GetByIdAsync(5);

        _stopwatch.Stop();

        Console.WriteLine($"Time: {_stopwatch.ElapsedMilliseconds} ms");
    }

    public async Task GetAllAsync()
    {
        _stopwatch.Reset();
        _stopwatch.Start();

        await _carRepository.GetAllAsync();
        await _carRepository.GetAllAsync();
        await _carRepository.GetAllAsync();
        await _carRepository.GetAllAsync();
        await _carRepository.GetAllAsync();

        _stopwatch.Stop();

        Console.WriteLine($"Time: {_stopwatch.ElapsedMilliseconds} ms");
    }

    public async Task GetByBrandAsync()
    {
        var brand = Brands.First();
        _stopwatch.Reset();
        _stopwatch.Start();

        await _carRepository.GetByBrandAsync(brand);
        await _carRepository.GetByBrandAsync(brand);
        await _carRepository.GetByBrandAsync(brand);
        await _carRepository.GetByBrandAsync(brand);
        await _carRepository.GetByBrandAsync(brand);

        _stopwatch.Stop();

        Console.WriteLine($"Time: {_stopwatch.ElapsedMilliseconds} ms");
    }

    public async Task InsertAsync()
    {
        var carData = await GetData();
        _stopwatch.Reset();
        _stopwatch.Start();

        foreach (var car in carData)
        {
            await _carRepository.InsertAsync(car);
            await _carRepository.InsertAsync(car);
            await _carRepository.InsertAsync(car);
            await _carRepository.InsertAsync(car);
            await _carRepository.InsertAsync(car);
            await _carRepository.InsertAsync(car);
            await _carRepository.InsertAsync(car);
            await _carRepository.InsertAsync(car);
            await _carRepository.InsertAsync(car);
            await _carRepository.InsertAsync(car);
            await _carRepository.InsertAsync(car);
            await _carRepository.InsertAsync(car);
        }

        //await _context.SaveChangesAsync();

        _stopwatch.Stop();

        Console.WriteLine($"Time: {_stopwatch.ElapsedMilliseconds} ms");
    }

    public async Task RemoveAllAsync()
    {
        await _carRepository.RemoveAllAsync();
    }

    private async Task<List<CarEntity>> GetData()
    {
        var brands = await _carApiRepository.GetBrandAsync();
        if (brands == null) throw new Exception("Brand not found!");

        var brandCodes = brands
            .Where(brand => Brands.Contains(brand.Nome))
            .Select(brand => brand.Codigo);

        var modelsInfo = new List<ModelInfoDto>();

        foreach (var brandCode in brandCodes)
        {
            var models = await _carApiRepository.GetModelAsync(brandCode);
            if (models == null) throw new Exception("Model not found!");

            foreach (var model in models)
            {
                var modelYears = await _carApiRepository.GetModelYearAsync(brandCode, model.Codigo);
                if (modelYears == null) throw new Exception("Model year not found!");

                var info = await _carApiRepository.GetModelInfoAsync(brandCode, model.Codigo, modelYears.Last().Codigo);
                if (info == null) throw new Exception("Info not found!");

                modelsInfo.Add(info);
            }
        }

        var carsEntity = new List<CarEntity>();

        modelsInfo.ForEach(info =>
        {
            carsEntity.Add(new CarEntity
            {
                Model = info.Modelo,
                Brand = info.Marca,
                Fuel = info.Combustivel,
                Value = info.Valor,
                Year = info.AnoModelo
            });
        });

        return carsEntity;
    }
}