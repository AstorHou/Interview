using Interview.Models;
using Microsoft.EntityFrameworkCore;

namespace Interview.Data
{
    public class NorthwindContext : DbContext
    {
        public NorthwindContext(DbContextOptions<NorthwindContext> options) : base(options) { }

        public DbSet<Product> Products { get; set; }
    }
}
