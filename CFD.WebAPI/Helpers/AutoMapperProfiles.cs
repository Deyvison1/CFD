using System;
using AutoMapper;
using CFD.Dominio;
using CFD.WebAPI.Dtos;

namespace CFD.WebAPI.Helpers
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<User, UserDto>().ReverseMap();
            CreateMap<Divida, DividaDto>().ReverseMap();
            CreateMap<Renda, RendaDto>().ReverseMap();
        }
    }
}
