namespace nooshop.Models;

public class Shop
{
    public string ShopName { get; set; }
    public string Password { get; set; }
    public int SellerId { get; set; }
    public List<SmartPhone> ProvidedPhones { get; set; }
    public List<Laptop> ProvidedLapTops { get; set; }
    public double? TotalSell { get; set; } = 0;
}