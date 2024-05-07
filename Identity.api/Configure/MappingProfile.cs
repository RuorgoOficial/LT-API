using AutoMapper;
using LT.model;
using Microsoft.AspNetCore.Identity;

namespace Identity.api.Configure
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<EntityScore, EntityScoreDto>().ReverseMap();
        }
    }
}
