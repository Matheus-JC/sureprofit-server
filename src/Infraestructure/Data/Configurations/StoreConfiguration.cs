using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SureProfit.Domain.Entities;

namespace SureProfit.Infra.Data;

public class StoreConfiguration : IEntityTypeConfiguration<Store>
{
    public void Configure(EntityTypeBuilder<Store> builder)
    {
        builder.ToTable("Stores");

        builder.HasKey(s => s.Id);

        builder.Property(s => s.Name)
            .HasColumnType("varchar(250)")
            .IsRequired();

        builder.Property(c => c.TargetProfit)
            .HasColumnType("decimal")
            .HasPrecision(18, 2);

        builder.Property(c => c.PerItemFee)
            .HasColumnType("decimal")
            .HasPrecision(18, 2);

        builder.Property(s => s.Enviroment)
            .HasConversion<int>()
            .IsRequired();

        builder.HasOne(s => s.Company)
            .WithMany(c => c.Stores)
            .HasForeignKey(s => s.CompanyId);

        builder.HasMany(s => s.Costs)
            .WithOne(c => c.Store);
    }
}
