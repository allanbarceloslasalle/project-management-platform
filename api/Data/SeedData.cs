using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Api.Data {
    public static class SeedData {
        public static async Task Initialize(IServiceProvider serviceProvider){
            using (var context = new ApplicationDbContext(
                serviceProvider.GetRequiredService<DbContextOptions<ApplicationDbContext>>()
            )) {
                var userManager = serviceProvider.GetRequiredService<UserManager<IdentityUser>>();
                var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();

                var roles = new[] {"Admin", "Manager", "User", "Developer"};

                foreach (var role in roles)
                {
                    if(!await roleManager.RoleExistsAsync(role))
                    {
                        await roleManager.CreateAsync(new IdentityRole(role));
                    }
                }


                if(userManager.Users.All(u => u.UserName != "admin@example.com"))
                {
                    var adminUser = new IdentityUser { UserName = "admin@example.com", Email = "admin@example.com"};
                    await userManager.CreateAsync(adminUser, "AdminPassword123!");
                    await userManager.AddToRoleAsync(adminUser, "Admin");
                }

                // ---

                await context.SaveChangesAsync();
            }
        }
    }
}