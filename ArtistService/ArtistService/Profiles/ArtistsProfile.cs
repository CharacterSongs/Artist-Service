using ArtistService.Dtos;
using ArtistService.Models;
using AutoMapper;

namespace ArtistService.Profiles
{
    public class ArtistsProfile : Profile
    {
        public ArtistsProfile()
        {
            // Source -> Target
            CreateMap<Artist, ArtistReadDto>();
            CreateMap<ArtistCreateDto, Artist>();
            CreateMap<ArtistReadDto, ArtistPublishedDto>();
            CreateMap<Artist, GrpcArtistModel>()
                .ForMember(dest => dest.ArtistId, opt => opt.MapFrom(src =>src.Id))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src =>src.Name));
        }
    }
}