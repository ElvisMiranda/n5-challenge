using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using N5.Challenge.Api.Middleware;
using N5.Challenge.Infrastructure;

namespace N5.Challenge.Api.Extensions;

public static class ApplicationBuilderExtensions
{
    public static void ApplyMigrations(this IApplicationBuilder app)
    {
        using var scope = app.ApplicationServices.CreateScope();

        using var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
        
        if (!dbContext.Database.ProviderName.Equals("Microsoft.EntityFrameworkCore.InMemory")) 
            dbContext.Database.Migrate();
    }

    public static void UseCustomEndpointLogging(this IApplicationBuilder app)
    {
        app.UseMiddleware<LoggingMiddleware>();
    }

    public static void UseCustomExceptionHandler(this IApplicationBuilder app)
    {
        app.UseMiddleware<ExceptionHandlingMiddleware>();
    }
}