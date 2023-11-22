namespace nooshop.Models;
public enum Brand
{
    Apple,
    Samsung,
    Xiaomi,
    BlackBerry
}
public class SmartPhone
{
    public string? Name { get; set; }
    public double? Price { get; set; } = 0;
    public int? AvaiableCount { get; set; } = 0;
    public int ProductID { get; set; }
    public Brand Brand { get; set; }
}