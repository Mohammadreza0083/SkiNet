using Core.Entities;
using Core.Interfaces;
using Core.Specification;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

public class ProductsController(IUnitOfWork unitOfWork) : BaseApiController
{
    [HttpGet]
    public async Task<ActionResult<IReadOnlyList<Product>>> GetProducts(string? brand, 
        string? type, string? sort)
    {
        var specification = new ProductSpecification(brand, type, sort);
        var products = await unitOfWork.Repository<Product>().GetListWithSpecification(specification);
        if (!products.Any())
        {
            return NotFound("No products found");
        }
        return Ok(products);
    }
    [HttpGet("{id:int}")]
    public async Task<ActionResult<Product>> GetProduct(int id)
    {
        var product = await unitOfWork.Repository<Product>().GetByIdAsync(id);
        return Ok(product);
    }

    [HttpPost]
    public async Task<ActionResult<Product>> CreateProduct(Product product)
    {
        var result = unitOfWork.Repository<Product>().Add(product);
        if (await unitOfWork.Complete() && result)
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
        unitOfWork.Repository<Product>().Update(product);
        if (await unitOfWork.Complete())
        {
            return NoContent();
        }
        return BadRequest("Failed to update product");
    }
    [HttpDelete("{id:int}")]
    public async Task<ActionResult> DeleteProduct(int id)
    {
        var product = await unitOfWork.Repository<Product>().GetByIdAsync(id);
        if (product == null) return NotFound("Product not found");
        unitOfWork.Repository<Product>().Delete(product);
        if (await unitOfWork.Complete())
        {
            return NoContent();
        }
        return BadRequest("Failed to delete product");
    }
    [HttpGet("brands")]
    public async Task<ActionResult<IReadOnlyList<string>>> GetBrands()
    {
        var specification = new BrandListSpecification();
        return Ok(await unitOfWork.Repository<Product>().GetListWithSpecification(specification));
    }
    [HttpGet("types")]
    public async Task<ActionResult<IReadOnlyList<string>>> GetTypes()
    {
        var specification = new TypeListSpecification();
        return Ok(await unitOfWork.Repository<Product>().GetListWithSpecification(specification));
    }
}