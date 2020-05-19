using System;
using System.IO;
using Autofac.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using NLog.Web;

namespace Passenger.API
{
  public class Program
  {
    public static void Main(string[] args)
    {
      var logger = NLogBuilder.ConfigureNLog("nlog.config").GetCurrentClassLogger();
      try
      {
        CreateHostBuilder(args).Build().Run();
        logger.Debug($"Starting application...");
      }
      catch (Exception ex)
      {
        logger.Error(ex, "Stopped program because of exception");
        throw;
      }
    }

    public static IHostBuilder CreateHostBuilder(string[] args) =>
      Host.CreateDefaultBuilder(args)
        .UseServiceProviderFactory(new AutofacServiceProviderFactory())
        .ConfigureLogging(logging =>
        {
          logging.ClearProviders();
          logging.SetMinimumLevel(LogLevel.Trace);
        })
        .UseNLog()
        .ConfigureWebHostDefaults(webHostBuilder =>
        {
          webHostBuilder
            .UseContentRoot(Directory.GetCurrentDirectory())
            .UseIISIntegration()
            .UseStartup<Startup>();
        });
  }
}