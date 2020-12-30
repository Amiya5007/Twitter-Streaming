
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Domain.Twitter;
using Domain.Common;
using Domain.Dispather;

namespace Domain
{
    public static class Factory
    {
        private static Dictionary<string, object> actionList = new Dictionary<string, object>();
        static Factory()
        {
            actionList.Add(Common.Common.BatchType.Twitter.ToString(), new TwitterHandler());
            actionList.Add(Common.Common.BatchType.TwitterStore.ToString(), new TwitterStoreRequestHandler());
            actionList.Add(Common.Common.BatchType.TwitterReportStore.ToString(), new TwitterReportStoreRequestHandler());
            actionList.Add(Common.Common.BatchType.TwitterReport.ToString(), new TwitterReportRequestHandler());
        }
        public static ICommandHandler  GetCommand(Common.Common.BatchType batchType) 
        {
            return (ICommandHandler)actionList.FirstOrDefault(i => i.Key == batchType.ToString()).Value;
        }
    }
}
