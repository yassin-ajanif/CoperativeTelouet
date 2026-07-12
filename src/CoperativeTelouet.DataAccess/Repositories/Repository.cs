using System.Linq.Expressions;
using CoperativeTelouet.Domain.Common;
using CoperativeTelouet.Domain.Logging;
using Microsoft.EntityFrameworkCore;

namespace CoperativeTelouet.DataAccess.Repositories;

public class Repository<T> : IRepository<T> where T : BaseEntity
{
    protected readonly AppDbContext Db;
    protected DbSet<T> Set => Db.Set<T>();
    private readonly IErrorLogger _logger;

    public Repository(AppDbContext db, IErrorLogger logger)
    {
        Db = db;
        _logger = logger;
    }

    public Task<T?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
        => ExecuteAsync(nameof(GetByIdAsync), async () => await Set.FindAsync([id], cancellationToken));

    public Task<T?> GetByIdWithNavigationsAsync(
        int id,
        Expression<Func<T, object>>[] includes,
        CancellationToken cancellationToken = default)
        => ExecuteAsync(nameof(GetByIdWithNavigationsAsync), async () =>
        {
            IQueryable<T> query = Set;
            foreach (var include in includes)
                query = query.Include(include);

            return await query.FirstOrDefaultAsync(e => e.Id == id, cancellationToken);
        });

    public Task<IReadOnlyList<T>> GetAllAsync(CancellationToken cancellationToken = default)
        => ExecuteAsync(nameof(GetAllAsync), async () =>
            (IReadOnlyList<T>)await Set.AsNoTracking().ToListAsync(cancellationToken));

    public Task<IReadOnlyList<T>> FindAsync(
        Expression<Func<T, bool>> predicate,
        CancellationToken cancellationToken = default)
        => ExecuteAsync(nameof(FindAsync), async () =>
            (IReadOnlyList<T>)await Set.AsNoTracking().Where(predicate).ToListAsync(cancellationToken));

    public Task<(IReadOnlyList<TResult> Items, int TotalCount)> QueryPagedAsync<TResult>(
        Expression<Func<T, bool>>? predicate,
        Func<IQueryable<T>, IOrderedQueryable<T>> orderBy,
        Expression<Func<T, TResult>> selector,
        int page,
        int pageSize,
        CancellationToken cancellationToken = default)
        => ExecuteAsync(nameof(QueryPagedAsync), async () =>
        {
            if (page < 1)
                page = 1;
            if (pageSize < 1)
                pageSize = 15;

            IQueryable<T> query = Set.AsNoTracking();
            if (predicate is not null)
                query = query.Where(predicate);

            var totalCount = await query.CountAsync(cancellationToken);
            var items = await orderBy(query)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .Select(selector)
                .ToListAsync(cancellationToken);

            return ((IReadOnlyList<TResult>)items, totalCount);
        });

    public Task<T> AddAsync(T entity, CancellationToken cancellationToken = default)
        => ExecuteAsync(nameof(AddAsync), async () =>
        {
            Set.Add(entity);
            await Db.SaveChangesAsync(cancellationToken);
            return entity;
        });

    public Task UpdateAsync(T entity, CancellationToken cancellationToken = default)
        => ExecuteAsync(nameof(UpdateAsync), async () =>
        {
            if (Db.Entry(entity).State == EntityState.Detached)
                Set.Update(entity);

            await Db.SaveChangesAsync(cancellationToken);
        });

    public Task DeleteAsync(int id, CancellationToken cancellationToken = default)
        => ExecuteAsync(nameof(DeleteAsync), async () =>
        {
            var entity = await Set.FindAsync([id], cancellationToken);
            if (entity is null)
                return;

            Set.Remove(entity);
            await Db.SaveChangesAsync(cancellationToken);
        });

    private async Task<TResult> ExecuteAsync<TResult>(string operation, Func<Task<TResult>> action)
    {
        try
        {
            return await action();
        }
        catch (OperationCanceledException)
        {
            throw;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, AppLayer.DataAccess, $"Repository<{typeof(T).Name}>.{operation}");
            throw;
        }
    }

    private async Task ExecuteAsync(string operation, Func<Task> action)
    {
        try
        {
            await action();
        }
        catch (OperationCanceledException)
        {
            throw;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, AppLayer.DataAccess, $"Repository<{typeof(T).Name}>.{operation}");
            throw;
        }
    }
}
