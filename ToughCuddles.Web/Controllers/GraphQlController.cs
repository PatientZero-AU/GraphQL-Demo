using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ToughCuddles.Services;
using ToughCuddles.Web.Dtos;
using ToughCuddles.Web.Helpers;

namespace ToughCuddles.Web.Controllers
{
  [Route("api/[controller]")]
  public class GraphQlController : Controller
  {
    private readonly IGraphQlService _graphQlService;
    public GraphQlController(IGraphQlService graphQlService)
    {
      _graphQlService = graphQlService;
    }

    [HttpGet]
    [AllowAnonymous]
    public async Task<IActionResult> GetAsync()
    {
      return Ok("Hello GraphQL");
    }

    [HttpPost]
    [AllowAnonymous]
    public async Task<IActionResult> PostAsync([FromBody] GraphQlQueryDto query, CancellationToken cancellationToken)
    {
      var queryStr = string.IsNullOrEmpty(query.Query) ? Constants.IntrospectionQuery : query.Query;
      var json = await _graphQlService.ExecuteAsync(queryStr, query.Variables?.ToString(), query.OperationName, cancellationToken);
      
      return Ok(json);
    }
  }
}
