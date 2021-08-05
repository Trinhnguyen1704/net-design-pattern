using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using net_design_pattern.Domain.Models.DTOs;
using net_design_pattern.Domain.Services.Common;

namespace net_design_pattern.Controllers
{
    [Route("")]
    [ApiController]
    public class CommonController : ControllerBase
    {
        private readonly IProductService _productService;
        public CommonController(IProductService productService)
        {
            _productService = productService;
        }
        [HttpGet("product")]
        public ActionResult<IEnumerable<ProductDto>> GetProducts()
        {
            var accountId = 2;
            return _productService.GetProducts(accountId);
        }

        [HttpGet("product/{productId}")]
        public ActionResult<ProductDto> GetProductById(int productId)
        {
            var accountId = 2;
            return _productService.GetProductById(accountId, productId);
        }

        [HttpPost("product")]
        public ActionResult<ProductDto> AddProduct([FromBody] ProductDto product)
        {
            var accountId = 2;// Only admin have permission to add product
            // var errorMessage = "";
            // if (accountId == null)
            // {
            //     errorMessage = "You don't have permission!";
            //     return 
            // }
            var productRes = _productService.AddProduct(accountId, product);

            if (productRes == null)
            {
                // errorMessage = "You don't have permission!";
                 return null;
            }
            return productRes;
        }

        [HttpPut("product/{productId}")]
        public ActionResult<ProductDto> UpdateProduct(int productId, [FromBody] ProductDto product)
        {
            var accountId = 2;// Only admin have permission to add product
            var productRes = _productService.UpdateProduct(accountId,productId, product);

            if (productRes == null)
            {
                // errorMessage = "You don't have permission!";
                 return null;
            }
            return productRes;
        }

        [HttpDelete("product/{productId}")]
        public bool DeleteProduct(int productId)
        {
            var accountId = 2;// Only admin have permission to add product
            // var errorMessage = "";
            // if (accountId == null)
            // {
            //     errorMessage = "You don't have permission!";
            //     return false
            // }
            var productRes = _productService.DeleteProduct(accountId, productId);

            return productRes;
        }
    }
}