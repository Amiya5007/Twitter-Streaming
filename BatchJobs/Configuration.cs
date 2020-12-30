
using BatchJobs.TwitterClient;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using Serilog.Core;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace BatchJobs
{
    public static class ConfigurationServices
    {
        public static IServiceCollection ConfigureServices()
        {
            IServiceCollection services = new ServiceCollection();

            var config = LoadConfiguration();
            var logger = LoadLogingConfiguration(config);
            services.AddSingleton(config)
                    .AddSingleton(logger)
                    .AddScoped<IClient, Twitter>()
                    .AddScoped<App>();

            return services;
        }
        private static IConfiguration LoadConfiguration()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);

            return builder.Build();
        }
        private static ILogger LoadLogingConfiguration(IConfiguration config)
        {
            var logDirectory = config.GetValue<string>("Runtime:LogOutputDirectory");
            var logger = new LoggerConfiguration()
                .WriteTo.Console()
                .WriteTo.File(logDirectory)
                .CreateLogger();
            return logger;

        }
    }

    
}
