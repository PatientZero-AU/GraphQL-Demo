using System;

namespace ToughCuddles.Web.Exceptions
{
  public class GraphQlInvalidOperationException : Exception
  {
    public GraphQlInvalidOperationException(string message) : base(message)
    {

    }
  }
}
