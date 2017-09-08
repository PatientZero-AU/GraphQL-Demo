using GraphQL.Types;
using System.Collections.Generic;
using System.Linq;
using ToughCuddles.Core.Enums;

namespace ToughCuddles.Data.GraphQL
{
  public static class GraphQlExtensions
  {
    private const string RequiredClaims = "RequiredClaims";

    /// <summary>
    /// Add required claims per field
    /// </summary>
    /// <param name="type"></param>
    /// <param name="roles"></param>
    public static void RequestClaim(this IProvideMetadata type, params CuddlesRole[] roles)
    {
      var requiredClaims = type.GetMetadata<List<CuddlesRole>>(RequiredClaims);

      if (requiredClaims != null) return;
      requiredClaims = new List<CuddlesRole>();
      requiredClaims.AddRange(roles);
      type.Metadata[RequiredClaims] = requiredClaims;
    }

    /// <summary>
    /// During authentication, check whether a fields requires any claims
    /// </summary>
    /// <param name="type"></param>
    /// <returns></returns>
    public static bool RequiresPermissions(this IProvideMetadata type)
    {
      var permissions = type?.GetMetadata<IEnumerable<CuddlesRole>>(RequiredClaims);
      return permissions?.Any() ?? false;
    }
  }
}
