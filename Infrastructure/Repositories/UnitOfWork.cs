using System.Collections.Concurrent;
using Core.Entities;
using Core.Interfaces;
using Infrastructure.Data;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.Repositories;

public class UnitOfWork(StoreContext context, IServiceProvider serviceProvider) : IUnitOfWork, IAsyncDisposable
{
    private readonly StoreContext _context = context ?? throw new ArgumentNullException(nameof(context));
    private readonly IServiceProvider _serviceProvider = serviceProvider ?? throw new ArgumentNullException(nameof(serviceProvider));
    private readonly ConcurrentDictionary<Type, Lazy<object>> _repositories = new();

    public IGenericRepository<TEntity> Repository<TEntity>() where TEntity : BaseEntity
    {
        var type = typeof(TEntity);

        var lazyRepository = _repositories.GetOrAdd(type, _ =>
            new Lazy<object>(() => _serviceProvider.GetRequiredService<IGenericRepository<TEntity>>()));

        return (IGenericRepository<TEntity>)lazyRepository.Value;
    }

    public async Task<bool> Complete()
    {
        return await _context.SaveChangesAsync() > 0;
    }

    public void Dispose()
    {
        _context.Dispose();
        GC.SuppressFinalize(this);
    }

    public async ValueTask DisposeAsync()
    {
        await _context.DisposeAsync();
        GC.SuppressFinalize(this);
    }
}