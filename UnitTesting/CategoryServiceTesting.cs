using System;
using Xunit;
using net_design_pattern.Domain.Services.Admin;
using net_design_pattern.Domain.Models.DTOs;
using System.Collections.Generic;
using Microsoft.Extensions.Logging;
using net_design_pattern.Controllers;
using FluentAssertions;
using net_design_pattern.Domain.Services.Communication;
using Moq;
using net_design_pattern.Domain.Repositories.Admin;
using System.Linq;
using net_design_pattern.Domain.Services.Common;
using net_design_pattern.Domain.Repositories.Common;
using net_design_pattern.Domain.Repositories.Authorization;
using net_design_pattern.Services.Admin;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;

namespace net_design_pattern.Tests
{
    public class CategoryServiceTesting
    {
        private readonly Mock<ICategoryService> categoryService = new();
        private readonly Mock<IRoleRepository> roleRepository = new();
        private readonly IMapper _mapper;

        // Unit test get all categories successfully.
        [Fact]
        public void GetAllCategories_ReturnOkOjectRequest_Test()
        { 
            List<CategoryDto> allCategories = PrepareData();
            //Arrange
            categoryService.Setup(sv => sv.GetCategories(2)).Returns(allCategories);
            roleRepository.Setup(sv => sv.CheckRole(2)).Returns(true);
            var _controller = new AdminController(roleRepository.Object, categoryService.Object);
            //Act
            var result = _controller.GetCategories();
            //Assert
            //result.Should().HaveCount(3);
            Assert.IsType<OkObjectResult>(result);
        }

        // Unit test count total category records
        [Fact]
        public void GetAllCategories_CountTotalRecord_Test()
        { 
            List<CategoryDto> allCategories = PrepareData();
            //Arrange
            categoryService.Setup(sv => sv.GetCategories(1)).Returns(allCategories);
            //Act
            var result = categoryService.Object.GetCategories(1);
            //Assert
            result.Should().HaveCount(3);
        }

        [Fact]
        public void GetCategoryById_ReturnExpectedResult_Test()
        { 
            List<CategoryDto> allCategories = PrepareData();

            var expectedItem = allCategories[2];
            int accountId  = 2;
            categoryService.Setup(sv => sv.GetCategoryById(accountId,It.IsAny<int>())).Returns(expectedItem);
            //Act
            CategoryDto result = categoryService.Object.GetCategoryById(accountId,expectedItem.Id);
            //Assert
            result.Should().BeEquivalentTo(expectedItem);
        }


        // private CategoryDto CreateCategoryDto()
        // {
        //     return new()
        //     {
        //         Id = Guid.NewGuid(),
        //         Name = Guid.NewGuid().ToString()
        //     };
        // }
        public List<CategoryDto> PrepareData()
        {
            List<CategoryDto> allCategories = new List<CategoryDto>
            {
                new CategoryDto() {Id = 1, Name = "Iphone"},
                new CategoryDto() {Id = 2, Name = "Samsung"},
                new CategoryDto() {Id = 3, Name = "Oppo"},
            };
            return allCategories;
        }
    }
}
