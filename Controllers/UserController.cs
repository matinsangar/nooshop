using Microsoft.AspNetCore.Mvc;
using System.Data.Entity;
using nooshop.Models;
using nooshop.Data;
using nooshop.Views.User;

namespace nooshop.Controllers;

public class UserController : Controller
{
    private ILogger<ShopController> _logger;
    protected AppDbContext _DbContext;
    private static string savedName;
    private static double savedTotalAmount;

    public UserController(AppDbContext dbContext, ILogger<ShopController> logger)
    {
        _DbContext = dbContext;
        _logger = logger;
    }

    public IActionResult userSignup()
    {
        return View();
    }

    public IActionResult userLogin()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> UserSignup(User user)
    {
        if (user == null)
        {
            _logger.LogError("Given user was null!");
            return View("userSignup");
        }

        if (ModelState.IsValid)
        {
            await _DbContext.Users.AddAsync(user);
            await _DbContext.SaveChangesAsync();
            return View("userPanel");
        }

        return View("UserSignUp");
    }

    [HttpPost]
    public async Task<IActionResult> UserLogin(User user)
    {
        var isLoginValid = await _DbContext.VerifyUserLogin(user);
        if (isLoginValid)
        {
            savedName = user.Username;
            savedTotalAmount = user.Credit;
            return View("UserLoginSuccess");
        }

        return RedirectToAction("UserLogin");
    }

    [HttpGet]
    public async Task<IActionResult> UserPanel()
    {
        var userCredit = await _DbContext.getUserCredit(savedName);
        savedTotalAmount = userCredit;
        var viewModel = new UserPanelViewModel
        {
            Credit = userCredit
        };
        return View(viewModel);
    }
}