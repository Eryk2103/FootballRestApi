using System.ComponentModel.DataAnnotations;

namespace FootballApi.Data.Models
{
    public class ClubCreateDto
    {
        [Required]
        public string Name { get; set; } = null!;
    }
}