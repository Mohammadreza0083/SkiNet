using Core.Entities;
using Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

public class ProductsController(IUnitOfWork unitOfWork) : BaseApiController
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

    [HttpPost]
    public async Task<ActionResult<Product>> CreateProduct(Product product)
    {
        await unitOfWork.ProductsRepository.CreateProductAsync(product);
        if (await unitOfWork.Complete())
        {
            return CreatedAtAction(nameof(GetProduct), new { id = product.Id }, product);
        }
        return BadRequest("Failed to create product");
    }
    [HttpPut("{id:int}")]
    public async Task<ActionResult> UpdateProduct(int id, Product product)
    {
        if (id != product.Id)
        {
            return BadRequest("Product ID mismatch");
        }
        unitOfWork.ProductsRepository.UpdateProduct(product);
        if (await unitOfWork.Complete())
        {
            return NoContent();
        }
        return BadRequest("Failed to update product");
    }
    [HttpDelete("{id:int}")]
    public async Task<ActionResult> DeleteProduct(int id)
    {
        var product = await unitOfWork.ProductsRepository.GetProductByIdAsync(id);
        unitOfWork.ProductsRepository.DeleteProduct(product);
        if (await unitOfWork.Complete())
        {
            return NoContent();
        }
        return BadRequest("Failed to delete product");
    }
}