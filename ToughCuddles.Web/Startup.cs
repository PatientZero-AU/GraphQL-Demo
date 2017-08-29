using GraphQL;
using GraphQL.Http;
using GraphQL.Types;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ToughCuddles.Data.GraphQL;
using ToughCuddles.Data.Models;
using ToughCuddles.Services;

namespace ToughCuddles.Web
{
  public class Startup
  {
    public Startup(IConfiguration configuration)
    {
      Configuration = configuration;
    }

    public IConfiguration Configuration { get; }

    // This method gets called by the runtime. Use this method to add services to the container.
    public void ConfigureServices(IServiceCollection services)
    {
      services.AddMvc();

      services.AddSingleton<ISchema, Schema>(
        provider => new Schema
        {
          Query = new ToughCuddlesRootQuery()
        });

      services.AddSingleton<IDocumentExecuter, DocumentExecuter>();
      services.AddSingleton<IDocumentWriter, DocumentWriter>(provider => new DocumentWriter(true));
      
      services.AddScoped<IGraphQlService, GraphQlService>();
      services.AddDbContext<ToughCuddlesContext>(options =>
        options.UseSqlServer(Configuration.GetConnectionString("ToughCuddlesContext"), opt => opt.EnableRetryOnFailure()));
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IHostingEnvironment env)
    {
      if (env.IsDevelopment())
      {
        app.UseDeveloperExceptionPage();
      }

      app.UseMvc();
    }
  }
}
