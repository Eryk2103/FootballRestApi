using System;
using System.Collections.Generic;

namespace FootballApi.Models
{
    public partial class Player
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Position { get; set; } = null!;
        public int? Number { get; set; }
        public int? ClubId { get; set; }
        public int? NationalityId { get; set; }

        public virtual Club? Club { get; set; }
        public virtual Nationality? Nationality { get; set; }
    }
}
