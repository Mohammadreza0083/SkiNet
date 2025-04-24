using Core.Entities;
using Core.Interfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class ProductRepository(StoreContext context) : IProductRepository
{
    public async Task<IReadOnlyList<Product>> GetProductsAsync()
    {
        return await context.Products.ToListAsync();
    }

    public async Task<Product> GetProductByIdAsync(int id)
    {
        return await context.Products.FindAsync(id) ?? 
               throw new InvalidOperationException("Product not found");
    }

    public async Task<IReadOnlyList<string>> GetBrandsAsync()
    {
        var brands = await context.Products.Select(b => b.Brand).Distinct().ToListAsync();
        return brands ?? 
               throw new InvalidOperationException("No brands found");
    }

    public async Task<IReadOnlyList<string>> GetTypesAsync()
    {
        var types = await context.Products.Select(b => b.Type).Distinct().ToListAsync();
        return types ?? 
               throw new InvalidOperationException("No types found");
    }

    public void AddProduct(Product product)
    {
        if (product == null || string.IsNullOrEmpty(product.Name))
        {
            throw new ArgumentNullException(nameof(product), "Product cannot be null");
        }
        if (context.Products.Any(x => x.Name == product.Name))
        {
            throw new Exception("Product with this name already exists");
        }
        context.Products.Add(product);
    }

    public void UpdateProduct(Product product)
    {
        context.Entry(product).State = EntityState.Modified;
    }

    public void DeleteProduct(Product product)
    {
        if (product == null)
        {
            throw new ArgumentNullException(nameof(product), "Product cannot be null");
        }
        context.Products.Remove(product);
    }
}