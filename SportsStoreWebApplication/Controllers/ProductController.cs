using Microsoft.AspNetCore.Mvc;
using SportsStoreWebApplication.Data;
using SportsStoreWebApplication.Models;

namespace SportsStoreWebApplication.Controllers;

public class ProductController : Controller
{
    private readonly IProductRepository _repo;

    public ProductController(IProductRepository repo)
    {
        _repo = repo;
    }
    
    // GET
    public IActionResult Index()
    {
        var products = _repo.GetAllProducts();
        return View(products);
    }

    public IActionResult ViewProduct(int id)
    {
        var product = _repo.GetProduct(id);
        return View(product);
    }

    public IActionResult UpdateProduct(int id)
    {
        var product = _repo.GetProduct(id);
        if (product == null)
        {
            return View("ProductNotFound");
        }
        return View(product);
    }
    
    public IActionResult UpdateProductToDatabase(Product product)
    {
        _repo.UpdateProduct(product);

        return RedirectToAction("ViewProduct", new { id = product.ProductID });
    }
    
    public IActionResult InsertProduct()
    {
        var product = _repo.AssignCategory();
        return View(product);
    }
    
    public IActionResult InsertProductToDatabase(Product productToInsert)
    {
        _repo.InsertProduct(productToInsert);
        return RedirectToAction("Index");
    }
    
    public IActionResult DeleteProduct(Product product)
    {
        _repo.DeleteProduct(product);
        return RedirectToAction("Index");
    }
}