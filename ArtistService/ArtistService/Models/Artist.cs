using System.ComponentModel.DataAnnotations;

namespace ArtistService.Models
{
    public class Artist
    {
        [Key]
        public int Id {get; set;} 
        [Required]
        public string Name { get; set; }
        public DateTime DateOfBirth { get; set; }
    }
}