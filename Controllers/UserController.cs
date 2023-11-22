using Microsoft.AspNetCore.Mvc;
using System.Data.Entity;
using nooshop.Models;
using nooshop.Data;

namespace nooshop.Controllers;

public class UserController : Controller
{
    private ILogger<ShopController> _logger;
    protected AppDbContext _DbContext;

    public UserController(AppDbContext dbContext, ILogger<ShopController> logger)
    {
        _DbContext = dbContext;
        _logger = logger;
    }

    public IActionResult userSignup()
    {
        return View();
    }
}