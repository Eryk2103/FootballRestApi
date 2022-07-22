using System.ComponentModel.DataAnnotations;

namespace FootballApi.Models
{
    public class PlayerDto
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; } = null!;
        [Required]
        public string Position { get; set; } = null!;
        public int? Number { get; set; }
        [Required]
        public ClubDto? Club { get; set; }
        [Required]
        public NationalityDto? Nationality { get; set; }
    }
}
