using Dotnet.Orm.Benchmark.Model.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Dotnet.Orm.Benchmark.Infra.EFCore.Mapping;

public class CarMapping : IEntityTypeConfiguration<CarEntity>
{
    public void Configure(EntityTypeBuilder<CarEntity> builder)
    {
        builder.ToTable("TB_CAR");

        builder.HasKey(k => k.Id);

        builder.Property(p => p.Brand)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(p => p.Fuel)
            .IsRequired()
            .HasMaxLength(20);

        builder.Property(p => p.Model)
            .IsRequired()
            .HasMaxLength(120);

        builder.Property(p => p.Value)
            .IsRequired();

        builder.Property(p => p.Year)
            .IsRequired();
    }
}