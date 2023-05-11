using ArtistService.Dtos;

namespace ArtistService.AsyncDataServices
{
    public interface IMessageBusClient
    {
        void PublishNewArtist(ArtistPublishedDto artistPublishedDto);
    }
}