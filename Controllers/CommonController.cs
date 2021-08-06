using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using net_design_pattern.Domain.Models.DTOs;
using net_design_pattern.Domain.Services.Common;
using net_design_pattern.Domain.Services.Communication;

namespace net_design_pattern.Controllers
{
    [Route("ai")]
    [ApiController]
    public class CommonController : ControllerBase
    {
        //Dependence injection from constructor
        private readonly IProductService _productService;
        public CommonController(IProductService productService)
        {
            _productService = productService;
        }
        [HttpGet("/product")]
        public ActionResult GetProducts()
        {
            //Use a common response so it is easy for fe to get and use data.
            var response = new Response<List<ProductDto>>();
            var accountId = 2;
            // if(accountId == null)
            // {
            //     response.Code = 401; // authenticate error code
            //     response.IsSuccess = false;
            //     response.Message = "User don't have permission.";
            //     return Unauthorized(response);
            // }
            var products =  _productService.GetProducts(accountId);
            if(products == null)
            {
                response.Code = 404; // Bad request error code
                response.IsSuccess = false;
                response.Message = "User don't have permission.";
                return BadRequest(response);
            }
            response.Data = products;
            response.Message = "Get products successfully!";
            return Ok(response);
        }

        [HttpGet("/product/{productId}")]
        public ActionResult GetProductById(int productId)
        {
            var accountId = 2;
            var response = new Response<ProductDto>();
            var product = _productService.GetProductById(accountId, productId);
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

        [HttpPost("/product")]
        public ActionResult AddProduct([FromBody] ProductDto product)
        {
            var accountId = 2;// Only admin have permission to add product
            var response = new Response<ProductDto>();

            var productRes = _productService.AddProduct(accountId, product);

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

        [HttpPut("/product/{productId}")]
        public ActionResult UpdateProduct(int productId, [FromBody] ProductDto product)
        {
            var accountId = 2;// Only admin have permission to add product
            var response = new Response<ProductDto>();
            var productRes = _productService.UpdateProduct(accountId,productId, product);

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

        [HttpDelete("/product/{productId}")]
        public ActionResult DeleteProduct(int productId)
        {
            var accountId = 2;// Only admin have permission to add product
            var response = new Response<string>("");
            var productRes = _productService.DeleteProduct(accountId, productId);

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