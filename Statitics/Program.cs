using Domain.Common;
using Domain.Twitter;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;



namespace BatchJobs
{
    class Program
    {
        public static async Task Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.Unicode;
            var services = ConfigurationServices.ConfigureServices();

            var serviceProvider = services.BuildServiceProvider();
           
            try
            {
               var Response =  await serviceProvider.GetService<App>().Run(Common.BatchType.Twitter);

                if (Response != null)
                {
                   
                    var Reports = Response as GetTwitterReportRequest;
                    Console.WriteLine($"Total Tweets : -  { Reports.Report.TotalTweets}");
                    Console.WriteLine($"Average Tweets : -  { Reports.Report.AverageTweets}");
                    Console.WriteLine($"Top Emojis : - { String.Join(",",  Reports.Report.TopEmojis.Keys)}");
                    Console.WriteLine($"Top Hastags  : - {String.Join(",", Reports.Report.TopHasTags.Keys)}");
                    Console.WriteLine($"tweets that contain a url (%)  : - {Reports.Report.UrlPercentage}");
                    Console.WriteLine($"tweets that contain a photo url (%)  : - {Reports.Report.AveragePhotoUrl}");
                    Console.WriteLine($"Top Domain : - {String.Join(",", Reports.Report.TopDomains.Keys)}");
                    Console.WriteLine($"Top Person Tag : - {String.Join(",", Reports.Report.TopPersionTag.Keys)}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message.ToString());
            }




        }

    }
}



