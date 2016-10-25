using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using Microsoft.AspNetCore.Hosting;

namespace AppLifetime
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = new WebHostBuilder()
                .UseKestrel()
                .UseContentRoot(Directory.GetCurrentDirectory())
                .UseIISIntegration()
                .UseStartup<Startup>()
                .Build();

            IApplicationLifetime appLifetime = (IApplicationLifetime)host.Services.GetService(typeof(IApplicationLifetime));
            appLifetime.ApplicationStarted.Register(() => Log.Info("Application started"));
            appLifetime.ApplicationStopping.Register(() => Log.Info("Application stopping"));
            appLifetime.ApplicationStopped.Register(() => Log.Info("Application stopped"));

            Log.Info("Before run");
            host.Run();
            Log.Info("After run");
        }
    }
}
