using ArtistService.Models;

namespace ArtistService.Data
{
    public interface IArtistRepo
    {
        bool SaveChanges();
        IEnumerable<Artist> GetAllArtists();
        Artist GetArtistById(int id);
        void CreateArtist(Artist artist);
    }
}