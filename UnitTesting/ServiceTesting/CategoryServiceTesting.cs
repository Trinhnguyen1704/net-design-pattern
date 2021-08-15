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

namespace net_design_pattern.UnitTesting.ServiceTesting
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

        // Test get category by Id
        [Fact]
        public void CategoryService_GetCategoryById_Test()
        {
            var mockMapper = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new MappingProfile());
            });
            var mapper = mockMapper.CreateMapper();
            _categoryService = new CategoryService(_categoryRepository.Object,_roleRepository.Object, mapper);
            var accountId = 2;
            var expectedItem = _listCategory[1];
            //arrange
            _categoryRepository.Setup(c => c.GetCategoryById(It.IsAny<int>())).Returns(expectedItem);
            //act
            var result = _categoryService.GetCategoryById(accountId, expectedItem.Id) as CategoryDto;

            //assert
            Assert.NotNull(result);
            result.Id.Should().Equals(expectedItem.Id);
        }

        //test delete category item with false role
        [Fact]
        public void CategoryService_DeleteByIdWithFalseRole_Test()
        {
            var mockMapper = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new MappingProfile());
            });
            var mapper = mockMapper.CreateMapper();
            _categoryService = new CategoryService(_categoryRepository.Object,_roleRepository.Object, mapper);
            var accountId = 2;
            int categoryId = 1;
            //arrange
            _categoryRepository.Setup(c => c.DeleteCategory(It.IsAny<int>())).Returns(true);
            _roleRepository.Setup(r => r.CheckRole(It.IsAny<int>())).Returns(false);
            //act
            var result = _categoryService.DeleteCategory(accountId, categoryId);

            //assert
            result.Should().Equals(false);
        }
        
        //unit test delete category successfully
         //test delete category item with false role
        [Fact]
        public void CategoryService_DeleteByIdWithTrueRole_Test()
        {
            var mockMapper = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new MappingProfile());
            });
            var mapper = mockMapper.CreateMapper();
            _categoryService = new CategoryService(_categoryRepository.Object,_roleRepository.Object, mapper);
            var accountId = 2;
            int categoryId = 1;
            //arrange
            _categoryRepository.Setup(c => c.DeleteCategory(It.IsAny<int>())).Returns(true);
            _roleRepository.Setup(r => r.CheckRole(It.IsAny<int>())).Returns(true);
            //act
            var result = _categoryService.DeleteCategory(accountId, categoryId);

            //assert
            result.Should().Equals(true);
        }

        // test update category service
        // [Fact]
        // public void CategoryService_UpdateItemWithFalseRole_Test()
        // {
        //     var mockMapper = new MapperConfiguration(cfg =>
        //     {
        //         cfg.AddProfile(new MappingProfile());
        //     });
        //     int accountId = 2;
        //     var mapper = mockMapper.CreateMapper();
        //     _categoryService = new CategoryService(_categoryRepository.Object,_roleRepository.Object, mapper);

        //     CategoryDto category = new CategoryDto();
        //     int id = 1;
        //     category.Name = "Test";

        //     _categoryRepository.Setup(c => c.AddCategory(mapper.Map<Category>(category))).Returns((Category res) =>
        //     {
        //         res.Id = id;
        //         return res;
        //     });
        //     _roleRepository.Setup(r => r.CheckRole(accountId)).Returns(false);
        //     var result = _categoryService.AddCategory(accountId, category);
        //     Assert.NotNull(result);
        //     result.Id.Should().Equals(id);
        // }
    }
}
