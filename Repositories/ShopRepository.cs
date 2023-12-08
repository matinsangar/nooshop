using System.Data.Entity;
using nooshop.Data;
using nooshop.Models;

namespace nooshop.Repositories;

public class ShopRepository : IShopRepository
{
    private readonly AppDbContext _appDbContext;

    public ShopRepository(AppDbContext app)
    {
        _appDbContext = app;
    }

    public bool VerifyShopLogin(Shop shop)
    {
        try
        {
            var getShop = _appDbContext.Shops.FirstOrDefault(s => s.ShopName == shop.ShopName);
            if (getShop != null && shop.Password == getShop.Password)
            {
                return true;
            }

            Console.WriteLine("Wrong information for shoop login...");

            return false;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public async Task<Shop> GetShopByNameAsync(string shopName)
    {
        return await _appDbContext.Shops.FirstOrDefaultAsync(s => s.ShopName == shopName);
    }
}