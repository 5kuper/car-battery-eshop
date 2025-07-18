using AutoMapper;
using BattAPI.App.Common.Users;
using BattAPI.App.Specific.Notes.Models;
using BattAPI.App.Specific.Products.Batteries.Models;
using BattAPI.App.Specific.Products.JustProducts.Models;
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

            CreateMap<Note, NoteDto>();
            CreateMap<NoteInput, Note>();
        }
    }
}
