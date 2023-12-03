using Microsoft.AspNetCore.Mvc;
using System.Data.Entity;
using nooshop.Models;
using nooshop.Data;
using nooshop.Controllers;

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

    public IActionResult shopLogin()
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
            _DbContext.Shops.Add(shop);
            _DbContext.SaveChanges();
            return View("AllShops");
        }

        _logger.LogError("ModelState is not valid.");
        return View("shopSignUp");
    }


    [HttpPost]
    public async Task<IActionResult> shopLogin(Shop shop)
    {
        var isLoginValid = await _DbContext.VerifyShopLogin(shop);
        if (isLoginValid)
        {
            // return RedirectToAction("ShopLoginSuccess");
            return View("ShopPanel");
        }

        return RedirectToAction("shopLogin");
    }

    public IActionResult AllShops()
    {
        var shops = _DbContext.Shops.ToList();
        return View(shops);
    }

    public IActionResult ShopLoginSuccess()
    {
        return View();
    }
}