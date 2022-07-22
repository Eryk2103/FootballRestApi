using System.ComponentModel.DataAnnotations;

namespace FootballApi.Data.Models
{
    public class NationalityUpdateDto
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; } = null!;
    }
}