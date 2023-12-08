using nooshop.Models;

namespace nooshop.Repositories;

public interface ISellRepository
{
    void AddSell(Sell sell);
    void BuyPhoneAsync(string productId, double productPrice, int count, string userName);
    void BuyLaptopAsync(string productId, double productPrice, int count, string userName);

}