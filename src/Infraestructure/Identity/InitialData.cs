using Microsoft.AspNetCore.Identity;

namespace SureProfit.Infraestructure.Identity;

public class InitialData
{
    public static List<ApplicationUser> GetAdminUsers()
    {
        var admin = new ApplicationUser
        {
            Id = "c032759e-f7a7-4079-9c29-225bc9211be1",
            UserName = "admin@admin.com",
            NormalizedUserName = "ADMIN@ADMIN.COM",
            Email = "admin@admin.com",
            NormalizedEmail = "ADMIN@ADMIN.COM",
            EmailConfirmed = true,
            LockoutEnabled = false,
            SecurityStamp = new Guid().ToString(),
        };

        admin.PasswordHash = new PasswordHasher<ApplicationUser>()
            .HashPassword(admin, "Admin@123");

        return [admin];
    }
}
