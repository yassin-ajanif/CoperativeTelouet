using CoperativeTelouet.DataAccess;
using Microsoft.Extensions.DependencyInjection;

namespace CoperativeTelouet.Business;

/// <summary>
/// Business-facing entry point for schema migration / DB startup.
/// UI must call this — never <c>DatabaseInitializer</c> or DataAccess types.
/// </summary>
public interface IAppDatabaseInitializer
{
    Task InitializeAsync(CancellationToken cancellationToken = default);
}

public sealed class AppDatabaseInitializer(IServiceProvider services) : IAppDatabaseInitializer
{
    public Task InitializeAsync(CancellationToken cancellationToken = default) =>
        DatabaseInitializer.MigrateAsync(services, cancellationToken);
}
