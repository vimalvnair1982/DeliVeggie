using DeliVeggie.Model;
using Microsoft.EntityFrameworkCore;

namespace DeliVeggie.Data
{
    public class DeliVeggieDbContext : DbContext
    {
        public DeliVeggieDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Product> Products { get; set; }

    }
}
