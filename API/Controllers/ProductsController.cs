using Core.Entities;
using Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

public class ProductsController(IUnitOfWork unitOfWork) : BaseApiController
{
    [HttpGet]
    public async Task<ActionResult<IReadOnlyList<Product>>> GetProducts()
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
        unitOfWork.ProductsRepository.AddProduct(product);
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
    [HttpGet("brands")]
    public async Task<ActionResult<IReadOnlyList<string>>> GetBrands()
    {
        var brands = await unitOfWork.ProductsRepository.GetBrandsAsync();
        if (!brands.Any())
        {
            return NotFound("No brands found");
        }
        return Ok(brands);
    }
    [HttpGet("types")]
    public async Task<ActionResult<IReadOnlyList<string>>> GetTypes()
    {
        var types = await unitOfWork.ProductsRepository.GetTypesAsync();
        if (!types.Any())
        {
            return NotFound("No types found");
        }
        return Ok(types);
    }
}