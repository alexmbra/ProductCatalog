using Microsoft.AspNetCore.Mvc;
using ProductCatalog.Application.DTOs;
using ProductCatalog.Application.Interfaces;
using ProductCatalog.Application.Products.Queries;
using ProductCatalog.Domain.Entities;

namespace ProductCatalog.API.Controllers;
[Route("api/[controller]")]
[ApiController]
public class ProductsController : ControllerBase
{
    private readonly IProductService _productService;

    public ProductsController(IProductService productService)
    {
        _productService = productService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<ProductDTO>>> Get()
    {
        var products = await _productService.GetProductsAsync();
        if(products is null)
        {
            return NotFound("Products not found");  
        }

        return Ok(products);
    }

    [HttpGet("{id:int}", Name = "GetProduct")]
    public async Task<ActionResult<ProductDTO>> Get(int id)
    {
        var product = await _productService.GetProductByIdAsync(id);
        if (product is null)
        {
            return NotFound("product not found");
        }

        return Ok(product);
    }

    [HttpPost]
    public async Task<ActionResult<ProductDTO>> Post([FromBody] ProductDTO productDTO)
    {
        if (productDTO is null)
        {
            return BadRequest("Invalid Data");
        }

        await _productService.Add(productDTO);

        return new CreatedAtRouteResult("GetProduct", new {id = productDTO.Id} , productDTO);
    }

    [HttpPut]
    public async Task<IActionResult> Put(int id, [FromBody] ProductDTO productDTO)
    {
        if(id != productDTO.Id)
        {
            return BadRequest();
        }

        if(productDTO is null)
        {
            return BadRequest();
        }

        await _productService.Update(productDTO);

        return Ok(productDTO);
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        var product = await _productService.GetProductByIdAsync(id);
        if(product is null)
        {
            return NotFound("Product not found");
        }

        await _productService.Remove(id);

        return Ok(product);
    }
}
