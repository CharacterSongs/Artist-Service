using ArtistService.Models;

namespace ArtistService.Data
{
    public class ArtistRepo : IArtistRepo
    {
        private readonly AppDbContext _context;

        public ArtistRepo(AppDbContext context)
        {
            _context = context;
        }
        public void CreateArtist(Artist artist)
        {
            if(artist == null)
            {
                throw new ArgumentNullException(nameof(artist));
            }

            _context.Artists.Add(artist);
        }

        public IEnumerable<Artist> GetAllArtists()
        {
            return _context.Artists.ToList();
        }

        public Artist GetArtistById(int id)
        {
            return _context.Artists.FirstOrDefault(a => a.Id == id);
        }

        public bool SaveChanges()
        {
            return (_context.SaveChanges() >= 0);
        }
    }
}