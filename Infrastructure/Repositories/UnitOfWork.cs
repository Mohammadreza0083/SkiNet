using Core.Interfaces;
using Infrastructure.Data;

namespace Infrastructure.Repositories;

public class UnitOfWork(StoreContext context, IProductRepository productRepository) : IUnitOfWork
{
    public IProductRepository ProductsRepository => productRepository;
    public async Task<bool> Complete()
    {
        return await context.SaveChangesAsync() > 0;
    }
}