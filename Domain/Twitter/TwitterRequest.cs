

using Domain.Common;
using Domain.Dispather;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;



namespace Domain.Twitter
{
    public class TwitterRequest :ICommand
    {
        public Common.Common.BatchType RequestType { get; set; }
        public string ApiUrl { get; set; }
        public string ApiKey { get; set; }
        public string ReportFilePath { get; set; }
        public string ReportFileName { get; set; }
        public string StoreFilePath { get; set; }
        public string StoreFileName { get; set; }
        public int BatchVolume { get; set; }
        public int ReportProcessVolume { get; set; }
        public string FileExtension { get; set; }
        public Response ProcessStatus { get ; set ; }
        public Common.Common.CommandType CommandType { get; set; }
        public TwitterRequest()
        {
            ProcessStatus = new Response();
        }
    }
}
