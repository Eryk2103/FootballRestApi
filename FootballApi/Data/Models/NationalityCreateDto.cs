using System.ComponentModel.DataAnnotations;

namespace FootballApi.Data.Models
{
    public class NationalityCreateDto
    {
        [Required]
        public string Name { get; set; } = null!;
    }
}