using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using nooshop.Models;

namespace nooshop.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }


    public DbSet<Shop> Shops { get; set; }
    public DbSet<Laptop> Laptops { get; set; }
    public DbSet<SmartPhone> SmartPhones { get; set; }
    public DbSet<Sell> Sells { get; set; }
    public DbSet<User> Users { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Shop>().HasKey(s => s.sellerCode);
        modelBuilder.Entity<Shop>()
            .Property(s => s.ShopName)
            .IsRequired()
            .HasMaxLength(20);
        modelBuilder.Entity<Shop>()
            .Property(s => s.Password)
            .IsRequired();
        modelBuilder.Entity<Shop>()
            .Property(s => s.TotalSell)
            .IsRequired();
        modelBuilder.Entity<Shop>()
            .Property(s => s.sellerCode)
            .IsRequired();


        modelBuilder.Entity<User>().HasKey(u => u.UserID);
        modelBuilder.Entity<User>()
            .Property(u => u.Username)
            .IsRequired();
        modelBuilder.Entity<User>()
            .Property(u => u.Password)
            .IsRequired();
        modelBuilder.Entity<User>()
            .Property(u => u.Credit)
            .IsRequired();


        modelBuilder.Entity<Laptop>().HasKey(l => l.ProductID);
        modelBuilder.Entity<Laptop>()
            .Property(l => l.Name)
            .IsRequired();
        modelBuilder.Entity<Laptop>()
            .Property(l => l.Price);
        modelBuilder.Entity<Laptop>()
            .Property(l => l.AvaiableCount)
            .IsRequired();
        modelBuilder.Entity<Laptop>()
            .Property(l => l.ShopProvider)
            .IsRequired();
        modelBuilder.Entity<Laptop>()
            .Property(l => l.Model)
            .IsRequired()
            .HasConversion<int>() // Store enum as int in the db
            .HasDefaultValue(Models.Models.etc);


        modelBuilder.Entity<SmartPhone>().HasKey(s => s.ProductID);
        modelBuilder.Entity<SmartPhone>()
            .Property(s => s.Name)
            .IsRequired();
        modelBuilder.Entity<SmartPhone>()
            .Property(s => s.Brand)
            .IsRequired()
            .HasConversion<int>()
            .HasDefaultValue(Brands.etc);
        modelBuilder.Entity<SmartPhone>()
            .Property(s => s.Price)
            .IsRequired();
        modelBuilder.Entity<SmartPhone>()
            .Property(s => s.AvaiableCount)
            .IsRequired();
        modelBuilder.Entity<SmartPhone>()
            .Property(s => s.ShopProvider)
            .IsRequired();


        modelBuilder.Entity<Sell>().HasKey(s => s.sellId);
        modelBuilder.Entity<Sell>()
            .Property(s => s.ProductID)
            .IsRequired();
        modelBuilder.Entity<Sell>()
            .Property(s => s.ShopName)
            .IsRequired();

        modelBuilder.Entity<Sell>()
            .Property(s => s.ClientName)
            .IsRequired();

        modelBuilder.Entity<Sell>()
            .Property(s => s.Count)
            .IsRequired();

        modelBuilder.Entity<Sell>()
            .Property(s => s.Price)
            .IsRequired();

        modelBuilder.Entity<Sell>()
            .Property(s => s.IsValid)
            .IsRequired();

        modelBuilder.Entity<Sell>()
            .Property(s => s.DateTime)
            .IsRequired();
    }


    public async Task<bool> VerifyUserLogin(User user)
    {
        try
        {
            var getUser = await Users.FirstOrDefaultAsync(u => u.Username == user.Username);
            if (getUser != null && user.Password == getUser.Password)
            {
                return true;
            }

            Console.WriteLine("Wrong information for shoop login...");
            return false;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public async Task<double> getUserCredit(string savedName)
    {
        var user = await Users.FirstOrDefaultAsync(u => u.Username == savedName);
        if (user != null)
        {
            return user.Credit;
        }

        return 0;
    }

    public async Task<Laptop> getLaptopInfo(string name)
    {
        var laptop = await Laptops.FirstOrDefaultAsync(l => l.Name == name);
        return laptop;
    }

    public async Task<SmartPhone> getPhoneInfo(string name)
    {
        var phone = await SmartPhones.FirstOrDefaultAsync(l => l.Name == name);
        return phone;
    }
}