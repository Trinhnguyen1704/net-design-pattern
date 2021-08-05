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
            CreateMap<Product, ProductDto>()
            .ForMember(dest => dest.Status, act => act.MapFrom(
                src => (src.IsAvailable == 1 ? "Available": "Unavailable")
            ));
            CreateMap<Category, CategoryDto>();
        } 
            
    }
}