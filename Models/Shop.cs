using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace nooshop.Models;

public class Shop
{
    public string ShopName { get; set; }
    public string Password { get; set; }
   
    public string sellerCode { get; set; }
    
    public string Email { get; set; }
    
    public double? TotalSell { get; set; } = 0;
}