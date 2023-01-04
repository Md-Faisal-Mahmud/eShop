using System.Text;
using Core.Entities.Identity;
using Infrastructure.Identity;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace API.Extensions;

public static class IdentityServiceExtension
{
    public static async Task<IServiceCollection> AddIdentityServices(this IServiceCollection services, IConfiguration config)
    {
        var identityConnection = config.GetConnectionString("IdentityConnection");
        
        services.AddDbContext<AppIdentityDbContext>(x =>
        {
            x.UseSqlServer(identityConnection);
        });
        
        var builder = services.AddIdentityCore<AppUser>();

        builder = new IdentityBuilder(builder.UserType, builder.Services);
        builder.AddEntityFrameworkStores<AppIdentityDbContext>();
        builder.AddSignInManager<SignInManager<AppUser>>();

        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["Token:Key"])),
                    ValidIssuer = config["Token:Issuer"],
                    ValidateIssuer = true,
                    ValidateAudience = false
                };
            });

        
        var loggerFactory = services.BuildServiceProvider().GetRequiredService<ILoggerFactory>();

        try
        {
            // Migrate and Seed User Data
            
            var userManager = services.BuildServiceProvider().GetRequiredService<UserManager<AppUser>>();
            var identityContext = services.BuildServiceProvider().GetRequiredService<AppIdentityDbContext>();

            await identityContext.Database.MigrateAsync();
            await AppIdentityDbInitializer.SeedUserAsync(userManager);
        }
        catch (Exception e)
        {
            var logger = loggerFactory.CreateLogger<Program>();
            logger.LogError(e, "An error occured during migration");
        }
        
        return services;
    }
}