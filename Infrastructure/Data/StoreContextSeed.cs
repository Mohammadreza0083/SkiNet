using System.Text.Json;
using Core.Entities;

namespace Infrastructure.Data;

public class StoreContextSeed
{
    public static async Task SeedAsync(StoreContext context)
    {
        if (!context.Products.Any())
        {
            var productsData = await File.ReadAllTextAsync("../Infrastructure/Seed/products.json");
            var products = JsonSerializer.Deserialize<List<Product>>(productsData);
            if(products is null)
                return;
            await context.Products.AddRangeAsync(products);
            await context.SaveChangesAsync();
        }
    }
}