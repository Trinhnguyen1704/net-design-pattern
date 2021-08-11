using System.Collections.Generic;
using net_design_pattern.Domain.Models.DTOs;

namespace net_design_pattern.Domain.Services.Common
{
    public interface IProductService
    {
        List<ProductDto> GetProducts(int accountId);
        List<ProductDto> GetProductsByCategoryId(int accountId, int categoryId);
        ProductDto GetProductById(int accountId, int productId);
        ProductDto UpdateProduct(int accountId, int productId, ProductDto product);
        ProductDto AddProduct(int accountId,ProductDto Product);
        bool DeleteProduct(int accountId, int productId);
    }
}