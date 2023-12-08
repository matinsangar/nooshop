using System;
using Microsoft.AspNetCore.Mvc;
using System.Data.Entity;
using nooshop.Models;
using nooshop.Data;
using nooshop.Views.User;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.IdentityModel.Tokens;
using nooshop.Repositories;

namespace nooshop.Controllers;

public class UserController : Controller
{
    private readonly ILogger<UserController> _logger;
    private readonly AppDbContext _DbContext;
    private readonly IUserRepository _userRepository;
    private readonly ISellRepository _sellRepository;

    public UserController(AppDbContext dbContext, ILogger<UserController> logger, IUserRepository userRepository,
        ISellRepository _SellRepository)
    {
        _DbContext = dbContext;
        _logger = logger;
        _userRepository = userRepository;
        _sellRepository = _SellRepository;
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
        var isLoginValid = _userRepository.VerifyUserLogin(user);
        if (isLoginValid)
        {
            var claims = new[] { new Claim(ClaimTypes.Name, user.Username) };
            var secretKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes("MyNameIsMatinMyNameIs2*%%%%Matisdsd898989nMyNameIsMatinMyNameIsMatin"));
            var credentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);
            var expire = DateTime.Now.AddDays(1);
            var token = new JwtSecurityToken(
                "your-issuer",
                "your-audience",
                claims,
                expires: expire,
                signingCredentials: credentials
            );
            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenString = tokenHandler.WriteToken(token);
            // Save the token in a cookie
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme)),
                new AuthenticationProperties
                {
                    ExpiresUtc = expire,
                    IsPersistent = false,
                    AllowRefresh = false
                });


            return RedirectToAction("userPanel", "User");
        }

        return RedirectToAction("userLogin");
    }


    public IActionResult UserLoginSuccess()
    {
        return View();
    }

    [HttpGet]
    [Authorize]
    public async Task<IActionResult> UserPanel()
    {
        string username = User.Identity.Name;
        var userCredit = _userRepository.getUserCredit(username);
        Console.WriteLine($"The UserName is: {username}");
        var viewModel = new UserPanelViewModel
        {
            Credit = userCredit
        };
        return View(viewModel);
    }
    //
    // [HttpPost]
    // public IActionResult BuyLaptop(string productId, double productPrice, int count)
    // {
    //     try
    //     {
    //         string userName = User.Identity.Name;
    //         Console.WriteLine($"The name is {userName}");
    //         Console.WriteLine(
    //             $"Received BuyLaptop request - ProductId: {productId}, ProductPrice: {productPrice}, Count: {count}");
    //
    //         var laptop = _DbContext.Laptops.FirstOrDefault(l => l.ProductID == productId);
    //
    //         if (laptop != null && laptop.AvaiableCount >= count)
    //         {
    //             var sell = new Sell
    //             {
    //                 ProductID = productId,
    //                 Price = productPrice,
    //                 Count = count,
    //                 ShopName = laptop.ShopProvider,
    //                 ClientName = userName,
    //                 IsValid = true,
    //                 sellId = Guid.NewGuid().ToString(),
    //                 DateTime = DateTime.Now
    //             };
    //
    //             _sellRepository.AddSell(new Sell
    //             {
    //                 ProductID = productId,
    //                 Price = productPrice,
    //                 Count = count,
    //                 ShopName = laptop.ShopProvider,
    //                 ClientName = userName,
    //                 IsValid = true,
    //                 sellId = Guid.NewGuid().ToString(),
    //                 DateTime = DateTime.Now
    //             });
    //             _DbContext.SaveChanges();
    //
    //             var provider = _DbContext.Shops.FirstOrDefault(p => p.sellerCode == laptop.ShopProvider);
    //             if (provider != null)
    //             {
    //                 provider.TotalSell += count * productPrice;
    //                 _DbContext.SaveChanges();
    //             }
    //
    //             var client = _DbContext.Users.FirstOrDefault(p => p.Username == userName);
    //             if (client != null)
    //             {
    //                 client.Credit -= count * productPrice;
    //                 _DbContext.SaveChanges();
    //             }
    //
    //             laptop.AvaiableCount -= count;
    //             _DbContext.SaveChanges();
    //
    //             return Json(new { success = true });
    //         }
    //
    //         return Json(new
    //         {
    //             success = false,
    //             message = "Failed to complete the sale. The laptop is not available or the count is invalid."
    //         });
    //     }
    //     catch (Exception ex)
    //     {
    //         _logger.LogError($"Error in BuyLaptop: {ex.Message}");
    //         return BadRequest($"An error occurred while processing the sale: {ex.Message}");
    //     }
    // }
    //

    
    [HttpPost]
    public async Task<IActionResult> BuyPhone(string productId, double productPrice, int count)
    {
        try
        {
            string? userName = User.Identity?.Name;

            if (!string.IsNullOrEmpty(userName))
            {
                Console.WriteLine($"The name is {userName}");
                Console.WriteLine(
                    $"Received BuyPhone request - ProductId: {productId}, ProductPrice: {productPrice}, Count: {count}");

                _sellRepository.BuyPhoneAsync(productId, productPrice, count, userName);

                return Json(new { success = true });
            }

            return Json(new
            {
                success = false,
                message = "Failed to complete the sale. The phone is not available or the count is invalid."
            });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error in BuyPhone");
            return BadRequest(new { error = "An error occurred while processing the sale.", details = ex.Message });
        }
    }
    
    [HttpPost]
    public async Task<IActionResult> BuyLaptop(string productId, double productPrice, int count)
    {
        try
        {
            string? userName = User.Identity?.Name;

            if (!string.IsNullOrEmpty(userName))
            {
                Console.WriteLine($"The name is {userName}");
                Console.WriteLine(
                    $"Received BuyPhone request - ProductId: {productId}, ProductPrice: {productPrice}, Count: {count}");

                _sellRepository.BuyLaptopAsync(productId, productPrice, count, userName);

                return Json(new { success = true });
            }

            return Json(new
            {
                success = false,
                message = "Failed to complete the sale. The phone is not available or the count is invalid."
            });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error in BuyPhone");
            return BadRequest(new { error = "An error occurred while processing the sale.", details = ex.Message });
        }
    }
}