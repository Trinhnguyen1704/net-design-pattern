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
        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public void CategoryService_AddNewItem_Test(bool checkRole)
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

            _categoryRepository.Setup(c => c.AddCategory(It.IsAny<Category>())).Returns((Category res) =>
            {
                res.Id = id;
                return res;
            });
            _roleRepository.Setup(r => r.CheckRole(accountId)).Returns(checkRole);
            var result = _categoryService.AddCategory(accountId, category);
            if(checkRole == true)
            {
                Assert.NotNull(result);
                result.Id.Should().Equals(id);
            }else
            {
                Assert.Null(result);
            }
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

        //test delete category item 
        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public void CategoryService_DeleteByIdWithFalseRole_Test(bool checkRole)
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
            _categoryRepository.Setup(c => c.DeleteCategory(It.IsAny<int>())).Returns(checkRole);
            _roleRepository.Setup(r => r.CheckRole(It.IsAny<int>())).Returns(false);
            //act
            var result = _categoryService.DeleteCategory(accountId, categoryId);
            //assert
            if(checkRole == true)
            {
                result.Should().Equals(true);
            }else
            {
                result.Should().Equals(false);
            }
        }
        
        //test update category service
        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public void CategoryService_UpdateItemWithFalseRole_Test(bool checkRole)
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
            category.Id = 1;
            category.Name = "Test";

            _categoryRepository.Setup(c => c.UpdateCategory(It.IsAny<int>(), It.IsAny<Category>())).Returns((int categoryId,Category res) =>
            {
                categoryId = id;
                res.Id = id;
                return res;
            });
            _roleRepository.Setup(r => r.CheckRole(accountId)).Returns(checkRole);
            var result = _categoryService.UpdateCategory(accountId,id, category);
            //Assert
            if(checkRole == true)
            {
                Assert.NotNull(result);
                result.Id.Should().Equals(id);
            }else
            {
                Assert.Null(result);
            }
        }
    }
}
