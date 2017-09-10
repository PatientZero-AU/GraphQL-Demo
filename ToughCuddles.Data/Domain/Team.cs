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
    public double AverageWinRate => AllMatches.Count > 0 ? (double)MatchesWinningTeam.Count / AllMatches.Count : 0;
    [NotMapped]
    public int TicketsSoldCount => AllMatches.SelectMany(m => m.Tickets).Count();
  }
}
