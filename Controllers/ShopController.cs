using Microsoft.AspNetCore.Mvc;
using System.Data.Entity;
using nooshop.Models;
using nooshop.Data;

namespace nooshop.Controllers;

public class ShopController : Controller
{
    private readonly ILogger<ShopController> _logger;
    private readonly AppDbContext _DbContext;

    public ShopController(AppDbContext dbContext, ILogger<ShopController> logger)
    {
        _DbContext = dbContext;
        _logger = logger;
    }

    public IActionResult shopSignUp()
    {
        return View();
    }

    [HttpPost]
    public IActionResult AddNewShop(Shop shop)
    {

        if (shop == null)
        {
            _logger.LogError("Shop object is null.");
            return BadRequest("Shop object is null.");
        }

        if (ModelState.IsValid)
        {
            Console.WriteLine(shop.Email);
            Console.WriteLine(shop.ShopName);
            Console.WriteLine(shop.Password);
            // Console.WriteLine(shop.SellerId);
            Console.WriteLine(shop.TotalSell);
            _DbContext.Shops.Add(shop);
            _DbContext.SaveChanges();
            return View("shopSignUp");
        }

        _logger.LogError("ModelState is not valid.");
        return View("shopSignUp");
    }

    public IActionResult AllShops()
    {
        var shops = _DbContext.Shops.ToList();
        return View(shops);
    }
}