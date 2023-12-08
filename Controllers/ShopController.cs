using Microsoft.AspNetCore.Mvc;
using System.Data.Entity;
using nooshop.Models;
using nooshop.Data;
using nooshop.Controllers;
using nooshop.Repositories;

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

public class ShopController : Controller
{
    private readonly ILogger<ShopController> _logger;
    private readonly AppDbContext _DbContext;
    private readonly IShopRepository _shopRepository;

    public ShopController(AppDbContext dbContext, ILogger<ShopController> logger, IShopRepository _ShopRepository)
    {
        _DbContext = dbContext;
        _logger = logger;
        _shopRepository = _ShopRepository;
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
        var isLoginValid = _shopRepository.VerifyShopLogin(shop);
        if (isLoginValid)
        {
            var claims = new[] { new Claim(ClaimTypes.Name, shop.ShopName) };
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