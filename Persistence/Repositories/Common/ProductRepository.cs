using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using net_design_pattern.Domain.Models;
using net_design_pattern.Domain.Repositories.Common;
using net_design_pattern.Persistence.Context;

namespace net_design_pattern.Persistence.Repositories.Common
{
    public class ProductRepository : IProductRepository
    {
        private readonly AppDbContext _context;
        public ProductRepository(AppDbContext context)
        {
            _context = context;
        }
        public Product AddProduct(Product product)
        {
            // try
            // {

            // }
            // catch (Exception ex)
            // {
            //     Console.WriteLine(ex.Message);
            // }
            throw new NotImplementedException();
        }

        public bool DeleteProduct(int productId)
        {
            throw new NotImplementedException();
        }

        public Product GetProductById(int productId)
        {
            throw new NotImplementedException();
        }

        public List<Product> GetProducts()
        {
            try
            {
                return _context.Products
                .Include(x => x.Category)
                .Where(x => x.IsDeleted == false)
                .OrderBy(x => x.Id)
                .ToList();
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return null;
        }

        public Product UpdateProduct(int productId, Product product)
        {
            throw new NotImplementedException();
        }
    }
}