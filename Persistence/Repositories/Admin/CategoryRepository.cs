using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using net_design_pattern.Domain.Models;
using net_design_pattern.Domain.Repositories.Admin;
using net_design_pattern.Persistence.Context;

namespace net_design_pattern.Persistence.Repositories.Admin
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly AppDbContext _context;
        public CategoryRepository(AppDbContext context)
        {
            _context = context;
        }
        public Product AddCategory(Category category)
        {
            throw new NotImplementedException();
        }

        public bool DeleteCategory(int categoryId)
        {
            throw new NotImplementedException();
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
            throw new NotImplementedException();
        }
    }
}