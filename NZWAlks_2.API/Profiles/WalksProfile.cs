using AutoMapper;
using Microsoft.Extensions.Options;
using NZWAlks_2.API.Models.Domain;
using NZWAlks_2.API.Models.DTO;

namespace NZWAlks_2.API.Profiles
{
    public class WalksProfile : Profile
    {
        //Convert domain model to dto model(src to dest)
        public WalksProfile()
        {
            //Convert domain model to dto model(src to dest)
            CreateMap<Walk, WalkDTO>().
                ReverseMap().
                ForMember(dest => dest.Id, options => options.MapFrom(src => src.Id));

            CreateMap<WalkDifficulty, WalkDifficultyRequestDTO>().
                ReverseMap().
                ForMember(dest => dest.Id, options => options.MapFrom(src => src.Id));
        }
    }
}
