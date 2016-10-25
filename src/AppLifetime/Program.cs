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
            appLifetime.ApplicationStarted.Register(() => Log("Application started"));
            appLifetime.ApplicationStopping.Register(() => Log("Application stopping"));
            appLifetime.ApplicationStopped.Register(() => Log("Application stopped"));

            Log("Before run");
            host.Run();
            Log("After run");
        }

        private static void Log(string entry)
        {
            File.AppendAllText("AppLifetime.log", $"{DateTime.UtcNow} {Process.GetCurrentProcess().Id} {entry}\n");
        }
    }
}
