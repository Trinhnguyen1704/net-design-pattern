using System;
using System.Collections.Generic;
using System.Linq;
using net_design_pattern.Domain.Models;
using net_design_pattern.Domain.Repositories.Admin;
using net_design_pattern.Persistence.Context;

namespace net_design_pattern.Persistence.Repositories.Admin
{
    public class CategoryRepository : ICategoryRepository
    {
        //Dependency Injection
        private readonly AppDbContext _context;
        public CategoryRepository(AppDbContext context)
        {
            _context = context;
        }
        public Category AddCategory(Category category)
        {
            try
            {
                _context.Categories.Add(category);
                _context.SaveChanges();
                return category;
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return null;
        }

        public bool DeleteCategory(int categoryId)
        {
            try
            {
                var categoryToDelete = _context.Categories.FirstOrDefault(x => x.Id == categoryId);
                if(categoryToDelete == null)
                {
                    return false;
                }
                categoryToDelete.IsDeleted = true;
                _context.SaveChanges();
                return true;
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return false;
        }

        public List<Category> GetCategories()
        {
            try
            {
                return _context.Categories
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

        public Category GetCategoryById(int categoryId)
        {
            try
            {
                var category = _context.Categories.Where(x => x.IsDeleted == false).FirstOrDefault(x => x.Id == categoryId);
                return category;
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return null;
        }

        public Category UpdateCategory(int categoryId, Category category)
        {
             try
            {
                var categoryToUpdate = _context.Categories.FirstOrDefault(x => x.Id == categoryId);
                if(categoryToUpdate == null)
                {
                    return null;
                }
                categoryToUpdate.Name = category.Name;

                _context.SaveChanges();

                return categoryToUpdate;
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return null;
        }
    }
}