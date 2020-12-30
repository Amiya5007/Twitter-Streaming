using Domain.Common;
using Domain.Dispather;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Twitter
{
    public class TwitterReportRequest : ICommand
    {
        public string ReportFilePath { get; set; }
        public string StoreFilePath { get; set; }
        public string ReportFileName { get; set; }
        public string FileExtension { get; set; }
        public Common.Common.BatchType RequestType { get ; set; }
        public Response ProcessStatus { get; set; }
        public Common.Common.CommandType CommandType { get; set; }
        public TwitterReport Report { get; set; }
        public Dictionary<string,List<string>> SourceData { get; set; }
        public int BatchVolume { get; set; }
        public bool Display { get; set; }
        public TwitterReportRequest(ICommand Command)
        {
            var Commands = Command as TwitterRequest;
            StoreFilePath = Commands.StoreFilePath;
            ReportFilePath = Commands.ReportFilePath;
            ReportFileName = Commands.ReportFileName;
            FileExtension = Commands.FileExtension;
            BatchVolume = Commands.BatchVolume;
            RequestType = Common.Common.BatchType.TwitterReport;
            CommandType = Commands.CommandType;// Common.Common.CommandType.Other;
            Display = false;
            Report = new TwitterReport();
            SourceData = new Dictionary<string, List<string>>();
            ProcessStatus = new Response();
        }
    }
}
