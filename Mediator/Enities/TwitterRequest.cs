
using Mediator;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;



namespace Domain.Twitter
{
    public class TwitterRequest :ICommand
    {
        public Mediator.Common.BatchType RequestType { get; set; }
        public string ApiUrl { get; set; }
        public string ApiKey { get; set; }
        public string ReportFilePath { get; set; }
        public string ReportFileName { get; set; }
        public string StoreFilePath { get; set; }
        public string StoreFileName { get; set; }
        public int BatchVolume { get; set; }


    }
}
