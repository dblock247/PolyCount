using System;
using System.Collections.Generic;
using CommandLine;
using Microsoft.Extensions.Configuration;
using PolyCount.Sandbox.Models;
using Microsoft.Extensions.DependencyInjection;
using PolyCount.Services;
using Serilog;

namespace PolyCount.Sandbox
{
    class Program
    {
        static void Main(string[] args)
        {
            Parser.Default.ParseArguments<Options>(args)
                .WithParsed(options =>
                {
                    var services = Bootstrap.Load<Services.Boot.PolyCount>()
                        .Build();

                    var configuration = services.GetService<IConfiguration>();

                    Log.Logger = new LoggerConfiguration()
                        .ReadFrom.Configuration(configuration)
                        .CreateLogger();

                    Graph.Load(options.File)
                        .Solve();
                })
                .WithNotParsed(HandleParseError);
        }

        static void HandleParseError(IEnumerable<Error> errors)
        {
            foreach (var error in errors)
                Console.WriteLine(error);

            Environment.Exit(1);
        }
    }
}