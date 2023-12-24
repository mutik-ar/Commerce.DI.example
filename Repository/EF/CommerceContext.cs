using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Core.Entities;


namespace Repository.EF
{
    public class CommerceContext : DbContext
    {

        private readonly string connectionString;

        public CommerceContext(string connectionString)
        {
            if (connectionString == null) throw new ArgumentNullException(nameof(connectionString));
            if (string.IsNullOrWhiteSpace(connectionString))
                throw new ArgumentException("Value should not be empty.", nameof(connectionString));

            this.connectionString = connectionString;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(this.connectionString);
        }


        public DbSet<Product> Products { get; set; }
        //public DbSet<AuditEntry> AuditEntries { get; set; }
        //public DbSet<ProductInventory> ProductInventories { get; set; }
        //public DbSet<Order> Orders { get; set; }
    }
}