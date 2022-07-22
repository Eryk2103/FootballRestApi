using System.ComponentModel.DataAnnotations;

namespace FootballApi.Models
{
    public class NationalityDto
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; } = null!;
    }
}
