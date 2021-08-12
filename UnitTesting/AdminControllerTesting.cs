using System;
using Xunit;
using  net_design_pattern.Domain.Services.Admin;
using  net_design_pattern.Domain.Models.DTOs;
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

namespace ProjectService.Tests
{
    public class AdminControllerTesting
    {
        private readonly Mock<ICategoryService> categoryService = new();
        private readonly Mock<IRoleRepository> roleRepository = new();
        private readonly IMapper _mapper;

         [Fact]
        public void GetAllCategoriesTest()
        { 
            List<CategoryDto> allCategories = new List<CategoryDto>
            {
                new CategoryDto() {Id = 1, Name = "Iphone"},
                new CategoryDto() {Id = 2, Name = "Samsung"},
                new CategoryDto() {Id = 3, Name = "Oppo"},
            };
            //Arrange
            categoryService.Setup(sv => sv.GetCategories(3)).Returns(allCategories);
            roleRepository.Setup(sv => sv.CheckRole(3));
            var _controller = new AdminController(roleRepository.Object, categoryService.Object);
            //Act
            var result = categoryService.Object.GetCategories(3);
            //Assert
            result.Should().HaveCount(3);
        }

        // private CategoryDto CreateCategoryDto()
        // {
        //     return new()
        //     {
        //         Id = Guid.NewGuid(),
        //         Name = Guid.NewGuid().ToString()
        //     };
        // }
    }
}
