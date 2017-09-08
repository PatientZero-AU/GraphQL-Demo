using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using ToughCuddles.Core.Contracts;
using ToughCuddles.Core.Enums;

namespace ToughCuddles.Services
{
  public class UserService : IUserService
  {
    private readonly IEnumerable<Claim> _claims;
    public UserService(IEnumerable<Claim> claims)
    {
      _claims = claims;
    }

    public bool IsAdmin()
    {
      return _claims.Where(c => c.Type == ClaimTypes.Role).Any(c => c.Value == $"{CuddlesRole.Admin}");
    }
  }
}
