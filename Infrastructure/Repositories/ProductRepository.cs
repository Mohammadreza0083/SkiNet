using Core.Entities;
using Core.Interfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class ProductRepository(StoreContext context) : IProductRepository
{
    public async Task<IEnumerable<Product>> GetProductsAsync()
    {
        return await context.Products.ToListAsync();
    }

    public async Task<Product> GetProductByIdAsync(int id)
    {
        return await context.Products.FindAsync(id) ?? 
               throw new InvalidOperationException("Product not found");
    }

    public async Task<Product?> CreateProductAsync(Product product)
    {
        if (product == null || string.IsNullOrEmpty(product.Name))
        {
            throw new ArgumentNullException(nameof(product), "Product cannot be null");
        }
        if (context.Products.Any(x => x.Name == product.Name))
        {
            throw new Exception("Product with this name already exists");
        }
        await context.Products.AddAsync(product);
        return product;
    }
}