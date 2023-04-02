using ArtistService.Data;
using ArtistService.Dtos;
using ArtistService.Models;
using ArtistService.SyncDataServices.Http;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace ArtistService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ArtistsController : ControllerBase
    {
        private IArtistRepo _repo;
        private IMapper _mapper;
        private readonly IAlbumDataClient _albumDataClient;

        public ArtistsController(
         IArtistRepo repo,
         IMapper mapper,
         IAlbumDataClient albumDataClient)
        {
            _repo = repo;
            _mapper = mapper;
            _albumDataClient = albumDataClient;
        }
        [HttpGet]
        public ActionResult<IEnumerable<ArtistReadDto>> GetArtists()
        {
            Console.WriteLine("--> Getting Artists...");

            var artistItem = _repo.GetAllArtists();

            return Ok(_mapper.Map<IEnumerable<ArtistReadDto>>(artistItem));
        }

        [HttpGet("{id}", Name = "GetArtistById")]
        public ActionResult<ArtistReadDto> GetArtistById(int id)
        {
            var artistItem = _repo.GetArtistById(id);
            if (artistItem != null)
            {
                return Ok(_mapper.Map<ArtistReadDto>(artistItem));
            }
            return NotFound();
        }

        [HttpPost]
        public async Task<ActionResult<ArtistCreateDto>> PostArtist(ArtistCreateDto artistCreateDto)
        {
            var artistModel = _mapper.Map<Artist>(artistCreateDto);
            _repo.CreateArtist(artistModel);
            _repo.SaveChanges();

            var artistReadDto = _mapper.Map<ArtistReadDto>(artistModel);

            try
            {
                await _albumDataClient.SendArtistToAlbum(artistReadDto);
            }
            catch(Exception ex)
            {
                Console.WriteLine($"--> could not send synchronously");
            }

            return CreatedAtRoute(nameof(GetArtistById), new { Id = artistReadDto.Id }, artistReadDto);
        }
    }
}