using Core.Entities.Identity;
using Microsoft.AspNetCore.Identity;

namespace Infrastructure.Identity;

public static class AppIdentityDbInitializer
{
    public static async Task SeedUserAsync(UserManager<AppUser> userManager)
    {
        if (!userManager.Users.Any())
        {
            var user = new AppUser()
            {
                DisplayName = "Shuvo",
                Email = "shuvo@email.com",
                UserName = "shuvo@email.com",
                Address = new Address
                {
                    FirstName = "H R",
                    LastName = "Shuvo",
                    Street = "27/A",
                    City = "Dhaka",
                    State = "DHK",
                    ZipCode = "1209"
                }
            };

            await userManager.CreateAsync(user, "Pa$$w0rd");
        }
    }
}