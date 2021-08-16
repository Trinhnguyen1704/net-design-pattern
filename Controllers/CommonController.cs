using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using net_design_pattern.Domain.Models.DTOs;
using net_design_pattern.Domain.Services.Common;
using net_design_pattern.Domain.Services.Communication;

namespace net_design_pattern.Controllers
{
    [Authorize]
    [Route("api")]
    [ApiController]
    public class CommonController : ControllerBase
    {
        //Dependence injection from constructor
        private readonly IProductService _productService;
        public CommonController(IProductService productService)
        {
            _productService = productService;
        }

        /// <summary>
        /// Get all products.
        /// </summary>
        /// <response code="401">If user is not authenticated.</response>
        /// <response code="404">If result is not found.</response>
        /// <response code="400">If Invalid request .</response>
        [HttpGet("product")]
        [ProducesResponseType(typeof(Response<List<ProductDto>>), 200)]
        public ActionResult GetProducts()
        {
            //Use a common response so it is easy for fe to get and use data.
            var response = new Response<List<ProductDto>>();
            var accountId = User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier).Value;
            if(accountId == null)
            {
                response.Code = 401; // authenticate error code
                response.IsSuccess = false;
                response.Message = "User don't have permission.";
                return Unauthorized(response);
            }
            var products =  _productService.GetProducts(Int32.Parse(accountId));
            if(products == null)
            {
                response.Code = 400; // Bad request error code
                response.IsSuccess = false;
                response.Message = "User don't have permission.";
                return BadRequest(response);
            }
            response.Data = products;
            response.Message = "Get products successfully!";
            return Ok(response);
        }

        /// <summary>
        /// Get product by product Id.
        /// </summary>
        /// <response code="401">If user is not authenticated.</response>
        /// <response code="404">If result is not found.</response>
        /// <response code="400">If Invalid request .</response>
        /// <param name="productId">Id of product to get.</param>
        [HttpGet("product/{productId}")]
        [ProducesResponseType(typeof(Response<ProductDto>), 200)]
        public ActionResult GetProductById(int productId)
        {
            var response = new Response<ProductDto>();
            var accountId = User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier).Value;
            if(accountId == null)
            {
                response.Code = 401; // authenticate error code
                response.IsSuccess = false;
                response.Message = "User don't have permission.";
                return Unauthorized(response);
            }
            var product = _productService.GetProductById(Int32.Parse(accountId), productId);
            if(product == null)
            {
                response.Code = 404; 
                response.IsSuccess = false;
                response.Message = "Product is not exist.";
                return NotFound(response);
            }
            response.Data = product;
            response.Message = "Get product successfully!";
            return Ok(response);
        }

        /// <summary>
        /// Add new product item.
        /// </summary>
        /// <response code="401">If user is not authenticated.</response>
        /// <response code="201">Return the newly created item.</response>
        /// <response code="400">If Invalid request .</response>
        /// <param name="product">Product object need to be added.</param>
        [HttpPost("product")]
        [ProducesResponseType(typeof(Response<ProductDto>), 200)]
        [ProducesResponseType(typeof(Response<ProductDto>), 201)]
        public ActionResult AddProduct([FromBody] ProductDto product)
        {
            var response = new Response<ProductDto>();
            var accountId = User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier).Value;
            if(accountId == null)
            {
                response.Code = 401; // authenticate error code
                response.IsSuccess = false;
                response.Message = "User don't have permission.";
                return Unauthorized(response);
            }
            var productRes = _productService.AddProduct(Int32.Parse(accountId), product);

            if (productRes == null)
            {
                response.Code = 404; // Bad request
                response.IsSuccess = false;
                response.Message = "User don't have permission.";
                return BadRequest(response);
            }
            response.Data = productRes;
            response.Message = "Update product successfully!";
            return Ok(response);
        }

        /// <summary>
        /// Update a specific product.
        /// </summary>
        /// <response code="401">If user is not authenticated.</response>
        /// <response code="404">If result is not found.</response>
        /// <response code="400">If Invalid request .</response>
        /// <param name="productId">Id of product need to be updated.</param>
        /// <param name="product">Product object with information need to be updated.</param>
        [HttpPut("product/{productId}")]
        [ProducesResponseType(typeof(Response<ProductDto>), 200)]
        public ActionResult UpdateProduct(int productId, [FromBody] ProductDto product)
        {
            var response = new Response<ProductDto>();
            var accountId = User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier).Value;
            if(accountId == null)
            {
                response.Code = 401; // authenticate error code
                response.IsSuccess = false;
                response.Message = "User don't have permission.";
                return Unauthorized(response);
            }
            var productRes = _productService.UpdateProduct(Int32.Parse(accountId),productId, product);

            if (productRes == null)
            {
                response.Code = 404; 
                response.IsSuccess = false;
                response.Message = "Product is not exist.";
                return NotFound(response);
            }
            response.Data = productRes;
            response.Message = "Update product successfully";
            return Ok(response);
        }

        /// <summary>
        /// Get products by category id.
        /// </summary>
        /// <response code="401">If user is not authenticated.</response>
        /// <response code="404">If result is not found.</response>
        /// <response code="400">If Invalid request .</response>
        /// <param name="categoryId">Id of category need to be gotten.</param>
        [HttpGet("product/category/{categoryId}")]
        [ProducesResponseType(typeof(Response<List<ProductDto>>), 200)]
        public ActionResult GetProductsByCategoryId(int categoryId)
        {
            var response = new Response<List<ProductDto>>();
            var accountId = User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier).Value;
            if(accountId == null)
            {
                response.Code = 401; // authenticate error code
                response.IsSuccess = false;
                response.Message = "User don't have permission.";
                return Unauthorized(response);
            }
            var product = _productService.GetProductsByCategoryId(Int32.Parse(accountId), categoryId);
            if(product == null)
            {
                response.Code = 404; 
                response.IsSuccess = false;
                response.Message = "Product is not exist.";
                return NotFound(response);
            }
            response.Data = product;
            response.Message = "Get product by category successfully!";
            return Ok(response);
        }

        /// <summary>
        /// Delete a specific product item.
        /// </summary>
        /// <response code="401">If user is not authenticated.</response>
        /// <response code="404">If result is not found.</response>
        /// <response code="400">If Invalid request .</response>
        /// <param name="productId">Id of product need to be deleted.</param>
        [HttpDelete("product/{productId}")]
        [ProducesResponseType(typeof(Response<string>), 200)]
        public ActionResult DeleteProduct(int productId)
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
            var productRes = _productService.DeleteProduct(Int32.Parse(accountId), productId);

            if(productRes == false)
            {
                response.Code = 404; 
                response.IsSuccess = false;
                response.Message = "Product is not exist.";
                return NotFound(response);
            }
            response.Message = "Delete product successfully.";
            return Ok(response);
        }
    }
}