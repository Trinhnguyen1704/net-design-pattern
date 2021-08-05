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
        public void Post([FromBody] string value)
        {
        }

        [HttpPut("product/{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        [HttpDelete("product/{id}")]
        public void Delete(int id)
        {
        }
    }
}