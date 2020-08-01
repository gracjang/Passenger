using System.Text;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Passenger.API.Framework;
using Passenger.Infrastructure.AutoMapper;
using Passenger.Infrastructure.Extensions;
using Passenger.Infrastructure.IoC;
using Passenger.Infrastructure.Mongo;
using Passenger.Infrastructure.Services.Interfaces;
using Passenger.Infrastructure.Settings;

namespace Passenger.API
{
  public class Startup
  {
    public Startup(IWebHostEnvironment env)
    {
      var builder = new ConfigurationBuilder()
        .SetBasePath(env.ContentRootPath)
        .AddJsonFile("appsettings.json", true, true)
        .AddJsonFile($"appsettings.{env.EnvironmentName}.json", true)
        .AddEnvironmentVariables();

      Configuration = builder.Build();
    }

    public IConfigurationRoot Configuration { get; private set; }

    public ILifetimeScope AutofacContainer { get; private set; }

    // This method gets called by the runtime. Use this method to add services to the container.
    public void ConfigureServices(IServiceCollection services)
    {
      services.AddAutoMapper(typeof(AutoMapperConfiguration));
      services.AddControllers();
      services.AddMemoryCache();
      services.AddMvc()
        .AddJsonOptions(options => { options.JsonSerializerOptions.WriteIndented = true; });
      services.AddSwaggerGen(c =>
      {
        c.SwaggerDoc("v1", new OpenApiInfo()
        {
          Title = "Passenger API",
          Version = "v1",
          Description = "Passenger",
        });
      });
      var jwtSettings = Configuration.GetSettings<JwtSettings>();
      services.AddAuthentication(x =>
        {
          x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
          x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        })
        .AddJwtBearer(x =>
        {
          x.TokenValidationParameters = new TokenValidationParameters()
          {
            ValidIssuer = jwtSettings.Issuer,
            ValidateAudience = false,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.Key)),
            ValidateIssuerSigningKey = true,
          };
        });

      services.AddAuthorization(x => x.AddPolicy("admin", x => x.RequireRole("admin")));
    }

    public void ConfigureContainer(ContainerBuilder builder)
    {
      // Register your own things directly with Autofac, like:
      builder.RegisterModule(new ContainerModule(Configuration));
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app)
    {
      AutofacContainer = app.ApplicationServices.GetAutofacRoot();

      MongoConfigurator.Initialize();
      var dataInitializer = app.ApplicationServices.GetService<IDataInitializer>();
      dataInitializer.SeedAsync();
      app.UseException();
      app.UseRouting();
      app.UseAuthentication();
      app.UseAuthorization();
      app.UseSwagger();
      app.UseSwaggerUI(c => { c.SwaggerEndpoint("/swagger/v1/swagger.json", "Passenger API V1"); });
      app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
    }
  }
}