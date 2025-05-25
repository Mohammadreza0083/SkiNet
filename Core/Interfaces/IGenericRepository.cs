using Core.Entities;

namespace Core.Interfaces;

public interface IGenericRepository<T>
where T : BaseEntity
{
    Task<T?> GetByIdAsync(int id);
    Task<IReadOnlyList<T>> ListAllAsync();
    Task<T?> GetEntityWithSpecification(ISpecification<T> spec);
    Task<IReadOnlyList<T>> GetListWithSpecification(ISpecification<T> spec);
    Task<TResult?> GetEntityWithSpecification<TResult>(ISpecification<T, TResult?> spec);
    Task<IReadOnlyList<TResult>> GetListWithSpecification<TResult>(ISpecification<T, TResult> spec);
    bool Add(T entity);
    void Update(T entity);
    void Delete(T entity);
    bool Exists(int id);
    Task<int> CountAsync(ISpecification<T> specification);
}