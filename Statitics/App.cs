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
        public App(IConfiguration _config, IEnumerable<IClient> _client)
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
            
            GetTwitterReportRequest request = new GetTwitterReportRequest();
            if (request.ApiKey == null)
            {
                await Task.Run(() =>
                {
                    
                    request.ReportFilePath = config.GetValue<string>("Twitter:ReportFilePath");
                    request.RequestType = Common.BatchType.Twitter;
                    request.CommandType = Common.CommandType.GetReport;
                });
            }
            return request; 
        }
    }
}
