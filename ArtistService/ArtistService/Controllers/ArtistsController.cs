using ArtistService.Data;
using ArtistService.Dtos;
using ArtistService.Models;
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

        public ArtistsController(IArtistRepo repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
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
        public ActionResult<ArtistCreateDto> PostArtist(ArtistCreateDto artistCreateDto)
        {
            var artistModel = _mapper.Map<Artist>(artistCreateDto);
            _repo.CreateArtist(artistModel);
            _repo.SaveChanges();

            var artistReadDto = _mapper.Map<ArtistReadDto>(artistModel);

            return CreatedAtRoute(nameof(GetArtistById), new { Id = artistReadDto.Id}, artistReadDto );
        }
    }
}