using ArtistService.Models;
using Microsoft.EntityFrameworkCore;

namespace ArtistService.Data
{
    public static class PrepDb
    {
       public static void PrepPopulation(IApplicationBuilder app, bool isProd)
        {
            using( var serviceScope = app.ApplicationServices.CreateScope())
            {
                SeedData(serviceScope.ServiceProvider.GetService<AppDbContext>(), isProd);
            }
        }
        private static void SeedData(AppDbContext context, bool isProd)
        {
            if(isProd)
            {
                Console.WriteLine("--> Attempting to apply migrations...");
                try
                {
                    context.Database.Migrate();
                }
                catch(Exception ex)
                {
                    Console.WriteLine($"--> Could not run migrations: {ex.Message}");
                }
            }
            
            if(!context.Artists.Any())
            {
                Console.WriteLine("--> Seeding Data...");

                context.Artists.AddRange(
                    new Artist() {Name = "Louis Tomlinson"},
                    new Artist() {Name = "STARSET"},
                    new Artist() {Name = "Harry Styles"}
                );

                context.SaveChanges();
            }
            else
            {
                Console.WriteLine("--> We already have data");
            }
        }
    }
}