using ProductApi.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace ProductApi.Infrastructure.Data
{
    public class ProductDbContext(DbContextOptions<ProductDbContext> options) : DbContext(options)
    {
        public DbSet<Product> Product { get; set; }
    }
}
