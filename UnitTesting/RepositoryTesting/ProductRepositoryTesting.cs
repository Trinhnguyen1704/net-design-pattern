using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using net_design_pattern.Domain.Models;
using net_design_pattern.Domain.Repositories.Common;
using net_design_pattern.Persistence.Context;
using net_design_pattern.Persistence.Repositories.Common;
using Xunit;

namespace net_design_pattern.UnitTesting.RepositoryTesting
{
    public class ProductRepositoryTesting
    {
        IProductRepository productRepository;

        //get all product test
         [Fact]
        public void ProductRepository_GetAll_Test()
        {
            //Arrange
            var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase(databaseName: "phonestore")
            .Options;
            using (var context = new AppDbContext(options))
            {
                context.Categories.Add(new Category
                {
                    Id = 1,
                    Name = "IPhone"
                });
                context.Products.Add(new Product
                {
                    Id = 1,
                    Name = "IPhone 9",
                    Price = 120,
                    NumInStock = 10,
                    Description = "This is Iphone 9",
                    IsAvailable = 1,
                    CategoryId = 1
                });
                context.Products.Add(new Product
                {
                    Id = 2,
                    Name = "IPhone 12",
                    Price = 140,
                    NumInStock = 20,
                    Description = "This is Iphone 12",
                    IsAvailable = 1,
                    CategoryId = 1
                });
                context.SaveChanges();
            }
            using (var context = new AppDbContext(options))
            {
                productRepository = new ProductRepository(context);
                //Act
                var list = productRepository.GetProducts().ToList();
                //Assert
                Assert.NotNull(list);
                list.Should().HaveCount(2);
            }
        }
        //get product by id test
        [Fact]
        public void ProductRepository_GetProductById_Test()
        {
            //Arrange
            var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase(databaseName: "phonestore")
            .Options;
            using (var context = new AppDbContext(options))
            {
                context.Categories.Add(new Category
                {
                    Id = 1,
                    Name = "IPhone"
                });
                context.Products.Add(new Product
                {
                    Id = 1,
                    Name = "IPhone 9",
                    Price = 120,
                    NumInStock = 10,
                    Description = "This is Iphone 9",
                    IsAvailable = 1,
                    CategoryId = 1
                });
                context.Products.Add(new Product
                {
                    Id = 2,
                    Name = "IPhone 12",
                    Price = 140,
                    NumInStock = 20,
                    Description = "This is Iphone 12",
                    IsAvailable = 1,
                    CategoryId = 1
                });
                context.SaveChanges();
            }
            using (var context = new AppDbContext(options))
            {
                productRepository = new ProductRepository(context);
                //Act
                var product = productRepository.GetProductById(1);
                //Assert
                Assert.NotNull(product);
                product.Id.Should().Equals(1);
            }
        }
        //add new product test
        [Fact]
        public void ProductRepository_AddProduct_Test()
        {
            //Arrange
            var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase(databaseName: "phonestore")
            .Options;
            using (var context = new AppDbContext(options))
            {
                context.Categories.Add(new Category
                {
                    Id = 1,
                    Name = "IPhone"
                });
                context.Products.Add(new Product
                {
                    Id = 1,
                    Name = "IPhone 9",
                    Price = 120,
                    NumInStock = 10,
                    Description = "This is Iphone 9",
                    IsAvailable = 1,
                    CategoryId = 1
                });
                context.SaveChanges();
            }
            using (var context = new AppDbContext(options))
            {
                productRepository = new ProductRepository(context);
                var product = new Product
                {
                    Name = "IPhone 12",
                    Price = 140,
                    NumInStock = 20,
                    Description = "This is Iphone 12",
                    IsAvailable = 1,
                    CategoryId = 1
                };
                //Act
                var result = productRepository.AddProduct(product);
                //Assert
                Assert.NotNull(result);
                result.Id.Should().Equals(2);
                result.Name.Should().Equals("IPhone 12");
            }
        }
    }
}