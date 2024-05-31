using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SureProfit.Domain.Entities;

namespace SureProfit.Infra.Data;

public class CompanyConfiguration : IEntityTypeConfiguration<Company>
{
    public void Configure(EntityTypeBuilder<Company> builder)
    {
        builder.ToTable("Companies");

        builder.HasKey(c => c.Id);

        builder.Property(c => c.Name)
            .HasColumnType("varchar(250)")
            .IsRequired();

        builder.OwnsOne(c => c.Cnpj, cnpj =>
            cnpj.Property(c => c.Value)
            .HasColumnName("Cnpj"));

        builder.HasMany(c => c.Stores)
            .WithOne(c => c.Company);
    }
}
