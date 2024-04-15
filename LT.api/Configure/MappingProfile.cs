using AutoMapper;
using LT.model;

namespace LT.api.Configure
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<EntityScore, EntityScoreDto>();
            CreateMap<EntityScoreDto, EntityScore>();
        }
    }
}
