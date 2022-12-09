using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace API.Extensions;

public static class ApplicationServiceExtensions
{
    public static async Task AddApplicationServices(this IServiceCollection services, IConfiguration config)
    {
        // set db connection
        var connectionString = config.GetConnectionString("DefaultConnection");
        // var connectionStringSqlite = config.GetConnectionString("DefaultConnectionSqlite");
        
        services.AddDbContext<StoreContext>(options =>
        {
            options.UseSqlServer(connectionString);
            // options.UseSqlite(connectionStringSqlite);
        });

        var loggerFactory = services.BuildServiceProvider().GetRequiredService<ILoggerFactory>();

        try
        {
            // Migrate and Seed Data
            
            var context = services.BuildServiceProvider().GetRequiredService<StoreContext>();
            await context.Database.MigrateAsync();
            
            await AppDbInitializer.SeedAsync(context, loggerFactory);
        }
        catch (Exception e)
        {
            var logger = loggerFactory.CreateLogger<Program>();
            logger.LogError(e, "An error occured during migration");
        }
        
        
    }
}