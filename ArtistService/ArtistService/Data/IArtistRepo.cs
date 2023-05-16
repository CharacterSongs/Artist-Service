using ArtistService.Models;

namespace ArtistService.Data
{
    public interface IArtistRepo
    {
        bool SaveChanges();
        IEnumerable<Artist> GetAllArtists();
        Artist GetArtistById(Guid id);
        void CreateArtist(Artist artist);
        void DeleteArtist(Guid id);
    }
}