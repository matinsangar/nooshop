namespace nooshop.Models;

public class Sell
{
    public Sell(int productId, double price, int count, string shopName, string clientName, bool isValid, int sellId,
        DateTime dateTime)
    {
        this.ProductID = productId;
        this.Price = price;
        this.Count = count;
        this.ShopName = shopName;
        this.ClientName = clientName;
        this.IsValid = isValid;
        this.sellId = sellId;
    }

    // if we dont implement deadult constructor it wont migrate ...
    public Sell()
    {
        this.ProductID = 0;
        this.Price = 0;
        this.Count = 0;
        this.ShopName = "none";
        this.ClientName = "none";
        this.IsValid = false;
        this.sellId = 0;
    }

    public int ProductID { get; set; }
    public double Price { get; set; }
    public int Count { get; set; } = 0;
    public string ShopName { get; set; }
    public string ClientName { get; set; }
    public bool IsValid { get; set; } = false;

    public int sellId { get; set; }

    public DateTime DateTime { get; set; } = DateTime.Now;
}