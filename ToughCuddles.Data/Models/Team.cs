using System;
using System.Collections.Generic;

namespace ToughCuddles.Data.Models
{
    public partial class Team
    {
        public Team()
        {
            Contestants = new HashSet<Contestant>();
            MatchesAwayTeam = new HashSet<Match>();
            MatchesHomeTeam = new HashSet<Match>();
            MatchesWinningTeam = new HashSet<Match>();
        }

        public Guid Id { get; set; }
        public string Name { get; set; }
        public DateTime JoinDate { get; set; }

        public ICollection<Contestant> Contestants { get; set; }
        public ICollection<Match> MatchesAwayTeam { get; set; }
        public ICollection<Match> MatchesHomeTeam { get; set; }
        public ICollection<Match> MatchesWinningTeam { get; set; }
    }
}
