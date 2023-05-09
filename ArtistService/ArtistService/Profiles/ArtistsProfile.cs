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
        }
    }
}