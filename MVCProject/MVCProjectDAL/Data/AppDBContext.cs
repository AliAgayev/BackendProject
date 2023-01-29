using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MVCProjectDAL.Model;
using MVCProjectDAL.Model.Identity;
using static System.Formats.Asn1.AsnWriter;


namespace MVCProjectDAL.Data
{
    public class AppDBContext : IdentityDbContext<AppUser, AppRole, int>
    {
        public AppDBContext(DbContextOptions<AppDBContext> options) : base(options)
        {
        }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Product> Products { get; set; } 
        public DbSet<ProductCategory> ProductCategories { get; set; } 
        public DbSet<Branch> Branches { get; set; } 
        public DbSet<Store> Stores { get; set; } 
        public DbSet<Card> Cards { get; set; } 
        public DbSet<Order> Orders { get; set; } 
        public DbSet<OrderInf> OrderInfos { get; set; } 
    }
}
