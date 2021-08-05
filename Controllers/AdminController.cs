using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using net_design_pattern.Domain.Models.DTOs;
using net_design_pattern.Domain.Repositories.Authorization;
using net_design_pattern.Domain.Services.Admin;
using net_design_pattern.Domain.Services.Communication;

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
        public ActionResult GetCategories()
        {
            var accountId = 2;
            var response = new Response<List<CategoryDto>>();

            var categories = _categoryService.GetCategories(accountId);
            if(categories == null)
            {
                //response.Code = 404; // Bad request error code
                response.IsSuccess = false;
                response.Message = "User don't have permission.";
                return BadRequest(response);
            }
            response.Data = categories;
            response.Message = "Get category list successfully";
            return Ok(response);
        }

        [HttpGet("category/{categoryId}")]
        public ActionResult GetCategory(int categoryId)
        {
            var response = new Response<CategoryDto>();
            var accountId = 2;
            var category =  _categoryService.GetCategoryById(accountId, categoryId);
            if(category == null)
            {
                //response.Code = 404; // Bad request error code
                response.IsSuccess = false;
                response.Message = "Category is not exist.";
                return NotFound(response);
            }
            response.Data = category;
            response.Message = "Get category successfully";
            return Ok(response);
        }

        [HttpPost("category")]
        public ActionResult AddCategory([FromBody] CategoryDto category)
        {
            var accountId = 2;// Only admin have permission to add product
            var response = new Response<CategoryDto>();
            var categoryRes = _categoryService.AddCategory(accountId, category);

            if (categoryRes == null)
            {
                response.Code = 401;
                response.IsSuccess = false;
                response.Message = "User don't have permission.";
                return Unauthorized(response);
            }
            response.Data = categoryRes;
            response.Message = "Add category successfully.";
            return Ok(response);
        }

        [HttpPut("category/{categoryId}")]
        public ActionResult UpdateCategory(int categoryId, [FromBody] CategoryDto category)
        {
            var accountId = 2;
            var response = new Response<CategoryDto>();
            var categoryRes = _categoryService.UpdateCategory(accountId, categoryId, category);

            if (categoryRes == null)
            {
                response.Code = 404;
                response.IsSuccess = false;
                response.Message = "Category is not exist.";
                return NotFound(response);
            }
            
            response.Data = categoryRes;
            response.Message = "Update Category successfully.";
            return Ok(response);
        }

        [HttpDelete("category/{categoryId}")]
        public ActionResult DeleteCategory(int categoryId)
        {
            var accountId = 2;// Only admin have permission to add product
            var response = new Response<string>("");
            var categoryRes = _categoryService.DeleteCategory(accountId, categoryId);
            if(categoryRes == false)
            {
                response.Code = 404; 
                response.IsSuccess = false;
                response.Message = "Category is not exist.";
                return NotFound(response);
            }
            response.Message = "Delete category successfull.";
            return Ok(response);
        }
    }
}