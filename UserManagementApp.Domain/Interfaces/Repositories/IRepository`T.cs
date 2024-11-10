using UserManagementApp.Domain.Entities;

namespace UserManagementApp.Domain.Interfaces.Repositories;

public interface IRepository<T> where T : Entity
{
    IQueryable<T> Queryable(CancellationToken cancellationToken = default);

    Task<T> GetByIdAsync(string id, CancellationToken cancellationToken = default);

    Task<List<T>> GetByIdsAsync(IEnumerable<string> ids, CancellationToken cancellationToken = default);

    Task AddAsync(T entity, CancellationToken cancellationToken = default);

    Task AddRangeAsync(List<T> entities, CancellationToken cancellationToken = default);

    Task DeleteAsync(T entity, CancellationToken cancellationToken = default);

    Task DeleteRangeAsync(List<T> entities, CancellationToken cancellationToken = default);

    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);

    Task<T> UpdateAsync(T entity, CancellationToken cancellationToken = default);
}
