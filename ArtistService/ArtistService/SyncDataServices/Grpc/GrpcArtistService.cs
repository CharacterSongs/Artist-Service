using AutoMapper;
using Grpc.Core;
using ArtistService.Data;

namespace ArtistService.SyncDataServices.Grpc
{
    public class GrpcArtistService : GrpcArtist.GrpcArtistBase
    {
        private readonly IArtistRepo _repository;
        private readonly IMapper _mapper;

        public GrpcArtistService(IArtistRepo repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public override Task<ArtistResponse> GetAllArtists(GetAllRequest request, ServerCallContext context)
        {
            var response = new ArtistResponse();
            var artists = _repository.GetAllArtists();

            foreach(var art in artists)
            {
                response.Artist.Add(_mapper.Map<GrpcArtistModel>(art));
            }

            return Task.FromResult(response);
        }
    }
}