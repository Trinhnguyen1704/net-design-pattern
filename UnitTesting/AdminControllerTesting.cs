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
        AdminController _controller;
        // ICategoryService _categoryService;
        IRoleRepository  _roleRepository;
        private readonly Mock<ICategoryService> categoryService = new();
        private readonly Mock<IRoleRepository> roleRepository = new();
        private readonly IMapper _mapper;
        public AdminControllerTesting()
        {
            // _categoryService = new CategoryService(categoryRep.Object, roleRepository.Object, _mapper);
            _controller = new AdminController(_roleRepository, categoryService.Object);
        }
         [Fact]
        public void GetCategories_ReturnBadRequest()
        { 
            var result = _controller.GetCategories();
            Assert.IsType<BadRequestObjectResult>(result);
        }
    }
}
