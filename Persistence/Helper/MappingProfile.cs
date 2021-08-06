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
            CreateMap<CategoryDto, Category>();

            CreateMap<Domain.Models.Profile, ProfileDto>()
            .ForMember(dest => dest.Gender, act => act.MapFrom(
                src => (src.Gender == 0 ? "Female" : src.Gender == 1 ? "Male" : src.Gender == 2 ? "Other" : null)
            ));

            CreateMap<ProfileDto, Domain.Models.Profile>()
            .ForMember(dest => dest.Gender, act => act.MapFrom(
                src => src.Gender.ToLower().Equals("male") ? 1 : src.Gender.ToLower().Equals("female") ? 0 : src.Gender.ToLower().Equals("other")? 2 : -1
            ));
        } 
            
    }
}
//AutoMapper: object to object