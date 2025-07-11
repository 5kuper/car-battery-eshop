using AutoMapper;
using BattAPI.App.Common.Users;
using BattAPI.App.Specific.Products.Models.Batteries;
using BattAPI.App.Specific.Products.Models.JustProducts;
using BattAPI.Domain.Entities;
using BattAPI.Domain.Entities.Products;

namespace BatteriesAPI
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<User, UserInfo>()
                .ForMember(dest => dest.IsAdmin, opt => opt.MapFrom(src => src.Role == "admin"));

            CreateMap<Product, JustProductDto>();
            CreateMap<JustProductInput, Product>();

            CreateMap<Battery, BatteryDto>();
            CreateMap<BatteryInput, Battery>();
        }
    }
}
