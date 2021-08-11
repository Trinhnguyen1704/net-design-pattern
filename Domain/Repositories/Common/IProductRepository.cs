using System.Collections.Generic;
using net_design_pattern.Domain.Models;

namespace net_design_pattern.Domain.Repositories.Common
{
    public interface IProductRepository
    {
        List<Product> GetProducts();
        Product GetProductById(int productId);
        Product UpdateProduct(int productId, Product product);
        Product AddProduct(Product product);
        bool DeleteProduct(int productId);
        List<Product> GetProductsByCategoryId(int categoryId);
    }
}