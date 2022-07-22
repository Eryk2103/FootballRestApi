﻿using System.ComponentModel.DataAnnotations;

namespace FootballApi.Models
{
    public class PlayerUpdateDto
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; } = null!;
        [Required]
        [RegularExpression("^Forward$|^Midfielder$|^Defender$|^Goalkeeper$", ErrorMessage = "Invalid Value")]
        public string Position { get; set; } = null!;
        [Range(1, 99)]
        public int? Number { get; set; }
        [Required]
        public int? ClubId { get; set; }
        [Required]
        public int? NationalityId { get; set; }
    }
}
