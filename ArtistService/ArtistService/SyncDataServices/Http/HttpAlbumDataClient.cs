using System.Text;
using System.Text.Json;
using ArtistService.Dtos;

namespace ArtistService.SyncDataServices.Http
{
    public class HttpAlbumDataClient : IAlbumDataClient
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;

        public HttpAlbumDataClient(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _configuration = configuration;
        }

        public async Task SendArtistToAlbum(ArtistReadDto art)
        {
            var httpContent = new StringContent(
                JsonSerializer.Serialize(art),
                Encoding.UTF8,
                "application/json");   

            var response = await _httpClient.PostAsync($"{_configuration["AlbumService"]}", httpContent);   

            if(response.IsSuccessStatusCode)
            {
                Console.WriteLine("--> Sync POST to AlbumService was Ok!");
            }
            else
            {
                Console.WriteLine("--> Sync POST to AlbumService was not Ok!");       
            }
        }

        public async Task SendArtistToAlbumForDelete(Guid artistId)
        {
           var httpContent = new StringContent(
                JsonSerializer.Serialize(artistId),
                Encoding.UTF8,
                "application/json");   

            var response = await _httpClient.DeleteAsync($"{_configuration["AlbumService"]}{artistId}");
            if(response.IsSuccessStatusCode)
            {
                Console.WriteLine("--> Sync Delete to AlbumService was Ok!");
            }
            else
            {
                Console.WriteLine("--> Sync Delete to AlbumService was not Ok!");       
            }
        }
    }
}