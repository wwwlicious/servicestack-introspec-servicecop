using System;
using System.Diagnostics;
using ServiceStack.Text;

namespace ServiceStack.IntroSpec.ServiceCop
{
    using Serilog;

    class Program
    {
        static void Main(string[] args)
        {
            var appHostUrl = "http://localhost:8088/";
            new AppHost(appHostUrl, CreateLogger()).Init().Start("http://*:8088/");
            $"ServiceStack Self Host with Razor listening at {appHostUrl}".Print();
            Process.Start(appHostUrl);
            Console.ReadLine();
        }

        private static ILogger CreateLogger()
        {
            return new LoggerConfiguration()
                .Enrich.FromLogContext()
                .MinimumLevel.Verbose()
                .WriteTo.LiterateConsole()
                .CreateLogger();
        }
    }
}
