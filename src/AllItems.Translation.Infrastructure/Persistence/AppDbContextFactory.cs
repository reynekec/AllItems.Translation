using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace AllItems.Translation.Infrastructure.Persistence;

/// <summary>Lets `dotnet ef migrations` construct the context at design time, outside the app's DI container.</summary>
public sealed class AppDbContextFactory : IDesignTimeDbContextFactory<AppDbContext>
{
    public AppDbContext CreateDbContext(string[] args)
    {
        var builder = new DbContextOptionsBuilder<AppDbContext>();
        builder.UseSqlite($"Data Source={AppPaths.DatabaseFilePath}");
        return new AppDbContext(builder.Options);
    }
}
