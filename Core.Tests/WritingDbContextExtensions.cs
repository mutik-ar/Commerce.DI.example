using Core.Entities;

namespace Core.Tests
{
    public static class WritingDbContextExtensions
    {
        public static void AddSeedData(this WritingDbContext dbContext)
        {
            var product1 = new Product
            {
                Id = Guid.NewGuid(),
                Name = "Product 1",
                Description = "Description 1",
                UnitPrice = 10
            };

            var product2 = new Product
            {
                Id = Guid.NewGuid(),
                Name = "Product 2",
                Description = "Description 2",
                UnitPrice = 20
            };

            var product3 = new Product
            {
                Id = Guid.NewGuid(),
                Name = "Product 3",
                Description = "Description 3",
                UnitPrice = 30
            };

            if (dbContext.Products.Count() > 0)
            {
                dbContext.Products.RemoveRange(dbContext.Products);
                dbContext.SaveChanges();
            }


            dbContext.Products.Add(product1);
            dbContext.Products.Add(product2);
            dbContext.Products.Add(product3);

            dbContext.SaveChanges();

        }
    }
}

