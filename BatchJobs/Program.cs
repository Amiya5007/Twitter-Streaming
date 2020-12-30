using Domain.Common;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.IO;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
 


namespace BatchJobs
{
    class Program
    {
        public static async Task Main(string[] args)
        {

            var services = ConfigurationServices.ConfigureServices();

            var serviceProvider = services.BuildServiceProvider();

            foreach (string arg in args)
            {
                if (Enum.TryParse<Common.BatchType>(arg, out Common.BatchType batchType) && !batchType.Equals(Common.BatchType.Other))
                    try
                    {
                        await serviceProvider.GetService<App>().Run(batchType);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message.ToString());
                    }

            }


        }
        
        }
    }
  


