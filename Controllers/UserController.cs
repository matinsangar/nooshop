using Microsoft.AspNetCore.Mvc;
using System.Data.Entity;
using nooshop.Models;
using nooshop.Data;
using nooshop.Views.User;

namespace nooshop.Controllers;

public class UserController : Controller
{
    private readonly ILogger<UserController> _logger;
    private readonly AppDbContext _DbContext;
    private static string savedName;

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
            return RedirectToAction("userPanel", "User");
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

    [HttpPost]
    public IActionResult BuyLaptop(string productId, double productPrice, int count)
    {
        try
        {
            string userName = savedName;
            Console.WriteLine($"The name is {userName}");
            Console.WriteLine($"Received BuyLaptop request - ProductId: {productId}, ProductPrice: {productPrice}, Count: {count}");

            var laptop = _DbContext.Laptops.FirstOrDefault(l => l.ProductID == productId);

            if (laptop != null && laptop.AvaiableCount >= count)
            {
                var sell = new Sell
                {
                    ProductID = productId,
                    Price = productPrice,
                    Count = count,
                    ShopName = laptop.ShopProvider, 
                    ClientName = userName,
                    IsValid = true,
                    sellId = Guid.NewGuid().ToString(),
                    DateTime = DateTime.Now
                };

                _DbContext.Sells.Add(sell);
                _DbContext.SaveChanges();

                laptop.AvaiableCount -= count;
                _DbContext.SaveChanges();

                return Json(new { success = true });
            }

            return Json(new
            {
                success = false,
                message = "Failed to complete the sale. The laptop is not available or the count is invalid."
            });
        }
        catch (Exception ex)
        {
            _logger.LogError($"Error in BuyLaptop: {ex.Message}");
            return BadRequest($"An error occurred while processing the sale: {ex.Message}");
        }
    }

}