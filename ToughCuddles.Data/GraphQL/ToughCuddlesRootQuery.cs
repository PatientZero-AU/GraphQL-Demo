using GraphQL.Types;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using GraphQL.Builders;
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
          return await dbCtx.Teams.Include(t => t.Contestants).AsNoTracking().ToArrayAsync(ctx.CancellationToken);
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
      FieldAsync<ListGraphType<ContestantType>>(
        "contestants",
        resolve: async ctx =>
        {
          var team = ctx.Source;
          var dbCtx = (ToughCuddlesContext)ctx.UserContext;
          return await dbCtx.Contestants.Where(c => c.TeamId == team.Id).ToArrayAsync(ctx.CancellationToken);
        });
    }
  }

  //public class MatchType : ObjectGraphType<Match>
  //{
  //  public MatchType()
  //  {
  //    Field(m => m.Id, type: typeof(IdGraphType));
  //    Field(m => m.Date);
  //    Field(m => m.HomeOdds, type: typeof(FloatGraphType));
  //  }
  //}
}
