using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using System;

namespace Standard.TransactionSimulator.WebApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateWebHostBuilder(args).Build().Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args)
        {
            string env = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

            return WebHost.CreateDefaultBuilder(args)
                          .ConfigureAppConfiguration((context, builder) => { builder.AddEnvironmentVariables(); })
                          .UseEnvironment(env)
                          .UseStartup<Startup>();
        }
    }
}
