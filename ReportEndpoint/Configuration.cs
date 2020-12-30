 
using BatchJobs.TwitterClient;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
 
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
            
            services.AddSingleton(config)
                    .AddSingleton(logger)
                    .AddScoped<IClient, Twitter>()
                    //.AddScoped<IClient, TaxJar>()
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
        
    }

    
}
