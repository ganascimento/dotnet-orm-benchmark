using System.Text.Json;
using Dotnet.Orm.Benchmark.Interface.Repository;
using Dotnet.Orm.Benchmark.Model.Dto;

namespace Dotnet.Orm.Benchmark.Infra.API;

public class CarApiRepository : ICarApiRepository
{
    private static readonly HttpClient _client = new HttpClient();

    public CarApiRepository()
    {
        _client.BaseAddress = new Uri("https://parallelum.com.br/fipe/api/v1/carros/");
    }

    public async Task<List<BrandDto>?> GetBrandAsync()
    {
        var result = await _client.GetAsync("marcas");

        if (result.IsSuccessStatusCode)
        {
            var resultData = await result.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<List<BrandDto>>(resultData, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
        }

        return null;
    }

    public async Task<List<ModelDto>?> GetModelAsync(string brandCode)
    {
        var result = await _client.GetAsync($"marcas/{brandCode}/modelos");

        if (result.IsSuccessStatusCode)
        {
            var resultData = await result.Content.ReadAsStringAsync();
            var data = JsonSerializer.Deserialize<ModalDataDto>(resultData, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            return data?.Modelos;
        }

        return null;
    }

    public async Task<ModelInfoDto?> GetModelInfoAsync(string brandCode, int modelCode, string yearCode)
    {
        var result = await _client.GetAsync($"marcas/{brandCode}/modelos/{modelCode}/anos/{yearCode}");

        if (result.IsSuccessStatusCode)
        {
            var resultData = await result.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<ModelInfoDto>(resultData, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
        }

        return null;
    }

    public async Task<List<ModelYearDto>?> GetModelYearAsync(string brandCode, int modelCode)
    {
        var result = await _client.GetAsync($"marcas/{brandCode}/modelos/{modelCode}/anos");

        if (result.IsSuccessStatusCode)
        {
            var resultData = await result.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<List<ModelYearDto>>(resultData, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
        }

        return null;
    }
}