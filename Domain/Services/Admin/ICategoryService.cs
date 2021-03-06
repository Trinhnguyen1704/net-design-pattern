using System.Collections.Generic;
using net_design_pattern.Domain.Models.DTOs;

namespace net_design_pattern.Domain.Services.Admin
{
    public interface ICategoryService
    {
        List<CategoryDto> GetCategories(int accountId);
        CategoryDto GetCategoryById(int accountId, int categoryId);
        CategoryDto UpdateCategory(int accountId, int categoryId, CategoryDto category);
        CategoryDto AddCategory(int accountId, CategoryDto category);
        bool DeleteCategory(int accountId, int categoryId);
    }
}
//We should separate each interface to avoid inheriting unnecessary functions 