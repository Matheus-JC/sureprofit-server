using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace SureProfit.Infraestructure.Identity;

public class ApiIdentityDbContext(DbContextOptions<ApiIdentityDbContext> options) : IdentityDbContext<ApplicationUser>(options)
{
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        string schemaName = "security";

        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<ApplicationUser>().ToTable("Users", schemaName).HasData(InitialData.GetAdminUsers());
        modelBuilder.Entity<IdentityRole>().ToTable("Roles", schemaName);
        modelBuilder.Entity<IdentityUserRole<string>>().ToTable("UserRoles", schemaName);
        modelBuilder.Entity<IdentityUserClaim<string>>().ToTable("UserClaims", schemaName);
        modelBuilder.Entity<IdentityUserLogin<string>>().ToTable("UserLogins", schemaName);
        modelBuilder.Entity<IdentityRoleClaim<string>>().ToTable("RoleClaims", schemaName);
        modelBuilder.Entity<IdentityUserToken<string>>().ToTable("UserTokens", schemaName);

        modelBuilder.HasDefaultSchema(schemaName);
    }
}
