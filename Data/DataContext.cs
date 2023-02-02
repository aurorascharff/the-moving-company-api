using Microsoft.EntityFrameworkCore;
using TheMovingCompanyAPI.Entities;

namespace TheMovingCompanyAPI.Data
{
    public class DataContext : DbContext
    {
        protected readonly IConfiguration _configuration;

        public DataContext(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            var connectionStrings = _configuration.GetSection("ConnectionStrings");
            options.UseSqlServer(connectionStrings["database"]);
            options.EnableSensitiveDataLogging();
        }

        public DbSet<Customer> Customers { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderProduct> OrderProducts { get; set; }
        public DbSet<Product> Products { get; set; }
    }
}
