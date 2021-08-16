using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using net_design_pattern.Domain.Models.DTOs;
using net_design_pattern.Domain.Repositories.Authorization;
using net_design_pattern.Domain.Services.Admin;
using net_design_pattern.Domain.Services.Communication;

namespace net_design_pattern.Controllers
{
    [Authorize]
    [Route("api")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        //Dependence injection from constructor
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
        /// <summary>
        /// Get all categories.
        /// </summary>
        /// <response code="401">If user is not authenticated.</response>
        /// <response code="404">If result is not found.</response>
        /// <response code="400">If Invalid request .</response>
        [HttpGet("category")]
        [ProducesResponseType(typeof(Response<List<CategoryDto>>), 200)]
        public ActionResult GetCategories()
        {
            var response = new Response<List<CategoryDto>>();
            var accountId = User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier).Value;
            if(accountId == null)
            {
                response.Code = 401; // authenticate error code
                response.IsSuccess = false;
                response.Message = "User don't have permission.";
                return Unauthorized(response);
            }

            var categories = _categoryService.GetCategories(Int32.Parse(accountId));
            if(categories == null)
            {
                response.Code = 400; // Bad request error code
                response.IsSuccess = false;
                response.Message = "User don't have permission.";
                return BadRequest(response);
            }
            response.Data = categories;
            response.Message = "Get category list successfully";
            return Ok(response);
        }

        /// <summary>
        /// Get category by category id.
        /// </summary>
        /// <response code="401">If user is not authenticated.</response>
        /// <response code="404">If result is not found.</response>
        /// <response code="400">If Invalid request .</response>
        [HttpGet("category/{categoryId}")]
        [ProducesResponseType(typeof(Response<CategoryDto>), 200)]
        public ActionResult GetCategory(int categoryId)
        {
            var response = new Response<CategoryDto>();
            var accountId = User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier).Value;
            if(accountId == null)
            {
                response.Code = 401; // authenticate error code
                response.IsSuccess = false;
                response.Message = "User don't have permission.";
                return Unauthorized(response);
            }
            var category =  _categoryService.GetCategoryById(Int32.Parse(accountId), categoryId);
            if(category == null)
            {
                response.Code = 404; // 
                response.IsSuccess = false;
                response.Message = "Category is not exist.";
                return NotFound(response);
            }
            response.Data = category;
            response.Message = "Get category successfully";
            return Ok(response);
        }

        /// <summary>
        /// Add new CategoryItem.
        /// </summary>
        /// <response code="401">If user is not authenticated.</response>
        /// <response code="201">Return the newly created item.</response>
        /// <response code="400">If Invalid request .</response>
        [HttpPost("category")]
        [ProducesResponseType(typeof(Response<CategoryDto>), 200)]
        [ProducesResponseType(typeof(Response<CategoryDto>), 201)]
        public ActionResult AddCategory([FromBody] CategoryDto category)
        {
            var response = new Response<CategoryDto>();
            var accountId = User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier).Value;
            if(accountId == null)
            {
                response.Code = 401; // authenticate error code
                response.IsSuccess = false;
                response.Message = "User don't have permission.";
                return Unauthorized(response);
            }
            var categoryRes = _categoryService.AddCategory(Int32.Parse(accountId), category);

            if (categoryRes == null)
            {
                response.Code = 400;
                response.IsSuccess = false;
                response.Message = "Add category failed.";
                return Unauthorized(response);
            }
            response.Data = categoryRes;
            response.Message = "Add category successfully.";
            return Ok(response);
        }

        /// <summary>
        /// Update a specific CategoryItem.
        /// </summary>
        /// <response code="401">If user is not authenticated.</response>
        /// <response code="404">If result is not found.</response>
        /// <response code="400">If Invalid request .</response>
        [HttpPut("category/{categoryId}")]
        [ProducesResponseType(typeof(Response<CategoryDto>), 200)]
        public ActionResult UpdateCategory(int categoryId, [FromBody] CategoryDto category)
        {
            var response = new Response<CategoryDto>();
             var accountId = User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier).Value;
            if(accountId == null)
            {
                response.Code = 401; // authenticate error code
                response.IsSuccess = false;
                response.Message = "User don't have permission.";
                return Unauthorized(response);
            }
            var categoryRes = _categoryService.UpdateCategory(Int32.Parse(accountId), categoryId, category);

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
        /// <summary>
        /// Deletes a specific CategoryItem.
        /// </summary>
        /// <response code="401">If user is not authenticated.</response>
        /// <response code="404">If result is not found.</response>
        /// <response code="400">If Invalid request .</response>
        /// <param name="categoryId"></param>
        [HttpDelete("category/{categoryId}")]
        [ProducesResponseType(typeof(Response<string>), 200)]
        public ActionResult DeleteCategory(int categoryId)
        {
            var response = new Response<string>("");
            var accountId = User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier).Value;
            if(accountId == null)
            {
                response.Code = 401; // authenticate error code
                response.IsSuccess = false;
                response.Message = "User don't have permission.";
                return Unauthorized(response);
            }
            var categoryRes = _categoryService.DeleteCategory(Int32.Parse(accountId), categoryId);
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