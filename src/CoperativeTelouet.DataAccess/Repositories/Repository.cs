using CoperativeTelouet.Domain.Common;
using Microsoft.EntityFrameworkCore;

namespace CoperativeTelouet.DataAccess.Repositories;

public class Repository<T> : IRepository<T> where T : BaseEntity
{
    protected readonly AppDbContext Db;
    protected DbSet<T> Set => Db.Set<T>();

    public Repository(AppDbContext db) => Db = db;

    public async Task<T?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
        => await Set.FindAsync([id], cancellationToken);

    public async Task<IReadOnlyList<T>> GetAllAsync(CancellationToken cancellationToken = default)
        => await Set.AsNoTracking().ToListAsync(cancellationToken);

    public async Task<T> AddAsync(T entity, CancellationToken cancellationToken = default)
    {
        Set.Add(entity);
        await Db.SaveChangesAsync(cancellationToken);
        return entity;
    }

    public async Task UpdateAsync(T entity, CancellationToken cancellationToken = default)
    {
        Set.Update(entity);
        await Db.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(int id, CancellationToken cancellationToken = default)
    {
        var entity = await Set.FindAsync([id], cancellationToken);
        if (entity is null)
            return;

        Set.Remove(entity);
        await Db.SaveChangesAsync(cancellationToken);
    }
}
