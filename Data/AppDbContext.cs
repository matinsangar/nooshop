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

    public System.Data.Entity.DbSet<User> Users { get; set; }
    public System.Data.Entity.DbSet<Shop> Shops { get; set; }
    public System.Data.Entity.DbSet<Laptop> Laptops { get; set; }
    public System.Data.Entity.DbSet<SmartPhone> SmartPhones { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Shop>().HasKey(s => s.SellerId);
        modelBuilder.Entity<Shop>()
            .Property(s => s.ShopName)
            .IsRequired()
            .HasMaxLength(20);
        modelBuilder.Entity<Shop>()
            .Property(s => s.Password)
            .IsRequired();
        modelBuilder.Entity<Shop>()
            .Property(s => s.ProvidedPhones)
            .IsRequired();
        modelBuilder.Entity<Shop>()
            .Property(s => s.ProvidedLapTops)
            .IsRequired();
        modelBuilder.Entity<Shop>()
            .Property(s => s.TotalSell)
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
        modelBuilder.Entity<User>()
            .Property(u => u.SellRecords)
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


        base.OnModelCreating(modelBuilder);
    }
}