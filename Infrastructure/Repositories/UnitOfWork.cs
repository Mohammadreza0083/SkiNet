using Core.Interfaces;

namespace Infrastructure.Repositories;

public class UnitOfWork(IProductRepository productRepository) : IUnitOfWork
{
    public IProductRepository ProductsRepository => productRepository;
}