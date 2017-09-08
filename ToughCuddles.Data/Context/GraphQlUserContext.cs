using ToughCuddles.Core.Contracts;
using ToughCuddles.Data.Models;

namespace ToughCuddles.Data.Context
{
    public class GraphQlUserContext
    {
        public readonly IUserService UserService;
        public readonly ToughCuddlesContext DbContext;
        public GraphQlUserContext(IUserService userService, ToughCuddlesContext dbContext)
        {
            UserService = userService;
            DbContext = dbContext;
        }
    }
}
