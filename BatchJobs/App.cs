using Microsoft.Extensions.Configuration;
using Serilog;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using Domain.Common;
using Domain.Twitter;
using Domain.Dispather;

namespace BatchJobs
{
    public class App
    {
        private readonly IConfiguration config;
        private readonly ILogger logger;
        private readonly IEnumerable<IClient> client;
        public App(IConfiguration _config, ILogger _logger, IEnumerable<IClient> _client)
        {
            config = _config;
            logger = _logger;
            client = _client;
        }

        public async Task<ICommand> Run(Common.BatchType batchType)
        {
            ICommand Request = null;
            try
            {
                logger.Information($"Recieved Request For : { batchType}");
                IClient handler = client.FirstOrDefault(i => i.GetType().Name == batchType.ToString());
                if (handler != null)
                {
                    Request = await CreateRequest(batchType);
                    logger.Information($"{ batchType} Handler");
                    Request = await handler.Handle(Request);
                }
                else
                {
                    logger.Information($"No Handler Found");
                }
                
            }
            catch (Exception ex)
            {
                Request.ProcessStatus = new Response() { Status = Common.ProcessState.Fail, StatusMessage = ex.Message.ToString() };

            }
            return Request;
        }
        private async Task<ICommand> CreateRequest(Common.BatchType batchType)
        {
            ICommand command = null;
            switch (batchType)
            {
                case Common.BatchType.Twitter:
                  command =  await GetTwitterRequest();
                  break;
            }
            return command;
        }
        private async Task<ICommand> GetTwitterRequest()
        {
            
            TwitterRequest request = new TwitterRequest();
            if (request.ApiKey == null)
            {
                await Task.Run(() =>
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
                    request.CommandType = Common.CommandType.Other;
                });
            }
            return request;//(ICommand)Task.FromResult();
            //logger.Information($"Twitter Request Generated : Api Key : {request.ApiKey.ToString()}, Api Url : {request.ApiUrl}, Report File Path: {request.ReportFilePath}");

        }
    }
}
