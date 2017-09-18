using GraphQL;
using GraphQL.Http;
using GraphQL.Instrumentation;
using GraphQL.Types;
using GraphQL.Validation.Complexity;
using Newtonsoft.Json.Linq;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ToughCuddles.Data.Context;
using ToughCuddles.Data.Validations;

namespace ToughCuddles.Services
{
    public interface IGraphQlService
  {
    Task<string> ExecuteAsync(string query, string variables, string operationName, CancellationToken cancellationToken);
  }

  public class GraphQlService : IGraphQlService
  {
    private readonly ISchema _schema;
    private readonly IDocumentExecuter _executer;
    private readonly IDocumentWriter _writer;
    
    private readonly GraphQlUserContext _ctx;

    public GraphQlService(ISchema schema, IDocumentExecuter executer, IDocumentWriter writer, GraphQlUserContext ctx)
    {
      _schema = schema;
      _executer = executer;
      _writer = writer;
      _ctx = ctx;
    }

    public async Task<string> ExecuteAsync(string query, string variables, string operationName, CancellationToken cancellationToken)
    {
      var inputs = variables.ToInputs();

      var result = await _executer.ExecuteAsync(_ =>
      {
        _.UserContext = _ctx;
        _.Schema = _schema;
        _.Query = query;
        _.OperationName = operationName;
        _.Inputs = inputs;
        _.CancellationToken = cancellationToken;
        _.ComplexityConfiguration = new ComplexityConfiguration { MaxDepth = 15 }; // Set limit to the nesting
        _.FieldMiddleware.Use<InstrumentFieldsMiddleware>();
        _.ValidationRules = new[] { new RequiresAuthValidationRule() }; // Will be executed for every field before it gets resolved
      });

      if (result.Errors?.Any() ?? false)
      {
        var sb = new StringBuilder();
        foreach (var error in result.Errors)
          sb.AppendLine(error.Message);

        return JObject.FromObject(new {Error = sb.ToString()}).ToString();
      }
      return _writer.Write(result);
    }
  }
}
