using Core.Entities;
using Core.Interfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class GenericRepository<T> (StoreContext context) : IGenericRepository<T>
where T : BaseEntity
{
    public async Task<T?> GetByIdAsync(int id)
    {
        return await context.Set<T>().FindAsync(id);
    }

    public async Task<IReadOnlyList<T>> ListAllAsync()
    {
        return await context.Set<T>().ToListAsync();
    }

    public async Task<T?> GetEntityWithSpecification(ISpecification<T> spec)
    {
        return await ApplySpecification(spec).FirstOrDefaultAsync();
    }

    public async Task<IReadOnlyList<T>> GetListWithSpecification(ISpecification<T> spec)
    {
        return await ApplySpecification(spec).ToListAsync();
    }

    public async Task<TResult?> GetEntityWithSpecification<TResult>(ISpecification<T, TResult?> spec)
    {
        return await ApplySpecification(spec).FirstOrDefaultAsync();
    }

    public async Task<IReadOnlyList<TResult>> GetListWithSpecification<TResult>(ISpecification<T, TResult> spec)
    {
        return await ApplySpecification(spec).ToListAsync();
    }

    public bool Add(T entity)
    {
        if (entity.Id is 0) return false;
        if (Exists(entity.Id)) return false;
        context.Set<T>().Add(entity);
        return true;
    }
    
    public void Update(T entity)
    {
        context.Set<T>().Attach(entity);
        context.Entry(entity).State = EntityState.Modified;
    }

    public void Delete(T entity)
    {
        context.Set<T>().Remove(entity);
    }

    public bool Exists(int id)
    {
        return context.Set<T>().Any(e => e.Id == id);
    }

    public async Task<int> CountAsync(ISpecification<T> specification)
    {
        var query = context.Set<T>().AsQueryable();
        query = specification.ApplyCriteria(query);
        return await query.CountAsync();
    }

    private IQueryable<T> ApplySpecification(ISpecification<T> specification)
    {
        return SpecificationEvaluator<T>.GetQuery(context.Set<T>().AsQueryable(), specification);
    }
    private IQueryable<TResult> ApplySpecification<TResult>(ISpecification<T, TResult> specification)
    {
        return SpecificationEvaluator<T>.GetQuery<T, TResult>(context.Set<T>()
            .AsQueryable(), specification);
    }
}