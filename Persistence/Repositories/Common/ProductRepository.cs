using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using net_design_pattern.Domain.Models;
using net_design_pattern.Domain.Repositories.Common;
using net_design_pattern.Persistence.Context;

namespace net_design_pattern.Persistence.Repositories.Common
{
    public class ProductRepository : IProductRepository
    {
        // Dependency Injection by construtor
        private readonly AppDbContext _context;
        public ProductRepository(AppDbContext context)
        {
            _context = context;
        }
        public Product AddProduct(Product product)
        {
            try
            {
                _context.Products.Add(product);
                _context.SaveChanges();
                return product;
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return null;
        }

        public bool DeleteProduct(int productId)
        {
            try
            {
                var productToDelete = _context.Products.FirstOrDefault(x => x.Id == productId);
                if(productToDelete != null)
                {
                    productToDelete.IsDeleted = true;
                    _context.SaveChanges();
                    return true;
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return false;
        }

        public Product GetProductById(int productId)
        {
            try
            {
                return _context.Products
                .Include(x => x.Category)
                .Where(x => x.IsDeleted == false)
                .FirstOrDefault(x => x.Id == productId);
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return null;
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

        public List<Product> GetProductsByCategoryId(int categoryId)
        {
            try
            {
                return _context.Products
                .Where(x => x.IsDeleted == false && x.CategoryId == categoryId)
                .Include(x => x.Category)
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
            try
            {
                var productToUpdate = _context.Products
                .Where(x => x.IsDeleted == false)
                .FirstOrDefault(x => x.Id == productId);

                if(productToUpdate == null)
                {
                    return null;
                }

                productToUpdate.Name = product.Name;
                productToUpdate.Price = product.Price;
                productToUpdate.NumInStock = product.NumInStock;
                productToUpdate.Description = product.Description;
                productToUpdate.IsAvailable = product.IsAvailable;

                var category = _context.Categories.FirstOrDefault(x => x.Id == product.CategoryId && x.IsDeleted == false);
                productToUpdate.Category = category;

                _context.SaveChanges();

                return productToUpdate;
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            
            return null;
        }
    }
}