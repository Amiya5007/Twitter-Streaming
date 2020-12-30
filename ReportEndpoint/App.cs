using Microsoft.Extensions.Configuration;
 
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
         
        private readonly IEnumerable<IClient> client;
        public App(IConfiguration _config,  IEnumerable<IClient> _client)
        {
            config = _config;
            
            client = _client;
        }

        public async Task<ICommand> Run(Common.BatchType batchType)
        {
            ICommand Request = null;
            try
            {
                
                var handler = client.FirstOrDefault(i => i.GetType().Name == batchType.ToString());
                if (handler != null)
                {
                    Request = await CreateRequest(batchType);
                    
                    Request = await handler.Handle(Request);
                }
                else
                {
                    
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
                });
            }
            return request;//(ICommand)Task.FromResult();
            //logger.Information($"Twitter Request Generated : Api Key : {request.ApiKey.ToString()}, Api Url : {request.ApiUrl}, Report File Path: {request.ReportFilePath}");

        }
    }
}
