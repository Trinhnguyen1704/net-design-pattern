using System;
using AutoMapper;
using net_design_pattern.Domain.Models;
using net_design_pattern.Domain.Models.DTOs;

namespace net_design_pattern.Persistence.Helper
{
    public class MappingProfile : AutoMapper.Profile
    {
        public MappingProfile()
        {
            CreateMap<Role, RoleDto>();
            CreateMap<ProductDto, Product>()
            .ForMember(dest => dest.IsAvailable, act => act.MapFrom(
                src => (src.Status.Equals("Available")? 1 : 0)
            ));
            
            CreateMap<Product, ProductDto>()
            .ForMember(dest => dest.Status, act => act.MapFrom(
                src => (src.IsAvailable == 1? "Available" : "Unavailable")
            ));
            CreateMap<Category, CategoryDto>();
        } 
            
    }
}