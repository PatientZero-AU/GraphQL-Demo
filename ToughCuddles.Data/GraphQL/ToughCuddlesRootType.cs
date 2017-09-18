using GraphQL.Types;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using GraphQL;
using ToughCuddles.Core.Enums;
using ToughCuddles.Core.Helpers;
using ToughCuddles.Data.Context;
using ToughCuddles.Data.Enums;
using ToughCuddles.Data.Models;

namespace ToughCuddles.Data.GraphQL
{
  public class ToughCuddlesRootType : ObjectGraphType
  {
    /// <summary>
    /// 1) The entry point to the schema is called the Root Type
    /// </summary>
    public ToughCuddlesRootType()
    {
      Name = "toughcuddles";

      FieldAsync<ListGraphType<ContestantType>>(
        "contestants",
        resolve: async ctx =>
        {
          var dbCtx = ctx.UserContext.As<GraphQlUserContext>().DbContext;
          return await dbCtx.Contestants
                            .Include(t => t.Team)
                            .AsNoTracking()
                            .ToArrayAsync(ctx.CancellationToken);
        });

      FieldAsync<ContestantType>(
        "contestant",
        arguments: new QueryArguments(new QueryArgument<NonNullGraphType<IdGraphType>> { Name = "contestantId" }),
        resolve: async ctx =>
        {
          var id = ctx.GetArgument<Guid>("contestantId");
          var dbCtx = ctx.UserContext.As<GraphQlUserContext>().DbContext;
          return await dbCtx.Contestants.AsNoTracking().SingleAsync(c => c.Id == id, ctx.CancellationToken);
        });

      FieldAsync<TeamType>(
        "team",
        arguments: new QueryArguments(new QueryArgument<NonNullGraphType<IdGraphType>> { Name = "teamId" }),
        resolve: async ctx =>
        {
          var id = ctx.GetArgument<Guid>("teamId");
          var dbCtx = ctx.UserContext.As<GraphQlUserContext>().DbContext;
          return await dbCtx.Teams.Include(t => t.Contestants).AsNoTracking().SingleAsync(c => c.Id == id, ctx.CancellationToken);
        });

      // 2) Every type has fields which are also GraphTypes
      FieldAsync<ListGraphType<TeamType>>("teams",
        // 3) The resolver function of a field is responsible to fetch the data
        resolve:
        // 4) ctx is an object provided by the runtime
        async ctx =>
        {
          // 5) userCtx is an object provided by us
          var userCtx = ctx.UserContext.As<GraphQlUserContext>();

          // 6) DbCtx was injected to the userCtx
          var dbCtx = userCtx.DbContext;

          return await dbCtx.Teams
                        .Include(t => t.MatchesAwayTeam)
                          .ThenInclude(m => m.Tickets)
                        .Include(m => m.MatchesHomeTeam)
                          .ThenInclude(m => m.Tickets)
                        .Include(m => m.MatchesWinningTeam)
                        .Include(t => t.Contestants)
                        .OrderBy(t => t.Name)
                        .ToArrayAsync(ctx.CancellationToken);
        });

      FieldAsync<MatchType>(
        "match",
        // 1) This is where the field declares its arguments
        arguments: new QueryArguments(new QueryArgument<NonNullGraphType<IdGraphType>> { Name = "matchId" }),
        resolve: async ctx =>
        {
          // 2) we get the argument for the provided context
          var id = ctx.GetArgument<Guid>("matchId");

          var dbCtx = ctx.UserContext.As<GraphQlUserContext>().DbContext;
          return await dbCtx.Matches
            .Include(m => m.HomeTeam)
              .ThenInclude(t => t.Contestants)
            .Include(m => m.AwayTeam)
              .ThenInclude(t => t.Contestants)
            .SingleAsync(m => m.Id == id, ctx.CancellationToken);
        });

      FieldAsync<ListGraphType<MatchType>>(
        "matches",
        resolve: async ctx =>
        {
          var dbCtx = ctx.UserContext.As<GraphQlUserContext>().DbContext;
          return await dbCtx.Matches
                            .Include(m => m.HomeTeam)
                            .Include(m => m.AwayTeam)
                            .OrderBy(m => m.Date)
                            .AsNoTracking()
                            .ToArrayAsync(ctx.CancellationToken);
        });

      FieldAsync<ListGraphType<VenueType>>(
        "venues",
        resolve: async ctx =>
        {
          var dbCtx = ctx.UserContext.As<GraphQlUserContext>().DbContext;
          return await dbCtx.Venues
            .Include(v => v.Tickets)
              .ThenInclude(t => t.Match)
            .OrderBy(v => v.Name)
            .ToArrayAsync(ctx.CancellationToken);
        }).RequestClaim(CuddlesRole.Admin);
    }
  }

  /// <summary>
  /// 1) The Graph ContestantType is mapped to our Contestant entity
  /// </summary>
  public class ContestantType : ObjectGraphType<Contestant>
  {
    public ContestantType()
    {
      Field(r => r.Id, type: typeof(IdGraphType));

      // 2) Primitive fields get mapped automatically by value
      Field(r => r.Name);
      Field(r => r.ImageUrl);
      Field(r => r.DominantHand);
      Field(r => r.HeightCm, type: typeof(FloatGraphType));
      Field(r => r.WeightKg, type: typeof(FloatGraphType));
      Field(r => r.ReachCm, type: typeof(FloatGraphType));
      Field(r => r.StrikesMin, type: typeof(IntGraphType));

      // 3) Custom types also get mapped automatically
      Field(r => r.Team, type: typeof(TeamType));
    }
  }

  public class TeamType : ObjectGraphType<Team>
  {
    public TeamType()
    {
      Field(r => r.Id, type: typeof(IdGraphType));
      Field(r => r.Name);
      Field(r => r.JoinDate);
      Field(r => r.TicketsSoldCount, type: typeof(IntGraphType));

      // 1) We can use the behavior of our domain entities during the value mapping
      Field(r => r.WinRateAvg, type: typeof(FloatGraphType));

      Field(r => r.HeightCmAvg, type: typeof(FloatGraphType));
      Field(r => r.WeightKgAvg, type: typeof(FloatGraphType));
      Field(r => r.ReachCmAvg, type: typeof(FloatGraphType));
      Field(r => r.StrikesMinAvg, type: typeof(FloatGraphType));

      FieldAsync<ListGraphType<ContestantType>>(
        "contestants",
        resolve: async ctx =>
        {
          var team = ctx.Source;
          var dbCtx = ctx.UserContext.As<GraphQlUserContext>().DbContext; ;
          return await dbCtx.Contestants
            .Where(c => c.TeamId == team.Id).AsNoTracking()
            .ToArrayAsync(ctx.CancellationToken);
        });

      FieldAsync<ListGraphType<MatchType>>(
        "matches",
        resolve: async ctx =>
        {
          var team = ctx.Source;
          var dbCtx = ctx.UserContext.As<GraphQlUserContext>().DbContext;
          return await dbCtx.Matches
            .Where(m => m.HomeTeamId == team.Id || m.AwayTeamId == team.Id)
            .AsNoTracking()
            .ToArrayAsync(ctx.CancellationToken);
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
          var dbCtx = ctx.UserContext.As<GraphQlUserContext>().DbContext;
          return await dbCtx.Tickets.Where(t => t.MatchId == match.Id).AsNoTracking().ToArrayAsync(ctx.CancellationToken);
        });

      FieldAsync<UmpireType>(
        "umpire",
        resolve: async ctx =>
        {
          var match = ctx.Source;
          var dbCtx = ctx.UserContext.As<GraphQlUserContext>().DbContext; ;
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
          var dbCtx = ctx.UserContext.As<GraphQlUserContext>().DbContext; ;
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

      // 1) You can also have custom queries
      Field<ListGraphType<TupleType>>("ticketSales",
        resolve: ctx =>
        {
          var venue = ctx.Source;

          var ticketsPerWeek = venue.Tickets
                                .GroupBy(t => t.DateSold.Date)
                                .OrderBy(t => t.Key)
                                .ToDictionary(t => t.Key, t => t.Count())
                                .ToTupleList();

          return ticketsPerWeek;
        }).RequestClaim(CuddlesRole.Admin); // Demand a Role
    }
  }

  public class TupleType : ObjectGraphType<Tuple<DateTime, int>>
  {
    public TupleType()
    {
      Field(t => t.Item1, type: typeof(DateGraphType));
      Field(t => t.Item2, type: typeof(IntGraphType));
    }
  }

  public class DominantHandEnum : EnumerationGraphType
  {
    public DominantHandEnum()
    {
      Name = nameof(DominantHand);
      AddValue(nameof(DominantHand.Both), "Both Hands", (int)DominantHand.Both);
      AddValue(nameof(DominantHand.Right), "Right Hand", (int)DominantHand.Right);
      AddValue(nameof(DominantHand.Left), "Left Hand", (int)DominantHand.Left);
    }
  }

}
