
using Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace Core.Tests
{
    public class WritingDbContext : DbContext
    {
        public DbSet<Product> Products { get; set; }


        public WritingDbContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseInMemoryDatabase("WritingDb");
        }
    }
}

