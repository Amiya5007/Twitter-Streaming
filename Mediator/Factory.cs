
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;


namespace Mediator
{
    public static class Factory
    {
        private static Dictionary<string, object> actionList = new Dictionary<string, object>();
        static Factory()
        {
            actionList.Add(Common.BatchType.Twitter.ToString(), new TwitterHandler());
            
        }
        public static ICommandHandler  GetCommand(Common.BatchType batchType) 
        {
            return (ICommandHandler)actionList.FirstOrDefault(i => i.Key == batchType.ToString()).Value;
             
        }
    }
}
