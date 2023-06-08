using Dotnet.Orm.Benchmark.Model.Dto;

namespace Dotnet.Orm.Benchmark.Interface.Repository;

public interface ICarApiRepository
{
    Task<List<BrandDto>?> GetBrandAsync();
    Task<List<ModelDto>?> GetModelAsync(string brandCode);
    Task<List<ModelYearDto>?> GetModelYearAsync(string brandCode, int modelCode);
    Task<ModelInfoDto?> GetModelInfoAsync(string brandCode, int modelCode, string yearCode);
}