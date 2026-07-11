using CoperativeTelouet.DataAccess.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace CoperativeTelouet.DataAccess;

public static class DependencyInjection
{
    /// <summary>
    /// Registers AppDbContext (SQLite) and the generic repository.
    /// Swap <c>UseSqlite</c> for <c>UseNpgsql</c> when migrating to PostgreSQL.
    /// </summary>
    public static IServiceCollection AddDataAccess(
        this IServiceCollection services,
        string connectionString)
    {
        services.AddDbContext<AppDbContext>(options =>
            options.UseSqlite(connectionString));

        services.AddScoped(typeof(IRepository<>), typeof(Repository<>));

        return services;
    }
}
