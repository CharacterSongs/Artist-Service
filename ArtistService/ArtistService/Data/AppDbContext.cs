using ArtistService.Models;
using Microsoft.EntityFrameworkCore;

namespace ArtistService.Data
{
    public class AppDbContext : DbContext
    {
       public AppDbContext(DbContextOptions<AppDbContext> opt) : base(opt)
       {
        
       }

       public DbSet<Artist> Artists { get; set; }
    }
}