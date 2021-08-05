using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using net_design_pattern.Domain.Models;

namespace net_design_pattern.Domain.Repositories.Admin
{
    public interface ICategoryRepository
    {
        List<Category> GetCategories();
        Category GetCategoryById(int categoryId);
        Category UpdateCategory(int categoryId, Category category);
        Product AddCategory(Category category);
        bool DeleteCategory(int categoryId);
    }
}