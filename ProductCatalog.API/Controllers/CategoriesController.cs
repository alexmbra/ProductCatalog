using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProductCatalog.Application.DTOs;
using ProductCatalog.Application.Interfaces;
using ProductCatalog.Domain.Entities;

namespace ProductCatalog.API.Controllers;
[Route("api/[controller]")]
[ApiController]
public class CategoriesController : ControllerBase
{
    private readonly ICategoryService _categoryService;

    public CategoriesController(ICategoryService categoryService)
    {
        _categoryService = categoryService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<CategoryDTO>>> Get()
    {
        var categories = await _categoryService.GetCategories();
        if (categories is null)
        {
            return NotFound("Categories not found");
        }

        return Ok(categories);
    }

    [HttpGet("{id:int}", Name = "GetCategory")]
    public async Task<ActionResult<CategoryDTO>> Get(int id)
    {
        var category = await _categoryService.GetCategoryById(id);
        if (category is null)
        {
            return NotFound("category not found");
        }

        return Ok(category);
    }

    [HttpPost]
    public async Task<ActionResult<CategoryDTO>> Post([FromBody] CategoryDTO categoryDTO)
    {
        if(categoryDTO is null)
        {
            return BadRequest("Invalid Data");
        }

        await _categoryService.Add(categoryDTO);

        return new CreatedAtRouteResult("GetCategory", new { id = categoryDTO.Id }, categoryDTO);
    }

    [HttpPut]
    public async Task<IActionResult> Put(int id, [FromBody] CategoryDTO categoryDTO)
    {
        if(id != categoryDTO.Id)
        {
            return BadRequest();
        }

        if(categoryDTO is null)
        {
            return BadRequest();
        }

        await _categoryService.Update(categoryDTO);

        return Ok(categoryDTO);
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        var category = await _categoryService.GetCategoryById(id);
        if(category is null)
        {
            return NotFound("category not found");
        }

        await _categoryService.Remove(id);

        return Ok(category);
    }
}
