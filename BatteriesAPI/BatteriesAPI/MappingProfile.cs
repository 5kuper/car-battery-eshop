using AutoMapper;
using BattAPI.App.Models;
using BattAPI.Domain.Entities;

namespace BatteriesAPI
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Battery, OutputBattery>();

            CreateMap<InputBattery, Battery>();
        }
    }
}
