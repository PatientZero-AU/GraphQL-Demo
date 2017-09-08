using GraphQL;
using GraphQL.Language.AST;
using GraphQL.Validation;
using ToughCuddles.Data.Context;
using ToughCuddles.Data.GraphQL;

namespace ToughCuddles.Data.Validations
{
    public class RequiresAuthValidationRule : IValidationRule
  {
    public INodeVisitor Validate(ValidationContext context)
    {
      var userService = context.UserContext.As<GraphQlUserContext>().UserService;

      return new EnterLeaveListener(_ =>
      {
        _.Match<Field>(fieldAst =>
        {
          var fieldDef = context.TypeInfo.GetFieldDef();
          if (fieldDef.RequiresPermissions() && !userService.IsAdmin())
          {
            context.ReportError(new ValidationError(
              context.OriginalQuery,
              "auth-required",
              "You are not authorized to run this query.",
              fieldAst));
          }
        });
      });
    }
  }
}
