using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using net_design_pattern.Domain.Models;
using net_design_pattern.Domain.Models.DTOs;
using net_design_pattern.Domain.Repositories.Authorization;
using net_design_pattern.Domain.Repositories.Common;
using net_design_pattern.Domain.Services.Common;

namespace  net_design_pattern.Services.Common
{
    public class ProductService : IProductService
    {
        // Dependency Injection by construtor
        private readonly IProductRepository _productRepository;
        private readonly IRoleRepository _roleRepository;
        private readonly IMapper _mapper;
        public ProductService(IProductRepository productRepository, IRoleRepository roleRepository, IMapper mapper)
        {
            _productRepository = productRepository;
            _roleRepository = roleRepository;
            _mapper = mapper;
        }
        public ProductDto AddProduct(int accountId, ProductDto product)
        {
            //only admin has permission to add product
            var checkRole = _roleRepository.CheckRole(accountId);
            if (!checkRole)
            {
                return null;
            }
            var productToAdd = _mapper.Map<Product>(product);
            var productRes = _productRepository.AddProduct(productToAdd);
            return _mapper.Map<ProductDto>(productRes);
        }

        public bool DeleteProduct(int accountId, int productId)
        {
            //only admin has permission to delete product
            var checkRole = _roleRepository.CheckRole(accountId);
            if (!checkRole)
            {
                return false;
            }
            return _productRepository.DeleteProduct(productId);
        }

        public ProductDto GetProductById(int accountId, int productId)
        {
            var product = _productRepository.GetProductById(productId);
            if (product == null)
            {
                return null;
            }
            return _mapper.Map<ProductDto>(product);
        }

        public List<ProductDto> GetProducts(int accountId)
        {
            var products = _productRepository.GetProducts();
            var listProductRet = _mapper.Map<List<ProductDto>>(products);
            for (int i = 0; i < listProductRet.Count; i++)
            {
                listProductRet[i].Category = _mapper.Map<CategoryDto>(products[i].Category);
            }
            return listProductRet;
        }

        public List<ProductDto> GetProductsByCategoryId(int accountId, int categoryId)
        {
            var products = _productRepository.GetProductsByCategoryId(categoryId);
            var listProductRet = _mapper.Map<List<ProductDto>>(products);
            for (int i = 0; i < listProductRet.Count; i++)
            {
                listProductRet[i].Category = _mapper.Map<CategoryDto>(products[i].Category);
            }
            return listProductRet;
        }

        public ProductDto UpdateProduct(int accountId, int productId, ProductDto product)
        {
           var checkRole = _roleRepository.CheckRole(accountId);
            if (!checkRole)
            {
                return null;
            }
            var productToUpdate = _mapper.Map<Product>(product);
            var productRes = _productRepository.UpdateProduct(productId, productToUpdate);
            if (productRes == null)
            {
                return null;
            }
            return _mapper.Map<ProductDto>(productRes);
        }
    }
}