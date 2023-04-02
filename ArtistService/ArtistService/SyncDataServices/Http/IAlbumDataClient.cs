using ArtistService.Dtos;

namespace ArtistService.SyncDataServices.Http
{
    public interface IAlbumDataClient
    {
        Task SendArtistToAlbum(ArtistReadDto art);
    }
}