using nooshop.Models;

namespace nooshop.Repositories;

public interface IShopRepository 
{
    bool VerifyShopLogin(Shop shop);
    Shop? GetShopByName(string shopName);
}