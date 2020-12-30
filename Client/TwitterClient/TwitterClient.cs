 
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
 
using Domain.Twitter;
 
using Domain.Dispather;
using Domain.Common;

namespace BatchJobs.TwitterClient
{
    public class Twitter : IClient
    {
      //  private TwitterRequest request = new TwitterRequest();
       
        public async Task<ICommand> Handle(ICommand request)
        {
           // logger.Information($"TwitterClient Handler Executing");
           
            var dispatcher = new Dispatcher();
            await dispatcher.Send(request);
            return request;
            //if (request.ProcessStatus.Status.Equals(Common.ProcessState.Success))
            //    logger.Information($"TwitterClient Handler Executed Successfully");
            //else
            //    logger.Error(request.ProcessStatus.StatusMessage.ToString());
        }
        
    }
}
