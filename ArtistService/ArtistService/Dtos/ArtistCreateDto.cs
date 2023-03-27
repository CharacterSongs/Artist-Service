
using System.ComponentModel.DataAnnotations;

namespace ArtistService.Dtos
{
     public class ArtistCreateDto
     {
        [Required]
        public string Name { get; set; }

     }
}