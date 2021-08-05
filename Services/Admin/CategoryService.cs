using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using net_design_pattern.Domain.Models;
using net_design_pattern.Domain.Models.DTOs;
using net_design_pattern.Domain.Services.Admin;

namespace net_design_pattern.Services.Admin
{
    public class CategoryService : ICategoryService
    {
        public CategoryDto AddCategory(int accountId, CategoryDto category)
        {
            throw new NotImplementedException();
        }

        public bool DeleteCategory(int accountId, int categoryId)
        {
            throw new NotImplementedException();
        }

        public List<Category> GetCategories(int accountId)
        {
            throw new NotImplementedException();
        }

        public Category GetCategoryById(int accountId, int categoryId)
        {
            throw new NotImplementedException();
        }

        public CategoryDto UpdateCategory(int accountId, int categoryId, CategoryDto category)
        {
            throw new NotImplementedException();
        }
    }
}