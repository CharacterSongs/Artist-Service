using System.ComponentModel.DataAnnotations;

namespace ArtistService.Models
{
    public class Artist
    {
        [Key]
        public Guid Id {get; set;} 
        [Required]
        public string Name { get; set; }
    }
}