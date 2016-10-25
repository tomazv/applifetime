using System;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace AppLifetime
{
    public static class AppLog
    {
        private const string LogFileName = "AppLifetime.log";

        public static void Info(string entry)
        {
            File.AppendAllText(LogFileName, $"{DateTime.UtcNow} {Process.GetCurrentProcess().Id} {entry}\n");
        }

        public static string[] ReturnLast(int entryCount)
        {
            if (!File.Exists(LogFileName))
                return new string[0];
            return File.ReadAllLines(LogFileName).Reverse().Take(entryCount).ToArray();
        }
    }
}
