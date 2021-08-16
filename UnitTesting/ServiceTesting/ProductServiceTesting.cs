using System;
using Xunit;
using net_design_pattern.Domain.Services.Admin;
using net_design_pattern.Domain.Models.DTOs;
using System.Collections.Generic;
using net_design_pattern.Controllers;
using FluentAssertions;
using Moq;
using net_design_pattern.Domain.Repositories.Authorization;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using net_design_pattern.Domain.Models;
using net_design_pattern.Persistence.Helper;
using net_design_pattern.Domain.Repositories.Common;
using net_design_pattern.Domain.Services.Common;
using net_design_pattern.Services.Common;

namespace net_design_pattern.UnitTesting.ServiceTesting
{
    public class ProductServiceTesting
    {
        private Mock<IProductRepository> _productRepository = new Mock<IProductRepository>();
        private Mock<IRoleRepository> _roleRepository = new Mock<IRoleRepository>();
        private IProductService _productService;
        private List<Product> _listProduct = new List<Product>()
            {
                new Product() {Id =1, Name = "Iphone 12", Price = 120, CategoryId = 1, NumInStock = 10, IsAvailable = 1, Description = "This is available."},
                new Product() {Id =2, Name = "Oppo A37", Price = 140, CategoryId = 1, NumInStock = 20, IsAvailable = 1, Description = "This is available."},
                new Product() {Id =3, Name = "Samsung J7", Price = 120, CategoryId = 1, NumInStock = 10, IsAvailable = 1, Description = "This is available."},
            };

        //test get all products service
        [Fact]
        public void ProductService_GetAll_Test()
        {
            var mockMapper = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new MappingProfile());
            });
            var mapper = mockMapper.CreateMapper();
            _productService = new ProductService(_productRepository.Object,_roleRepository.Object, mapper);
            var accountId = 2;
            //arrange
            _productRepository.Setup(c => c.GetProducts()).Returns(_listProduct);
            //act
            var result = _productService.GetProducts(accountId) as List<ProductDto>;

            //assert
            Assert.NotNull(result);
            result.Should().HaveCount(3);
        }
    }
}
