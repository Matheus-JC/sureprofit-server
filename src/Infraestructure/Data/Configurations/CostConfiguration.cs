using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SureProfit.Domain.Entities;

namespace SureProfit.Infraestructure.Data.Configurations;

public class CostConfiguration : IEntityTypeConfiguration<Cost>
{
    public void Configure(EntityTypeBuilder<Cost> builder)
    {
        builder.ToTable("Costs");

        builder.HasKey(c => c.Id);

        builder.Property(c => c.Description)
            .HasColumnType("varchar(50)")
            .IsRequired();

        builder.Property(c => c.Value)
            .HasColumnType("decimal")
            .HasPrecision(18, 2)
            .IsRequired();

        builder.Property(c => c.Type)
            .HasConversion<int>()
            .IsRequired();

        builder.HasOne(c => c.Store)
            .WithMany(c => c.Costs)
            .HasForeignKey("StoreId")
            .IsRequired();

        builder.HasOne(c => c.Tag)
            .WithMany()
            .HasForeignKey("TagId");
    }
}
