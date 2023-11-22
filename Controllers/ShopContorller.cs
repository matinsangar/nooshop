using Microsoft.AspNetCore.Mvc;
using System.Data.Entity;
using nooshop.Models;
using nooshop.Data;

namespace nooshop.Controllers;

public class ShopContorller : Controller
{
    private readonly ILogger<ShopContorller> _logger;
    private readonly AppDbContext _DbContext;

    public ShopContorller(AppDbContext dbContext, ILogger<ShopContorller> logger)
    {
        _DbContext = dbContext;
        _logger = logger;
    }

    public IActionResult shopSignUp()
    {
        return View();
    }
}