using System.IO;
using Autofac.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Passenger.API
{
  public class Program
  {
    public static void Main(string[] args)
    {
      var host = Host.CreateDefaultBuilder(args)
        .UseServiceProviderFactory(new AutofacServiceProviderFactory())
        .ConfigureLogging(logging =>
        {
          logging.ClearProviders();
          logging.AddConsole();
          logging.AddDebug();
        })
        .ConfigureWebHostDefaults(webHostBuilder => {
          webHostBuilder
            .UseContentRoot(Directory.GetCurrentDirectory())
            .UseIISIntegration()
            .UseStartup<Startup>();
        })
        .Build();

      var logger = host.Services.GetRequiredService<ILogger<Program>>();
      logger.LogInformation($"Starting application...");

      host.Run();

      
    }
  }
}