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
            appLifetime.ApplicationStarted.Register(() => AppLog.Info("Application started"));
            appLifetime.ApplicationStopping.Register(() => AppLog.Info("Application stopping"));
            appLifetime.ApplicationStopped.Register(() => AppLog.Info("Application stopped"));

            AppLog.Info("Before run");
            host.Run();
            AppLog.Info("After run");
        }
    }
}
