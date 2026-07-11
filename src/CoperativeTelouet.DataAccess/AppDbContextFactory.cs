using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace CoperativeTelouet.DataAccess;

/// <summary>
/// Used by <c>dotnet ef</c> migrations when no UI startup project is present yet.
/// </summary>
public sealed class AppDbContextFactory : IDesignTimeDbContextFactory<AppDbContext>
{
    public AppDbContext CreateDbContext(string[] args)
    {
        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseSqlite("Data Source=coperative-telouet.db")
            .Options;

        return new AppDbContext(options);
    }
}
