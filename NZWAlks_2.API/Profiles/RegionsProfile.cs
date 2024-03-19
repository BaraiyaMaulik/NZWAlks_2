using AutoMapper;
using NZWAlks_2.API.Models.Domain;
using NZWAlks_2.API.Models.DTO;

namespace NZWAlks_2.API.Profiles
{
    public class RegionsProfile : Profile
    {
        public RegionsProfile()
        {
            //Convert domain model to dto model(src to dest)
            CreateMap<Region, RegionDTO>().
                ReverseMap().
                ForMember(dest=>dest.Id,options=>options.MapFrom(src=>src.Id));
        }
    }
}
