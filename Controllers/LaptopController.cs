using Microsoft.AspNetCore.Mvc;
using System.Data.Entity;
using nooshop.Models;
using nooshop.Data;
using nooshop.Controllers;

namespace nooshop.Controllers;

public class LaptopController : Controller
{
    private readonly ILogger<ShopController> _logger;
    private readonly AppDbContext _DbContext;

    public LaptopController(AppDbContext dbContext, ILogger<ShopController> logger)
    {
        _DbContext = dbContext;
        _logger = logger;
    }

    public IActionResult AddNewLaptop()
    {
        return View();
    }
    
    [HttpPost]
    public async Task<IActionResult> AddNewLaptop(Laptop laptop)
    {
        if (laptop != null)
        {
            _logger.LogError("Laptop object is null !");
        }

        if (ModelState.IsValid)
        {
            _DbContext.Laptops.Add(laptop);
            await _DbContext.SaveChangesAsync();
            return RedirectToAction("AllLaptops");
        }

        _logger.LogError("ModelState is not valid.");
        return View("AddNewLaptop");
    }
    public IActionResult AllLaptops()
    {
        var laptops = _DbContext.Laptops.ToList();
        return View(laptops);
    }

    [HttpGet]
    public IActionResult DisplayAllLaptops()
    {
        var laptops = _DbContext.Laptops.ToList();
        return View(laptops);
    }
}