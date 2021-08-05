using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using net_design_pattern.Domain.Models;
using net_design_pattern.Domain.Repositories.Admin;

namespace net_design_pattern.Persistence.Repositories.Admin
{
    public class CategoryRepository : ICategoryRepository
    {
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
            throw new NotImplementedException();
        }

        public Category GetCategoryById(int categoryId)
        {
            throw new NotImplementedException();
        }

        public Category UpdateCategory(int categoryId, Category category)
        {
            throw new NotImplementedException();
        }
    }
}