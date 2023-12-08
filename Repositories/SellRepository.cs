using System.Data.Entity;
using nooshop.Data;
using nooshop.Models;

namespace nooshop.Repositories;

public class SellRepository : ISellRepository
{
    private readonly AppDbContext _context;

    public SellRepository(AppDbContext context)
    {
        _context = context;
    }

    public async void BuyPhoneAsync(string productId, double productPrice, int count, string? userName)
    {
        try
        {
            var smartPhone = _context.SmartPhones.FirstOrDefault(l => l.ProductID == productId);
    
            if (smartPhone != null && smartPhone.AvaiableCount >= count)
            {
                var sell = new Sell
                {
                    ProductID = productId,
                    Price = productPrice,
                    Count = count,
                    ShopName = smartPhone.ShopProvider,
                    ClientName = userName,
                    IsValid = true,
                    sellId = Guid.NewGuid().ToString(),
                    DateTime = DateTime.Now
                };
    
                _context.Sells.Add(sell);
                await _context.SaveChangesAsync();
    
                var provider = await _context.Shops.FirstOrDefaultAsync(p => p.sellerCode == smartPhone.ShopProvider);
                if (provider != null)
                {
                    provider.TotalSell += count * productPrice;
                    await _context.SaveChangesAsync();
                }
    
                var client = await _context.Users.FirstOrDefaultAsync(p => p.Username == userName);
                if (client != null)
                {
                    client.Credit -= count * productPrice;
                    await _context.SaveChangesAsync();
                }
    
                smartPhone.AvaiableCount -= count;
                await _context.SaveChangesAsync();
            }
    
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
        }
    }

    public async void BuyLaptopAsync(string productId, double productPrice, int count, string userName)
    {
        try
        {
            var smartPhone = _context.Laptops.FirstOrDefault(l => l.ProductID == productId);
    
            if (smartPhone != null && smartPhone.AvaiableCount >= count)
            {
                var sell = new Sell
                {
                    ProductID = productId,
                    Price = productPrice,
                    Count = count,
                    ShopName = smartPhone.ShopProvider,
                    ClientName = userName,
                    IsValid = true,
                    sellId = Guid.NewGuid().ToString(),
                    DateTime = DateTime.Now
                };
    
                _context.Sells.Add(sell);
                await _context.SaveChangesAsync();
    
                var provider = await _context.Shops.FirstOrDefaultAsync(p => p.sellerCode == smartPhone.ShopProvider);
                if (provider != null)
                {
                    provider.TotalSell += count * productPrice;
                    await _context.SaveChangesAsync();
                }
    
                var client = await _context.Users.FirstOrDefaultAsync(p => p.Username == userName);
                if (client != null)
                {
                    client.Credit -= count * productPrice;
                    await _context.SaveChangesAsync();
                }
    
                smartPhone.AvaiableCount -= count;
                await _context.SaveChangesAsync();
            }
    
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
        }
    }

    public void AddSell(Sell sell)
    {
        _context.Sells.Add(sell);
        _context.SaveChanges();
    }
}