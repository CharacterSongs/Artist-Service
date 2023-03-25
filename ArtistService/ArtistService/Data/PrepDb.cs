using ArtistService.Models;

namespace ArtistService.Data
{
    public static class PrepDb
    {
        public static void PrepPopulation(IApplicationBuilder app)
        {
            using( var serviceScope = app.ApplicationServices.CreateScope())
            {
                SeedData(serviceScope.ServiceProvider.GetService<AppDbContext>());
            }           
        }

        private static void SeedData(AppDbContext context)
        {
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
                Console.WriteLine("--> We got data");
            }
        }
    }
}