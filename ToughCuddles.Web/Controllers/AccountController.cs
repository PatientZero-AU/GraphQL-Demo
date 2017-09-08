using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using ToughCuddles.Core.Enums;

namespace ToughCuddles.Web.Controllers
{
  [Route("api/[controller]/[action]")]
  public class AccountController : Controller
  {
    [HttpPost]
    [ProducesResponseType(typeof(void), 200)]
    public async Task<IActionResult> Login(string username, CancellationToken cancellationToken)
    {
      if (!string.Equals(username, "kermit", StringComparison.OrdinalIgnoreCase)) return Unauthorized();

      var identity = new ClaimsIdentity(CookieAuthenticationDefaults.AuthenticationScheme);
      identity.AddClaim(new Claim(identity.RoleClaimType, $"{CuddlesRole.Admin}"));

      await HttpContext.SignInAsync(
        CookieAuthenticationDefaults.AuthenticationScheme,
        new ClaimsPrincipal(identity),
        new AuthenticationProperties
        {
          IsPersistent = true
        });
      return Ok();
    }

    [HttpPost]
    [ProducesResponseType(typeof(void), 200)]
    public async Task<IActionResult> Logout(CancellationToken cancellationToken)
    {
      await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
      return Ok();
    }
  }
}
