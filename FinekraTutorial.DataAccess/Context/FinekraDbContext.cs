using FinekraTutorial.Entity.Entities;
using Microsoft.EntityFrameworkCore;

namespace FinekraTutorial.DataAccess.Context;

public class FinekraDbContext : DbContext
{
    public FinekraDbContext(DbContextOptions options) : base(options)
    {
    }
    public DbSet<Brand> Brands { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<OrderDetail> OrderDetails { get; set; }
    public DbSet<Perfume> Perfumes { get; set; }
    public DbSet<UserDetail> UserDetails { get; set; }
}
