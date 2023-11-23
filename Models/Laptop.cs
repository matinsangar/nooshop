namespace nooshop.Models;

public enum Models
{
    Apple,
    Acer,
    Hp,
    Asuz,
    etc
}

public class Laptop
{
    public string? Name { get; set; }
    public double? Price { get; set; } = 0;
    public int? AvaiableCount { get; set; } = 0;
    public Models Model { get; set; }
    public string ProductID { get; set; }
    public string ShopProvider { get; set; }
}