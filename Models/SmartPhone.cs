namespace nooshop.Models;
public enum Brands
{
    Apple,
    Samsung,
    Xiaomi,
    BlackBerry,
    etc
}
public class SmartPhone
{
    public string? Name { get; set; }
    public double? Price { get; set; } = 0;
    public int? AvaiableCount { get; set; } = 0;
    public int ProductID { get; set; }
    public Brands Brand { get; set; }
}