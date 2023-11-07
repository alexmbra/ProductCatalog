using ProductCatalog.Application.DTOs;
using ProductCatalog.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ProductCatalog.WebUI.Controllers;

[Authorize(Roles = "Admin")]
public class ProductsController : Controller
{
    private readonly IProductService _productService;
    private readonly ICategoryService _categoryService;
    private readonly IWebHostEnvironment _environment;

    public ProductsController(IProductService productService, ICategoryService categoryService, IWebHostEnvironment environment)
    {
        _productService = productService;
        _categoryService = categoryService;
        _environment = environment;
    }

    [AllowAnonymous]
    public async Task<IActionResult> Index()
    {
        var products = await _productService.GetProductsAsync();
        return View(products);
    }

    public async Task<IActionResult> Create()
    {
        ViewBag.CategoryId = new SelectList(await _categoryService.GetCategories(), "Id", "Name");

        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("Description,Price,Stock,Image,CategoryId,Id,Name")] ProductDTO product)
    {
        if (ModelState.IsValid)
        {
            await _productService.Add(product);
            return RedirectToAction(nameof(Index));
        }

        ViewBag.CategoryId = new SelectList(await _categoryService.GetCategories(), "Id", "Name");

        return View(product);
    }

    [AllowAnonymous]
    public async Task<IActionResult> Edit(int? id)
    {
        if (id is null)
        {
            return NotFound();
        }

        var product = await _productService.GetProductByIdAsync(id);
        if (product is null)
        {
            return NotFound();
        }

        ViewBag.CategoryId = new SelectList(await _categoryService.GetCategories(), "Id", "Name", product.Id);

        return View(product);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(ProductDTO product)
    {
        if (ModelState.IsValid)
        {
            await _productService.Update(product);
            return RedirectToAction(nameof(Index));
        }

        ViewBag.CategoryId = new SelectList(await _categoryService.GetCategories(), "Id", "Name", product.Id);

        return View(product);
    }

    public async Task<IActionResult> Details(int? id)
    {
        if (id is null)
        {
            return NotFound();
        }

        var product = await _productService.GetProductByIdAsync(id);
        if (product is null)
        {
            return NotFound();
        }

        var wwwroot = _environment.WebRootPath;
        var image = Path.Combine(wwwroot, "images\\" + product.Image);
        var existis = System.IO.File.Exists(image);
        ViewBag.ImageExist = existis;

        return View(product);
    }

    public async Task<IActionResult> Delete(int? id)
    {
        if (id is null)
        {
            return NotFound();
        }

        var product = await _productService.GetProductByIdAsync(id);
        if (product is null)
        {
            return NotFound();
        }

        return View(product);
    }

    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        await _productService.Remove(id);
        return RedirectToAction(nameof(Index));
    }
}
