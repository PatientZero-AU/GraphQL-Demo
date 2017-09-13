using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace ToughCuddles.Data.Models
{
  public partial class Team
  {
    [NotMapped]
    public ICollection<Match> AllMatches => MatchesAwayTeam.Concat(MatchesHomeTeam).ToArray();
    [NotMapped]
    public ICollection<Match> PastMatches => AllMatches.Where(m => m.WinningTeamId.HasValue).ToArray();
    [NotMapped]
    public ICollection<Match> WinningMatches => PastMatches.Where(m => m.WinningTeamId == Id).ToArray();
    [NotMapped]
    public decimal WinRateAvg => AllMatches.Count > 0 ? decimal.Round((decimal)MatchesWinningTeam.Count / AllMatches.Count, 3) : 0;

    [NotMapped]
    public decimal HeightCmAvg => Contestants.Average(c => c.HeightCm);
    [NotMapped]
    public decimal WeightKgAvg => Contestants.Average(c => c.WeightKg);
    [NotMapped]
    public decimal ReachCmAvg => Contestants.Average(c => c.ReachCm);
    [NotMapped]
    public double StrikesMinAvg => Contestants.Average(c => c.StrikesMin);

    [NotMapped]
    public int TicketsSoldCount => AllMatches.SelectMany(m => m.Tickets).Count();
  }
}
