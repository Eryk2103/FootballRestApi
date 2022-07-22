using System;
using System.Collections.Generic;

namespace FootballApi.Models
{
    public partial class Nationality
    {
        public Nationality()
        {
            Players = new HashSet<Player>();
        }

        public int Id { get; set; }
        public string Name { get; set; } = null!;

        public virtual ICollection<Player> Players { get; set; }
    }
}
