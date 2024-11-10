using Microsoft.EntityFrameworkCore;
using UserManagementApp.Domain.Entities;
using UserManagementApp.Domain.Interfaces.Repositories;

namespace UserManagementApp.Infrastructure.Repositories;

public class Repository<T> : IRepository<T> where T : Entity
{
    private readonly DbContext _context;

    public Repository(DbContext context)
    {
        _context = context;
    }

    public virtual IQueryable<T> Queryable(CancellationToken cancellationToken = default)
    {
        return _context.Set<T>().AsQueryable();
    }

    public virtual Task<T> GetByIdAsync(string id, CancellationToken cancellationToken = default)
    {
        return _context.Set<T>().FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public virtual Task<List<T>> GetByIdsAsync(IEnumerable<string> ids, CancellationToken cancellationToken = default)
    {
        return _context.Set<T>().Where(x => ids.Contains(x.Id)).ToListAsync(cancellationToken);
    }

    public virtual Task AddAsync(T entity, CancellationToken cancellationToken = default)
    {
        entity.Created = DateTimeOffset.UtcNow;
        entity.DeletedToken = Guid.Empty.ToString();

        cancellationToken.ThrowIfCancellationRequested();

        _context.Add(entity);

        return Task.CompletedTask;
    }

    public Task AddRangeAsync(List<T> entities, CancellationToken cancellationToken = default)
    {
        foreach (var entity in entities)
        {
            entity.Created = DateTimeOffset.UtcNow;
            entity.DeletedToken = Guid.Empty.ToString();

            cancellationToken.ThrowIfCancellationRequested();

            _context.Add(entity);
        }

        return Task.CompletedTask;
    }

    public virtual Task DeleteAsync(T entity, CancellationToken cancellationToken = default)
    {
        entity.IsDeleted = true;
        entity.DeletedToken = Guid.NewGuid().ToString();

        cancellationToken.ThrowIfCancellationRequested();

        _context.Set<T>().Update(entity);

        return Task.CompletedTask;
    }

    public virtual Task DeleteRangeAsync(List<T> entities, CancellationToken cancellationToken = default)
    {
        foreach (var entity in entities)
        {
            entity.IsDeleted = true;
            entity.DeletedToken = Guid.NewGuid().ToString();

            cancellationToken.ThrowIfCancellationRequested();

            _context.Set<T>().Update(entity);
        }

        return Task.CompletedTask;
    }

    public virtual Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        return _context.SaveChangesAsync(cancellationToken);
    }

    public Task<T> UpdateAsync(T entity, CancellationToken cancellationToken = default)
    {
        entity.Updated = DateTimeOffset.UtcNow;

        cancellationToken.ThrowIfCancellationRequested();

        _context.Entry(entity).State = EntityState.Modified;

        return Task.FromResult(entity);
    }
}
