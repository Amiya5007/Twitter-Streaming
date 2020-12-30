using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
 
using Domain.Twitter;
using Serilog;
using Domain.Dispather;
using Domain.Common;

namespace BatchJobs.TwitterClient
{
    public class Twitter : IClient
    {
        private TwitterRequest request = new TwitterRequest();
        private readonly IConfiguration config = null;
        private readonly ILogger logger;
           
        public Twitter(IConfiguration _config, ILogger _logger)
        {
            config = _config;
            logger = _logger;
        }
        public async Task Handle()
        {
            logger.Information($"TwitterClient Handler Executing");
            GetTwitterRequest();
            var dispatcher = new Dispatcher();
            await dispatcher.Send(request);
             
            if (request.ProcessStatus.Status.Equals(Common.ProcessState.Success))
                logger.Information($"TwitterClient Handler Executed Successfully");
            else
                logger.Error(request.ProcessStatus.StatusMessage.ToString());
        }
        private void GetTwitterRequest()
        {
            if (request.ApiKey == null)
            {
                request.ApiKey = config.GetValue<string>("Twitter:ApiKey");
                request.ApiUrl = config.GetValue<string>("Twitter:ApiUrl");
                request.ReportFilePath = config.GetValue<string>("Twitter:ReportFilePath");
                request.ReportFileName = config.GetValue<string>("Twitter:ReportFileName");
                request.BatchVolume = Convert.ToInt32(config.GetValue<string>("Twitter:BatchVolume"));
                request.StoreFilePath = config.GetValue<string>("Twitter:StoreFilePath");
                request.StoreFileName = config.GetValue<string>("Twitter:StoreFileName");
                request.FileExtension = config.GetValue<string>("Twitter:FileExtension");
                request.ReportProcessVolume = Convert.ToInt32(config.GetValue<string>("Twitter:ReportProcessVolume"));
                request.RequestType = Common.BatchType.Twitter;
            }
            logger.Information($"Twitter Request Generated : Api Key : {request.ApiKey.ToString()}, Api Url : {request.ApiUrl}, Report File Path: {request.ReportFilePath}");

        }
    }
}
