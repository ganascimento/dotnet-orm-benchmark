namespace Dotnet.Orm.Benchmark.Model.Entity;

public class CarEntity
{
    public int? Id { get; set; }
    public required string Model { get; set; }
    public required string Brand { get; set; }
    public required string Fuel { get; set; }
    public required int Year { get; set; }
    public required string Value { get; set; }
}