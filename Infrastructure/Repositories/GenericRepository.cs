using System.ComponentModel;
using Core.Entities;
using Core.Interfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class GenericRepository<T> (StoreContext context) : IGenericRepository<T>
where T : BaseEntity
{
    public async Task<T> GetByIdAsync(int id)
    {
        return await context.Set<T>().FindAsync(id) ?? 
               throw new InvalidOperationException("Entity not found");
    }

    public async Task<IReadOnlyList<T>> ListAllAsync()
    {
        var entities = await context.Set<T>().ToListAsync();
        return entities ?? 
               throw new InvalidOperationException("No entities found");
    }

    public void Add(T entity)
    {
        if (entity == null)
        {
            throw new ArgumentNullException(nameof(entity), "Entity cannot be null");
        }
        if (Exists(entity.Id))
        {
            throw new WarningException("Entity already exists");
        }
        context.Set<T>().Add(entity);
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
}