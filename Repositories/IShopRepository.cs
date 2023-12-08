using nooshop.Models;

namespace nooshop.Repositories;

public interface IShopRepository 
{
    bool VerifyShopLogin(Shop shop);
    Task<Shop> GetShopByNameAsync(string shopName);
}