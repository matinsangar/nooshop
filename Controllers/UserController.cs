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
    [HttpPost]
    public IActionResult AddNewUser(User user)
    {
        if (_DbContext != null)
        {
            _DbContext.Users.Add(user);
            _DbContext.SaveChanges();
            return View("UserSignUp");
        }
        else
        {
            // Log or handle the case where _DbContext is null
            return StatusCode(500, "Internal Server Error");
        }
    }
}