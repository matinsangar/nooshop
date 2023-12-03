using Microsoft.AspNetCore.Mvc;
using System.Data.Entity;
using nooshop.Models;
using nooshop.Data;
using nooshop.Views.User;
using Microsoft.AspNetCore.Authorization;

namespace nooshop.Controllers;

public class UserController : Controller
{
    private readonly ILogger<UserController> _logger;
    private readonly AppDbContext _DbContext;
    private static string savedName;
    private static double savedTotalAmount;
    public UserController(AppDbContext dbContext, ILogger<UserController> logger)
    {
        _DbContext = dbContext;
        _logger = logger;
    }

    public IActionResult UserSignUp()
    {
        return View();
    }

    public IActionResult userLogin()
    {
        return View();
    }

    [HttpPost]
    public IActionResult AddNewUser(User user)
    {
        if (user == null)
        {
            _logger.LogError("Shop object is null.");
            return BadRequest("Shop object is null.");
        }

        if (ModelState.IsValid)
        {
            _DbContext.Users.Add(user);
            _DbContext.SaveChanges();
            return View("userLogin");
        }

        _logger.LogError("ModelState is not valid.");
        return View("UserSignUp");
    }


    [HttpPost]
    public async Task<IActionResult> userLogin(User user)
    {
        var isLoginValid = await _DbContext.VerifyUserLogin(user);
        if (isLoginValid)
        {
            savedName = user.Username;
            savedTotalAmount = user.Credit;
            return RedirectToAction("userPanel","User");
        }

        return RedirectToAction("userLogin");
    }


    public IActionResult UserLoginSuccess()
    {
        return View();
    }
    [HttpGet]
    public async Task<IActionResult> UserPanel()
    {
        var userCredit = await _DbContext.getUserCredit(savedName);
        var viewModel = new UserPanelViewModel
        {
            Credit = userCredit
        };
        return View(viewModel);
    }
}