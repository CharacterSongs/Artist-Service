using ArtistService.AsyncDataServices;
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
        private readonly IMessageBusClient _messageBusClient;

        public ArtistsController(
         IArtistRepo repo,
         IMapper mapper,
         IAlbumDataClient albumDataClient,
         IMessageBusClient messageBusClient)
        {
            _repo = repo;
            _mapper = mapper;
            _albumDataClient = albumDataClient;
            _messageBusClient = messageBusClient;
        }
        [HttpGet]
        public ActionResult<IEnumerable<ArtistReadDto>> GetArtists()
        {
            Console.WriteLine("--> Getting Artists...");

            var artistItem = _repo.GetAllArtists();

            return Ok(_mapper.Map<IEnumerable<ArtistReadDto>>(artistItem));
        }

        [HttpGet("{id}", Name = "GetArtistById")]
        public ActionResult<ArtistReadDto> GetArtistById(Guid id)
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
            //send sync message
            try
            {
                await _albumDataClient.SendArtistToAlbum(artistReadDto);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"--> could not send synchronously: {ex.Message}");
            }

            //send async message
            try
            {
                var artistPublishedDto = _mapper.Map<ArtistPublishedDto>(artistReadDto);
                artistPublishedDto.Event = "Artist_Published";
                _messageBusClient.PublishNewArtist(artistPublishedDto);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"--> could not send asynchronously: {ex.Message}");
            }
            return CreatedAtRoute(nameof(GetArtistById), new { Id = artistReadDto.Id }, artistReadDto);
        }
        [HttpDelete("{artistId}", Name = "DeleteArtist")]
        public async Task DeleteArtist(Guid artistId)
        {
            _repo.DeleteArtist(artistId);
            _repo.SaveChanges();
                //send sync message
            try
            {
                await _albumDataClient.SendArtistToAlbumForDelete(artistId);
            }
            catch (Exception ex)
            {   //send async message
                try
                {
                    var artistDeletedDto = _mapper.Map<ArtistDeletedDto>(artistId);
                    artistDeletedDto.Event = "Artist_Deleted";
                    _messageBusClient.DeleteArtist(artistDeletedDto);
                }
                catch (Exception ex2)
                {
                    Console.WriteLine($"--> could not send asynchronously: {ex2.Message}");
                }
            }
        }
    }
}