using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using System;

namespace ToughCuddles.Web.Filters
{
  public class GlobalExceptionFilter : IExceptionFilter
  {
    private readonly ILogger _logger;

    public GlobalExceptionFilter(ILoggerFactory logger)
    {
      if (logger == null)
      {
        throw new ArgumentNullException(nameof(logger));
      }

      _logger = logger.CreateLogger("Global Exception Filter");
    }

    public void OnException(ExceptionContext context)
    {
      _logger.LogError(context.Exception.InnerException?.Message ?? context.Exception.Message);
    }
  }
}
