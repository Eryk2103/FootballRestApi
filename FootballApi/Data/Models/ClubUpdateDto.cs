using System.ComponentModel.DataAnnotations;

namespace FootballApi.Data.Models
{
    public class ClubUpdateDto
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; } = null!;
    }
}