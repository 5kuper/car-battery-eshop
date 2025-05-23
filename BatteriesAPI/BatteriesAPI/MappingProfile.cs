using AutoMapper;
using BattAPI.App.Models;
using BattAPI.Domain.Entities;

namespace BatteriesAPI
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<User, UserInfo>()
                .ForMember(dest => dest.IsAdmin, opt => opt.MapFrom(src => src.Role == "admin"));

            CreateMap<Battery, OutputBattery>();
            CreateMap<InputBattery, Battery>();
        }
    }
}
