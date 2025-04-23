using Core.Entities;

namespace Core.Interfaces;

public interface IProductRepository
{
    Task<IEnumerable<Product>> GetProductsAsync();
    Task<Product> GetProductByIdAsync(int id);
    Task<Product?> CreateProductAsync(Product product);
    void UpdateProduct(Product product);
    void DeleteProduct(Product product);
}