using Core.CommandsServices;
using Core.DTO;
using Core.Parameters;
using Core.Ports.Driving;
using Core.Results;
using Microsoft.EntityFrameworkCore;

namespace Core.Tests
{
    public class CommandsServicesTests
    {
        WritingDbContext dbContext;
        RepositoryProductInMemory repositoryProductInMemory;
        Guid repositoryProductId;
        public CommandsServicesTests()
        {
            var options = new DbContextOptionsBuilder<WritingDbContext>().UseInMemoryDatabase("WritingDb").Options;
            dbContext = new(options);
            dbContext.AddSeedData();
            repositoryProductId = dbContext.Products.First().Id;
            repositoryProductInMemory = new RepositoryProductInMemory(dbContext);
        }

        [Fact]
        public void GetAllProductsTests()
        {
            ICommandService<NullParameter, GetAllProductsResult> getAllProductService = new GetAllProductService(repositoryProductInMemory);
            GetAllProductsResult result = getAllProductService.Execute(new NullParameter());
            Assert.Equal(0, result.Code);
        }

        [Fact]
        public void InsertProductTests()
        {
            ICommandService<ProductParameter, Result> insertProductService = new InsertProductService(repositoryProductInMemory);
            ProductParameter productParameter = new ProductParameter();
            productParameter.ProductId = Guid.NewGuid();
            productParameter.Name = "product 4";
            productParameter.Description = "Description 4";
            productParameter.UnitPrice = "40";
            Result result = insertProductService.Execute(productParameter);
            dbContext.SaveChanges();
            Assert.Equal(0, result.Code);
        }

        [Fact]
        public void DeleteProductTests()
        {
            ICommandService<IdProductParameter, Result> deleteProductService = new DeleteProductService(repositoryProductInMemory);
            IdProductParameter idProductParameter = new() { ProductId = repositoryProductId };
            Result result = deleteProductService.Execute(idProductParameter); ;
            dbContext.SaveChanges();
            Assert.Equal(0, result.Code);
        }

        [Fact]
        public void UpdateProductTests()
        {
            ICommandService<ProductParameter, Result> updateProductService = new UpdateProductService(repositoryProductInMemory);
            ProductParameter productParameter = new();
            productParameter.ProductId = repositoryProductId;
            productParameter.Name = "product 4";
            productParameter.Description = "Description 4";
            productParameter.UnitPrice = "40";
            Result result = updateProductService.Execute(productParameter);
            dbContext.SaveChanges();
            Assert.Equal(0, result.Code);
        }
        [Fact]
        public void GetProductTests()
        {
            ICommandService<IdProductParameter, GetProductsResult> getProductService = new GetProductService(repositoryProductInMemory);
            IdProductParameter idProductParameter = new() { ProductId = repositoryProductId };
            GetProductsResult result = getProductService.Execute(idProductParameter); ;
            Assert.Equal(0, result.Code);
        }

    }
}