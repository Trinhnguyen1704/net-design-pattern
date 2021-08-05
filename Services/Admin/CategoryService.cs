using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using net_design_pattern.Domain.Models;
using net_design_pattern.Domain.Models.DTOs;
using net_design_pattern.Domain.Repositories.Admin;
using net_design_pattern.Domain.Repositories.Authorization;
using net_design_pattern.Domain.Services.Admin;

namespace net_design_pattern.Services.Admin
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IRoleRepository _roleRepository;
        private readonly IMapper _mapper;
        public CategoryService(ICategoryRepository categoryRepository, IRoleRepository roleRepository, IMapper mapper)
        {
            _categoryRepository = categoryRepository;
            _roleRepository = roleRepository;
            _mapper = mapper;
        }

        public CategoryDto AddCategory(int accountId, CategoryDto category)
        {
            var checkRole = _roleRepository.CheckRole(accountId);
            if (!checkRole)
            {
                return null;
            }
            var categoryRet = _categoryRepository.AddCategory(_mapper.Map<Category>(category));
            if(categoryRet == null)
            {
                return null;
            }
            return _mapper.Map<CategoryDto>(categoryRet);
        }

        public bool DeleteCategory(int accountId, int categoryId)
        {
            throw new NotImplementedException();
        }

        public List<CategoryDto> GetCategories(int accountId)
        {
            var checkRole = _roleRepository.CheckRole(accountId);
            if (!checkRole)
            {
                return null;
            }
            var listCategories = _categoryRepository.GetCategories();
            var listCategoryRes = _mapper.Map<List<CategoryDto>>(listCategories);
    
            return listCategoryRes;
        }

        public CategoryDto GetCategoryById(int accountId, int categoryId)
        {
            var checkRole = _roleRepository.CheckRole(accountId);
            if (!checkRole)
            {
                return null;
            }
            var category = _categoryRepository.GetCategoryById(categoryId);
            if (category == null)
            {
                return null;
            }
            var categoryRes = _mapper.Map<CategoryDto>(category);
            return categoryRes;
        }

        public CategoryDto UpdateCategory(int accountId, int categoryId, CategoryDto category)
        {
            throw new NotImplementedException();
        }
    }
}