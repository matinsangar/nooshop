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
            return View("AllLaptops");
        }

        _logger.LogError("ModelState is not valid.");
        return View("AddNewLaptop");
    }

    [HttpPost]
    public async Task<IActionResult> BuyLaptop(string productId, double price, int count, string shopName,
        string clientName, bool isValid,
        string sellId, DateTime dateTime)
    {
        var user = await _DbContext.Users.FirstOrDefaultAsync(u => u.Username == clientName);
        var shop = await _DbContext.Shops.FirstOrDefaultAsync(s => s.ShopName == shopName);
        if (user != null && shop != null)
        {
            var sell = new Sell(productId, price, count, shopName, clientName, isValid, sellId, dateTime);
            await _DbContext.Sells.AddAsync(sell);
            await _DbContext.SaveChangesAsync();
            return Json(new { success = true });
        }

        return Json(new { success = false, message = "User not found." });
    }

    public IActionResult AllLaptops()
    {
        var laptops = _DbContext.Laptops.ToList();
        return View(laptops);
    }
}