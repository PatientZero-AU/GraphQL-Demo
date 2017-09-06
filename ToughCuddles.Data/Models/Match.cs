using System;
using System.Collections.Generic;

namespace ToughCuddles.Data.Models
{
    public partial class Match
    {
        public Match()
        {
            Tickets = new HashSet<Ticket>();
        }

        public Guid Id { get; set; }
        public Guid HomeTeamId { get; set; }
        public Guid AwayTeamId { get; set; }
        public DateTimeOffset Date { get; set; }
        public decimal HomeOdds { get; set; }
        public decimal AwayOdds { get; set; }
        public Guid UmpireId { get; set; }
        public Guid? WinningTeamId { get; set; }

        public Team AwayTeam { get; set; }
        public Team HomeTeam { get; set; }
        public Umpire Umpire { get; set; }
        public Team WinningTeam { get; set; }
        public ICollection<Ticket> Tickets { get; set; }
    }
}
