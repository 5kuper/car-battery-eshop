using AutoMapper;
using BattAPI.App.Services.Common.Users.Models;
using BattAPI.App.Services.Specific.Notes.Models;
using BattAPI.App.Services.Specific.Products.Batteries.Models;
using BattAPI.App.Services.Specific.Products.JustProducts.Models;
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
