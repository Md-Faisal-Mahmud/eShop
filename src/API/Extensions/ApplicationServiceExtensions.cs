using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace API.Extensions;

public static class ApplicationServiceExtensions
{
    public static async Task AddApplicationServices(this IServiceCollection services, IConfiguration config)
    {
        // set db connection
        var connectionString = config.GetConnectionString("DefaultConnection");
        services.AddDbContext<StoreContext>(options =>
        {
            options.UseSqlite(connectionString);
        });

        var loggerFactory = services.BuildServiceProvider().GetRequiredService<ILoggerFactory>();

        try
        {
            var context = services.BuildServiceProvider().GetRequiredService<StoreContext>();
            await context.Database.MigrateAsync();
        }
        catch (Exception e)
        {
            var logger = loggerFactory.CreateLogger<Program>();
            logger.LogError(e, "An error occured during migration");
        }
        
        
    }
}