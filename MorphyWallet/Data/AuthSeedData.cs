using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MorphyWallet.Models;

namespace MorphyWallet.Data
{
    public class AuthSeedData
    {
        public static async Task InitializeAsync(IServiceProvider serviceProvider)
        {
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();

            // Seed Roles
            string[] roles = { "Admin", "Guest" };
            foreach (var role in roles)
            {
                if (!await roleManager.RoleExistsAsync(role))
                    await roleManager.CreateAsync(new IdentityRole(role));
            }

            // Seed Admin User
            var adminEmail = "admin@morphy.com";
            var adminUser = await userManager.FindByEmailAsync(adminEmail);
            if (adminUser == null)
            {
                adminUser = new ApplicationUser { UserName = adminEmail, Email = adminEmail };
                await userManager.CreateAsync(adminUser, "Admin@123");
                await userManager.AddToRoleAsync(adminUser, "Admin");
            }

            // Seed Guest User
            var guestEmail = "guest@morphy.com";
            var guestUser = await userManager.FindByEmailAsync(guestEmail);
            if (guestUser == null)
            {
                guestUser = new ApplicationUser { UserName = guestEmail, Email = guestEmail };
                await userManager.CreateAsync(guestUser, "Guest@123");
                await userManager.AddToRoleAsync(guestUser, "Guest");
            }
        }
    }
}
