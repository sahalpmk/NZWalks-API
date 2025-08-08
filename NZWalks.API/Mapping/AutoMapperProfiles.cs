using AutoMapper;
using NZWalks.API.Models.Domain;
using NZWalks.API.Models.Dto;

namespace NZWalks.API.Mapping
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<Region, RegionDto>().ReverseMap();
            CreateMap<Region, AddNewRegionDto>().ReverseMap();
            CreateMap<Region, UpdateRegionDto>().ReverseMap();
            CreateMap<Walk, WalkDto>().ReverseMap();
            CreateMap<Walk, AddNewWalkDto>().ReverseMap();
            CreateMap<Walk, UpdateWalkDto>().ReverseMap();
        }
    }
}
