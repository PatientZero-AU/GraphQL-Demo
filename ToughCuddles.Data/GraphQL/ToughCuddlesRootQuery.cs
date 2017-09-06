using GraphQL.Types;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using ToughCuddles.Data.Models;

namespace ToughCuddles.Data.GraphQL
{
  public class ToughCuddlesRootQuery : ObjectGraphType
  {
    public ToughCuddlesRootQuery()
    {
      Name = "toughcuddles";

      FieldAsync<ListGraphType<ContestantType>>(
        "contestants",
        resolve: async ctx =>
        {
          var dbCtx = (ToughCuddlesContext)ctx.UserContext;
          return await dbCtx.Contestants.AsNoTracking().ToArrayAsync(ctx.CancellationToken);
        });

      FieldAsync<ContestantType>(
        "contestant",
        arguments: new QueryArguments(new QueryArgument<NonNullGraphType<IdGraphType>> { Name = "contestantId" }),
        resolve: async ctx =>
        {
          var id = ctx.GetArgument<Guid>("contestantId");
          var dbCtx = (ToughCuddlesContext)ctx.UserContext;
          return await dbCtx.Contestants.AsNoTracking().SingleAsync(c => c.Id == id, ctx.CancellationToken);
        });

      FieldAsync<TeamType>(
        "team",
        arguments: new QueryArguments(new QueryArgument<NonNullGraphType<IdGraphType>> { Name = "teamId" }),
        resolve: async ctx =>
        {
          var id = ctx.GetArgument<Guid>("teamId");
          var dbCtx = (ToughCuddlesContext)ctx.UserContext;
          return await dbCtx.Teams.Include(t => t.Contestants).AsNoTracking().SingleAsync(c => c.Id == id, ctx.CancellationToken);
        });

      FieldAsync<ListGraphType<TeamType>>(
        "teams",
        resolve: async ctx =>
        {
          var dbCtx = (ToughCuddlesContext)ctx.UserContext;
          var q = await dbCtx.Teams
          .Include(t => t.Contestants)
          .Include(t => t.MatchesAwayTeam)
          .ToArrayAsync(ctx.CancellationToken);
          
          return q;
        });

    }
  }

  public class ContestantType : ObjectGraphType<Contestant>
  {
    public ContestantType()
    {
      Field(r => r.Id, type: typeof(IdGraphType));
      Field(r => r.DominantHand);
      Field(r => r.Name);
      FieldAsync<TeamType>(
        "team",
        resolve: async ctx =>
        {
          var contestant = ctx.Source;
          var dbCtx = (ToughCuddlesContext)ctx.UserContext;
          return await dbCtx.Teams.AsNoTracking().SingleAsync(c => c.Id == contestant.TeamId, ctx.CancellationToken);
        });
    }
  }

  public class TeamType : ObjectGraphType<Team>
  {
    public TeamType()
    {
      Field(r => r.Id, type: typeof(IdGraphType));
      Field(r => r.Name);
      Field(r => r.JoinDate);

      Field(r => r.AverageWinRate, type: typeof(FloatGraphType));

      FieldAsync<ListGraphType<ContestantType>>(
        "contestants",
        resolve: async ctx =>
        {
          var team = ctx.Source;
          var dbCtx = (ToughCuddlesContext)ctx.UserContext;
          return await dbCtx.Contestants
            .Where(c => c.TeamId == team.Id).AsNoTracking()
            .ToArrayAsync(ctx.CancellationToken);
        });

      FieldAsync<ListGraphType<MatchType>>(
        "matches",
        resolve: async ctx =>
        {
          var team = ctx.Source;
          var dbCtx = (ToughCuddlesContext)ctx.UserContext;
          return await dbCtx.Matches
            .Where(m => m.HomeTeamId == team.Id || m.AwayTeamId == team.Id)
            .AsNoTracking()
            .ToArrayAsync(ctx.CancellationToken);
        });

      // Authz
      FieldAsync<IntGraphType>("ticketsSold", resolve: async ctx =>
      {
        var team = ctx.Source;
        var dbCtx = (ToughCuddlesContext)ctx.UserContext;
        return await dbCtx.Matches
          .Where(m => m.HomeTeamId == team.Id || m.AwayTeamId == team.Id)
          .SelectMany(m => m.Tickets)
          .AsNoTracking()
          .CountAsync(ctx.CancellationToken);
      });
    }
  }

  public class MatchType : ObjectGraphType<Match>
  {
    public MatchType()
    {
      Field(m => m.Id, type: typeof(IdGraphType));
      Field(m => m.Date, type: typeof(DateGraphType));
      Field(m => m.HomeTeam, type: typeof(TeamType));
      Field(m => m.AwayTeam, type: typeof(TeamType));
      Field(m => m.WinningTeam, type: typeof(TeamType), nullable: true);
      Field(m => m.HomeOdds, type: typeof(FloatGraphType));
      Field(m => m.AwayOdds, type: typeof(FloatGraphType));
      FieldAsync<ListGraphType<TicketType>>("tickets",
        resolve: async ctx =>
        {
          var match = ctx.Source;
          var dbCtx = (ToughCuddlesContext)ctx.UserContext;
          return await dbCtx.Tickets.Where(t => t.MatchId == match.Id).AsNoTracking().ToArrayAsync(ctx.CancellationToken);
        });

      FieldAsync<UmpireType>(
        "umpire",
        resolve: async ctx =>
        {
          var match = ctx.Source;
          var dbCtx = (ToughCuddlesContext)ctx.UserContext;
          return await dbCtx.Umpires.SingleAsync(u => u.Id == match.UmpireId, ctx.CancellationToken);
        });
    }
  }

  public class TicketType : ObjectGraphType<Ticket>
  {
    public TicketType()
    {
      Field(t => t.Price, type: typeof(FloatGraphType));
      Field(t => t.Seat);
      Field(t => t.Venue, type: typeof(VenueType));
      Field(t => t.Match, type: typeof(MatchType));
    }
  }

  public class UmpireType : ObjectGraphType<Umpire>
  {
    public UmpireType()
    {
      Field(u => u.Name);
      Field(u => u.TotalMatchStops);
      FieldAsync<ListGraphType<MatchType>>("matches",
        resolve: async ctx =>
        {
          var umpire = ctx.Source;
          var dbCtx = (ToughCuddlesContext)ctx.UserContext;
          return await dbCtx.Matches.Where(m => m.UmpireId == umpire.Id).AsNoTracking().ToArrayAsync(ctx.CancellationToken);
        });
    }
  }

  public class VenueType : ObjectGraphType<Venue>
  {
    public VenueType()
    {
      Field(v => v.Name);
      Field(v => v.Capacity);
      Field(v => v.Tickets, type: typeof(ListGraphType<TicketType>));
    }
  }
}
