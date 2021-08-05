using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using net_design_pattern.Domain.Models.DTOs;
using net_design_pattern.Domain.Repositories.Authorization;
using net_design_pattern.Domain.Services.Admin;

namespace net_design_pattern.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        // test api get roles
        //Inject dependency from construct
        private readonly IRoleRepository _roleRepository;
        private readonly ICategoryService _categoryService;
        public AdminController(IRoleRepository roleRepository, ICategoryService categoryService)
        {
            _roleRepository = roleRepository;
            _categoryService = categoryService;
        }
        // [HttpGet("roles")]
        // public ActionResult<IEnumerable<RoleDto>> GetRoles()
        // {
        //     // accountId is get from login, but now is hard data
        //     var accountId = 2;
        //     return _roleRepository.GetRoles(accountId);
        // }
        [HttpGet("category")]
        public ActionResult<IEnumerable<CategoryDto>> GetCategories()
        {
            // accountId is get from login, but now is hard data
            var accountId = 2;
            return _categoryService.GetCategories(accountId);
        }

        [HttpGet("category/{categoryId}")]
        public ActionResult<CategoryDto> GetCategory(int categoryId)
        {
            // accountId is get from login, but now is hard data
            // 
            var accountId = 2;
            return _categoryService.GetCategoryById(accountId, categoryId);
        }

        [HttpPost("category")]
        public ActionResult<CategoryDto> AddCategory([FromBody] CategoryDto category)
        {
            var accountId = 2;// Only admin have permission to add product
            var categoryRes = _categoryService.AddCategory(accountId, category);

            if (categoryRes == null)
            {
                return null;
            }
            return categoryRes;
        }

        [HttpPut("category/{categoryId}")]
        public ActionResult<CategoryDto> UpdateCategory(int categoryId, [FromBody] CategoryDto category)
        {
            var accountId = 2;// Only admin have permission to add product
            var categoryRes = _categoryService.UpdateCategory(accountId, categoryId, category);

            if (categoryRes == null)
            {
                return null;
            }
            return categoryRes;
        }

        [HttpDelete("category/{categoryId}")]
        public bool DeleteCategory(int categoryId)
        {
            var accountId = 2;// Only admin have permission to add product

            var categoryRes = _categoryService.DeleteCategory(accountId, categoryId);

            return categoryRes;
        }
    }
}