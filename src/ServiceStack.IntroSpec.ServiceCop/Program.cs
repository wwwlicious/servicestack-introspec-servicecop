// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at http://mozilla.org/MPL/2.0/. 

namespace ServiceStack.IntroSpec.ServiceCop
{
    using System;
    using System.Diagnostics;

    using Serilog;

    using ServiceStack.Text;

    public class Program
    {
        public static void Main(string[] args)
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
