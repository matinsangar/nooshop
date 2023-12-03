using Microsoft.AspNetCore.Mvc;
using System.Data.Entity;
using nooshop.Models;
using nooshop.Data;
using nooshop.Controllers;

namespace nooshop.Controllers;

public class SmartPhoneController : Controller
{
    private readonly ILogger<ShopController> _logger;
    private readonly AppDbContext _DbContext;

    public SmartPhoneController(AppDbContext dbContext, ILogger<ShopController> logger)
    {
        _DbContext = dbContext;
        _logger = logger;
    }

    public IActionResult AddNewPhone()
    {
        return View("AddNewPhone");
    }

    [HttpPost]
    public async Task<IActionResult> AddNewPhone(SmartPhone smartPhone)
    {
        if (smartPhone != null)
        {
            _logger.LogError("Phone object is null !");
        }

        if (ModelState.IsValid)
        {
            _DbContext.SmartPhones.Add(smartPhone);
            await _DbContext.SaveChangesAsync();
            return View("AllPhones");
        }

        _logger.LogError("ModelState is not valid.");
        return View("AddNewPhone");
    }
}