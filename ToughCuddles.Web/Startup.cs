using System;
using GraphQL;
using GraphQL.Http;
using GraphQL.Types;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Cors.Internal;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Swashbuckle.AspNetCore.Swagger;
using ToughCuddles.Core.Contracts;
using ToughCuddles.Data.Context;
using ToughCuddles.Data.GraphQL;
using ToughCuddles.Data.Models;
using ToughCuddles.Services;
using ToughCuddles.Web.Filters;
using Schema = GraphQL.Types.Schema;

namespace ToughCuddles.Web
{
    public class Startup
    {
        private readonly ILoggerFactory _logger;
        public Startup(IConfiguration configuration, ILoggerFactory logger)
        {
            Configuration = configuration;
            _logger = logger;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
            services.AddCors(options =>
            {
                options.AddPolicy("AllowSpecificOrigin",
            builder => builder
              .WithOrigins("http://localhost:4200")
              .WithMethods("POST", "GET", "OPTIONS")
              .AllowAnyHeader()
              .AllowCredentials()
              );
            });
            services.Configure<MvcOptions>(options =>
            {
                options.Filters.Add(new CorsAuthorizationFilterFactory("AllowSpecificOrigin"));
                options.Filters.Add(new GlobalExceptionFilter(_logger));
            });

            services.AddScoped<ISchema, Schema>(
              sp =>
              {
                  var query = sp.GetService<ToughCuddlesRootQuery>();
                  return new Schema
                  {
                      Query = query
                  };
              });
            services.AddScoped<GraphQlUserContext>();
            services.AddScoped<ToughCuddlesRootQuery>();
            services.AddScoped<IDocumentExecuter, DocumentExecuter>();
            services.AddScoped<IDocumentWriter, DocumentWriter>(provider => new DocumentWriter(false));

            services.AddScoped<IGraphQlService, GraphQlService>();

            services.AddDbContext<ToughCuddlesContext>(options =>
              options.UseSqlServer(Configuration.GetConnectionString("ToughCuddlesContext"), opt => opt.EnableRetryOnFailure()));
            services.AddTransient<IHttpContextAccessor, HttpContextAccessor>();

            services.AddScoped<IUserService, UserService>(sp =>
            {
                var httpContext = sp.GetService<IHttpContextAccessor>();
                var claims = httpContext.HttpContext.User.Claims;
                return new UserService(claims);
            });

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Title = "Tough Cuddles API", Version = "v1" });
            });

            services
              .AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
              .AddCookie();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory logger)
        {

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseAuthentication();
            app.UseMvc();

            app.UseCors("AllowSpecificOrigin");

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Tough Cuddles V1");
            });
        }
    }
}
