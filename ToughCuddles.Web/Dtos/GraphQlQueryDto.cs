using Newtonsoft.Json.Linq;

namespace ToughCuddles.Web.Dtos
{
  public class GraphQlQueryDto
    {
        public string OperationName { get; set; }
        public string Query { get; set; }
        public JObject Variables { get; set; }
    }
}
