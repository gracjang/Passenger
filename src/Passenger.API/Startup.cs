using Autofac;
using Autofac.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Passenger.Infrastructure.IoC;
using Passenger.Infrastructure.Mongo;

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
      services.AddControllers();
      services.AddMvc()
        .AddJsonOptions(options =>
        {
          options.JsonSerializerOptions.WriteIndented = true;
        });
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
      
      app.UseHttpsRedirection();

      app.UseRouting();

      app.UseAuthorization();

      app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
    }
  }
}