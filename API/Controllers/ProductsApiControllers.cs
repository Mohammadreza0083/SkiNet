using Core.Entities;
using Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

public class ProductsApiControllers(IUnitOfWork unitOfWork) : BaseApiController
{
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Product>>> GetProducts()
    {
        var products = await unitOfWork.ProductsRepository.GetProductsAsync();
        if (!products.Any())
        {
            return NotFound("No products found");
        }
        return Ok(products);
    }
    [HttpGet("{id}")]
    public async Task<ActionResult<Product>> GetProduct(int id)
    {
        var product = await unitOfWork.ProductsRepository.GetProductByIdAsync(id);
        return Ok(product);
    }
    
}