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
// using Microsoft.VisualStudio.TestTools.UnitTesting;
using net_design_pattern.Domain.Repositories.Admin;
using net_design_pattern.Services.Admin;
using net_design_pattern.Domain.Models;
using net_design_pattern.Persistence.Helper;

namespace net_design_pattern.Tests
{
    public class CategoryServiceTesting
    {
        private Mock<ICategoryRepository> _categoryRepository = new Mock<ICategoryRepository>();
        private Mock<IRoleRepository> _roleRepository = new Mock<IRoleRepository>();
        private ICategoryService _categoryService;
        private List<Category> _listCategory = new List<Category>()
            {
                new Category() {Id =1, Name = "Iphone"},
                new Category() {Id =2, Name = "Oppo"},
                new Category() {Id =3, Name = "Samsung"}
            };

        //test get all categories service
        [Fact]
        public void CategoryService_GetAll_Test()
        {
            var mockMapper = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new MappingProfile());
            });
            var mapper = mockMapper.CreateMapper();
            _categoryService = new CategoryService(_categoryRepository.Object,_roleRepository.Object, mapper);
            var accountId = 2;
            //arrange
            _categoryRepository.Setup(c => c.GetCategories()).Returns(_listCategory);
            //act
            var result = _categoryService.GetCategories(accountId) as List<CategoryDto>;

            //assert
            Assert.NotNull(result);
            result.Should().HaveCount(3);
        }

        //test add category service
        [Fact]
        public void CategoryService_AddNewItem_Test()
        {
            var mockMapper = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new MappingProfile());
            });
            int accountId = 2;
            var mapper = mockMapper.CreateMapper();
            _categoryService = new CategoryService(_categoryRepository.Object,_roleRepository.Object, mapper);

            CategoryDto category = new CategoryDto();
            int id = 1;
            category.Name = "Test";

            _categoryRepository.Setup(c => c.AddCategory(mapper.Map<Category>(category))).Returns((Category res) =>
            {
                res.Id = id;
                return res;
            });
            _roleRepository.Setup(r => r.CheckRole(accountId)).Returns(true);
            var result = _categoryService.AddCategory(accountId, category);
            Assert.NotNull(result);
            result.Id.Should().Equals(id);
        }
        // private readonly Mock<ICategoryService> categoryService = new();
        // private readonly Mock<IRoleRepository> roleRepository = new();
        // private readonly IMapper _mapper;

        // // Unit test get all categories successfully.
        // [Fact]
        // public void GetAllCategories_ReturnOkOjectRequest_Test()
        // { 
        //     List<CategoryDto> allCategories = PrepareData();
        //     //Arrange
        //     categoryService.Setup(sv => sv.GetCategories(2)).Returns(allCategories);
        //     roleRepository.Setup(sv => sv.CheckRole(2)).Returns(true);
        //     var _controller = new AdminController(roleRepository.Object, categoryService.Object);
        //     //Act
        //     var result = _controller.GetCategories();
        //     //Assert
        //     //result.Should().HaveCount(3);
        //     Assert.IsType<OkObjectResult>(result);
        // }

        // // Unit test count total category records
        // [Fact]
        // public void GetAllCategories_CountTotalRecord_Test()
        // { 
        //     List<CategoryDto> allCategories = PrepareData();
        //     //Arrange
        //     categoryService.Setup(sv => sv.GetCategories(1)).Returns(allCategories);
        //     //Act
        //     var result = categoryService.Object.GetCategories(1);
        //     //Assert
        //     result.Should().HaveCount(3);
        // }

        // [Fact]
        // public void GetCategoryById_ReturnExpectedResult_Test()
        // { 
        //     List<CategoryDto> allCategories = PrepareData();

        //     var expectedItem = allCategories[2];
        //     int accountId  = 2;
        //     categoryService.Setup(sv => sv.GetCategoryById(accountId,It.IsAny<int>())).Returns(expectedItem);
        //     //Act
        //     CategoryDto result = categoryService.Object.GetCategoryById(accountId,expectedItem.Id);
        //     //Assert
        //     result.Should().BeEquivalentTo(expectedItem);
        // }

        // [Fact]
        // public void AddCategory_ReturnAddedItem_Test()
        // { 
        //     var itemToAdd = new CategoryDto 
        //     {
        //         Id =4,
        //         Name = "Ipad"
        //     };

        //     int accountId  = 2;
        //     categoryService.Setup(sv => sv.AddCategory(accountId,It.IsAny<CategoryDto>())).Returns(itemToAdd);
        //     //Act
        //     var createdItem = categoryService.Object.AddCategory(accountId, itemToAdd);
        //     //Assert
        //     itemToAdd.Should().BeEquivalentTo(
        //         createdItem,
        //         options => options.ComparingByMembers<CategoryDto>().ExcludingMissingMembers()
        //     );
        // }


        // // private CategoryDto CreateCategoryDto()
        // // {
        // //     return new()
        // //     {
        // //         Id = Guid.NewGuid(),
        // //         Name = Guid.NewGuid().ToString()
        // //     };
        // // }
        // public List<CategoryDto> PrepareData()
        // {
        //     List<CategoryDto> allCategories = new List<CategoryDto>
        //     {
        //         new CategoryDto() {Id = 1, Name = "Iphone"},
        //         new CategoryDto() {Id = 2, Name = "Samsung"},
        //         new CategoryDto() {Id = 3, Name = "Oppo"},
        //     };
        //     return allCategories;
        // }
    }
}
