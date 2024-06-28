using Microsoft.EntityFrameworkCore;

namespace Blog.Api;

public static class DataExtensions
{
    public static void MigrateDb(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<BloggingContext>();
        dbContext.Database.Migrate();
    }
}
