namespace Core.Interfaces;

public interface IUnitOfWork
{
    IProductRepository ProductsRepository { get; }
}