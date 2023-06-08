namespace Dotnet.Orm.Benchmark.Model.Dto;

public class ModalDataDto
{
    public required List<ModelDto> Modelos { get; set; }
}

public class ModelDto
{
    public required int Codigo { get; set; }
    public required string Nome { get; set; }
}