using System.ComponentModel.DataAnnotations;

namespace FootballApi.Models
{
    public class ClubDto
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; } = null!;
    }
}

