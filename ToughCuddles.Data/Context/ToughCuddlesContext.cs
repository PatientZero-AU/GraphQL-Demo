using Microsoft.EntityFrameworkCore;

namespace ToughCuddles.Data.Models
{
  public partial class ToughCuddlesContext
    {
      public ToughCuddlesContext(DbContextOptions options) : base(options)
      {
        
      }
    }
}
