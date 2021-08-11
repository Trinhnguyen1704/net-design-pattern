using System.Collections.Generic;
using net_design_pattern.Domain.Models;

namespace net_design_pattern.Domain.Repositories.Admin
{
    public interface ICategoryRepository
    {
        List<Category> GetCategories();
        Category GetCategoryById(int categoryId);
        Category UpdateCategory(int categoryId, Category category);
        Category AddCategory(Category category);
        bool DeleteCategory(int categoryId);
    }
}
// One interface just should have one duty, like this interface has only function relate to category.