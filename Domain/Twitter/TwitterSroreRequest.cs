using Domain.Common;
using Domain.Dispather;
using System;

namespace Domain.Twitter
{
    public class TwitterSroreRequest :ICommand
    {
        public string StoreFilePath { get; set; }
        public string StoreFileName { get; set; }
        public string Data { get; set; }
        public string FileExtension { get; set; }
        public Common.Common.BatchType RequestType { get; set ; }
        public Common.Common.CommandType CommandType { get; set; }
        public Response ProcessStatus { get; set; }
        public TwitterSroreRequest(ICommand command)
        {
            var Commands = command as TwitterRequest;
            StoreFilePath = Commands.StoreFilePath;
            StoreFileName = Commands.StoreFileName;
            FileExtension = Commands.FileExtension;
            RequestType = Common.Common.BatchType.TwitterStore;
            CommandType = Common.Common.CommandType.Other;
            ProcessStatus = new Response();
            
        }
    }
}
